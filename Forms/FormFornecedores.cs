using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using Exceptions;

namespace SistemaBazarPep.Forms {
    public partial class FormFornecedores : Form {
        FornecedorBLL fBll;
        Operacao op = Operacao.INSERIR_NOVO;
        TipoSelect ts = TipoSelect.ALL;
        bool isTelValidated = true, isCepValidated = true;
        bool txtMenuPesquisaHasDefaultValue = true;
        bool load = false;

        public enum Operacao {
            INSERIR_NOVO, EDITAR_EXISTENTE
        }

        public enum TipoSelect {
            ALL, SEARCH
        }

        public FormFornecedores() {
            InitializeComponent();
            fBll = new FornecedorBLL();
        }

        private void FormFornecedores_Load(object sender, EventArgs e) {
            loadData(getSelectDataTable());
            disableButtonsConditionally();
            load = true;
            dgvFornecedor_Resize(dgvFornecedor, EventArgs.Empty);
        }

        //carrega fornecedores na lista
        private void loadData(DataTable dt) {
            dgvFornecedor.DataSource = dt;
            lblId.Text = Convert.ToString(fBll.GetAutoIncrement());
            statusLabelQtdFornec.Text = Convert.ToString(dgvFornecedor.RowCount);
            if(dgvFornecedor.RowCount > 0) {
                statusLabelQtdFornec.ForeColor = Color.Green;
            } else {
                statusLabelQtdFornec.ForeColor = Color.Red;
            }
        }

        public DataTable getSelectDataTable() {
            try {
                switch(ts) {
                    case TipoSelect.ALL:
                        return fBll.Select();
                    case TipoSelect.SEARCH:
                        return fBll.Search(txtMenuPesquisa.Text);
                }
            } catch(DbConnectionException ex) {
                showMessageBoxDbException(ex);
            } catch(Exception ex) {
                MessageBox.Show("Houve um erro ao tentar carregar os dados dos Fornecedores! " + ex.Message, "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private void btnMenuAtualizar_Click(object sender, EventArgs e) {
            loadData(getSelectDataTable());
        }  

        private void showMessageBoxDbException(DbConnectionException ex) {
            MessageBox.Show("Houve um erro ao tentar conectar com o banco de dados! " + ex.Message, "Erro na conexão com o banco",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            Fornecedor f = new Fornecedor();
            if(preencherDTO(ref f)) {
                try {
                    switch(op) {
                        case Operacao.INSERIR_NOVO:
                            if(fBll.Insert(f)) {
                                MessageBox.Show("Fornecedor adicionado!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadData(getSelectDataTable());
                                btnReset.PerformClick();
                            } else {
                                MessageBox.Show("Fornecedor não foi adicionado", "Falhou",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;

                        case Operacao.EDITAR_EXISTENTE:
                            if(fBll.Update(f)) {
                                MessageBox.Show("Fornecedor Editado!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadData(getSelectDataTable());
                                btnReset.PerformClick();
                            } else {
                                MessageBox.Show("Não foi possível editar o fornecedor", "Falhou",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            break;
                    }

                    dgvFornecedor_Resize(dgvFornecedor, EventArgs.Empty);
                } catch(DbConnectionException ex) {
                    showMessageBoxDbException(ex);
                } catch(Exception ex) {
                    MessageBox.Show("Ocorreu um erro ao salvar o fornecedor! " + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("Preencha os campos corretamente!", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool preencherDTO(ref Fornecedor f) {
            if(!String.IsNullOrEmpty(txtNome.Text.Trim()) && isCepValidated && isTelValidated) {
                f.id = Convert.ToInt32(lblId.Text);
                f.nome = txtNome.Text.Trim();

                if(!String.IsNullOrEmpty(removeMask(txtMaskTelefone)))
                    f.telefone = txtMaskTelefone.Text;
                    
                f.rua = txtRua.Text.Trim();

                if(txtNumNumero.Value != 0)
                    f.numero = Convert.ToString(txtNumNumero.Value);
                   
                f.complemento = txtComplemento.Text.Trim();

                if(!String.IsNullOrEmpty(removeMask(txtMaskCep)))
                    f.cep = txtMaskCep.Text;
                   
                f.bairro = txtBairro.Text.Trim();

                return true;
            }

            return false;
        }

        private void btnReset_Click(object sender, EventArgs e) {
            lblId.Text = fBll.GetAutoIncrement().ToString();
            txtNome.Text = "";
            txtMaskTelefone.Text = "";
            txtRua.Text = "";
            txtNumNumero.Value = 0;
            txtComplemento.Text = "";
            txtMaskCep.Text = "";
            txtBairro.Text = "";
            if(op == Operacao.EDITAR_EXISTENTE) {
                btnMenuEditar.Enabled = true;
                gbFornecedor.Text = "Cadastrar Fornecedor";
                btnReset.Text = "Limpar";
                op = Operacao.INSERIR_NOVO;
            }
        }

        private void btnMenuEditar_Click(object sender, EventArgs e) {
            btnMenuEditar.Enabled = false;
            gbFornecedor.Text = "Editar Fornecedor";
            btnReset.Text = "Cancelar";
            op = Operacao.EDITAR_EXISTENTE;

            DataGridViewRow row = dgvFornecedor.SelectedRows[0];

            lblId.Text = row.Cells[0].Value.ToString();
            txtNome.Text = row.Cells[1].Value.ToString();
            txtMaskTelefone.Text = row.Cells[3].Value.ToString();
            txtRua.Text = row.Cells[4].Value.ToString();

            if(String.IsNullOrEmpty(row.Cells[5].Value.ToString())) {
                txtNumNumero.Value = 0;
            } else {
                txtNumNumero.Value = Convert.ToDecimal(row.Cells[5].Value);
            }

            txtComplemento.Text = row.Cells[6].Value.ToString();
            txtMaskCep.Text = row.Cells[7].Value.ToString();
            txtBairro.Text = row.Cells[8].Value.ToString();
        }

        private string removeMask(MaskedTextBox m) {
            m.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string retString = m.Text;
            m.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            return retString;
        }

        private void txtNome_Validating(object sender, CancelEventArgs e) {
            EPR.Clear();
            if(String.IsNullOrEmpty(txtNome.Text.Trim())) {
                txtNome.BackColor = Color.LightCoral;
                EPR.SetError(txtNome, "Preencha o nome");
            } else {
                txtNome.BackColor = Color.WhiteSmoke;
            }
        }

        private void txtMaskTelefone_Validating(object sender, CancelEventArgs e) {
            EPR.Clear();
            if(txtMaskTelefone.MaskCompleted || String.IsNullOrEmpty(removeMask(txtMaskTelefone))) {
                txtMaskTelefone.BackColor = Color.WhiteSmoke;
                isTelValidated = true;
            } else {
                txtMaskTelefone.BackColor = Color.LightCoral;
                EPR.SetError(txtMaskTelefone, "Preencha o telefone");
                isTelValidated = false;
            }
        }

        private void txtMaskCep_Validating(object sender, CancelEventArgs e) {
            EPR.Clear();
            if(txtMaskCep.MaskCompleted || String.IsNullOrEmpty(removeMask(txtMaskCep))) {
                txtMaskCep.BackColor = Color.WhiteSmoke;
                isCepValidated = true;
            } else {
                txtMaskCep.BackColor = Color.LightCoral;
                EPR.SetError(txtMaskCep, "Preencha o CEP");
                isCepValidated = false;
            }
        }

        private void dgvFornecedor_SelectionChanged(object sender, EventArgs e) {
            disableButtonsConditionally();
        }

        private void disableButtonsConditionally() {
            int qtd = dgvFornecedor.SelectedRows.Count;
            if(qtd == 1) {
                btnMenuEditar.Enabled = true;
            } else {
                btnMenuEditar.Enabled = false;
            }

            if(qtd >= 1) {
                btnMenuExcluir.Enabled = true;
            } else {
                btnMenuExcluir.Enabled = false;
            }
        }

        private void btnPesquisa_Click(object sender, EventArgs e) {
            txtMenuPesquisa.Focus();
        }

        private void txtMenuPesquisa_Enter(object sender, EventArgs e) {
            if(txtMenuPesquisaHasDefaultValue) {
                txtMenuPesquisa.Text = "";
                txtMenuPesquisaHasDefaultValue = false;
            }
        }

        private void txtMenuPesquisa_Leave(object sender, EventArgs e) {
            if(String.IsNullOrEmpty(txtMenuPesquisa.Text.Trim())) {
                txtMenuPesquisaHasDefaultValue = true;
                txtMenuPesquisa.Text = "Pesquisar";
            }
        }

        private void txtMenuPesquisa_TextChanged(object sender, EventArgs e) {
            if(txtMenuPesquisaHasDefaultValue) {
                ts = TipoSelect.ALL;
                loadData(getSelectDataTable());
            } else {
                ts = TipoSelect.SEARCH;
                loadData(getSelectDataTable());
            }
        }

        private void dgvFornecedor_Resize(object sender, EventArgs e) {
            if(load) {
                int width = dgvFornecedor.RowHeadersWidth;

                foreach(DataGridViewColumn col in dgvFornecedor.Columns) {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    width += col.Width;
                }

                if(width < dgvFornecedor.Width) {
                    dgvFornecedor.Columns[dgvFornecedor.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void btnMenuExcluir_Click(object sender, EventArgs e) {
            DialogResult resultado = MessageBox.Show("Confirma exlcusão? Isso Excluirá todos os produtos deste(s)" +
                " fonecedor(es)", "Excluir Fornecedor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultado == DialogResult.Yes) {
                try {
                    Fornecedor f;
                    int deleteCount = 0;
                    for(int i = 0; i < dgvFornecedor.SelectedRows.Count; i++) {
                        int id = Convert.ToInt32(dgvFornecedor.SelectedRows[i].Cells[0].Value);
                        f = new Fornecedor(id);
                        if(fBll.Delete(f))
                            deleteCount++;
                    }
                    loadData(getSelectDataTable());
                    dgvFornecedor_Resize(dgvFornecedor, EventArgs.Empty);
                    MessageBox.Show("Sucesso! " + deleteCount + " fornecedor(es) removido(s)", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                } catch(DbConnectionException ex) {
                    showMessageBoxDbException(ex);
                } catch(Exception ex) {
                    MessageBox.Show("Ocorreu um erro ao deletar o(s) fornecedor(es)! " + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
