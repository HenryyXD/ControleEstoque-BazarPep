using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using Exceptions;


//to do: ERP dos input do cadastro
namespace SistemaBazarPep.Forms {
    public partial class FormProdutos : Form {
        readonly ProdutoBLL pBll;
        Operacao op = Operacao.INSERIR_NOVO;
        TipoSelect ts = TipoSelect.ALL;
        bool txtMenuPesquisaHasDefaultValue = true;
        bool load = false;
        public enum Operacao {
            INSERIR_NOVO, EDITAR_EXISTENTE
        }

        public enum TipoSelect {
            ALL, SEARCH
        }

        public FormProdutos() {
            InitializeComponent();
            pBll = new ProdutoBLL();
        }

        private void FormProdutos_Load(object sender, EventArgs e) {
            loadData(getSelectDataTable());
            disableButtonsConditionally();
            changeHeadersText();
            preencheFornecedoresCB();
            load = true;
            dgvProduto_Resize(dgvProduto, EventArgs.Empty);
        }

        private void changeHeadersText() {
            try {
                dgvProduto.Columns[0].HeaderText = "ID";
                dgvProduto.Columns[1].HeaderText = "Forncedor";
                dgvProduto.Columns[2].HeaderText = "Nome";
                dgvProduto.Columns[3].HeaderText = "Preço";
                dgvProduto.Columns[4].HeaderText = "Quantidade";
                dgvProduto.Columns[5].HeaderText = "Data de Aquisição";
                dgvProduto.Columns[6].HeaderText = "Descrição";
            }catch(Exception) {
                MessageBox.Show("Houve um erro ao tentar exibir a lista!", "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void disableButtonsConditionally() {
            int qtd = dgvProduto.SelectedRows.Count;
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

        public DataTable getSelectDataTable() {
            try {
                switch(ts) {
                    case TipoSelect.ALL:
                        return pBll.Select();
                    case TipoSelect.SEARCH:
                        return pBll.Search(txtMenuPesquisa.Text.Replace(",","."));
                }
            } catch(DbConnectionException ex) {
                showMessageBoxDbException(ex);
            } catch(Exception ex) {
                MessageBox.Show("Houve um erro ao tentar carregar os dados dos Fornecedores! " + ex.Message, "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private void loadData(DataTable dt) {
            dgvProduto.DataSource = dt;
            lblId.Text = Convert.ToString(pBll.GetAutoIncrement());
            statusLabelQtdProd.Text = Convert.ToString(dgvProduto.RowCount);
            if(dgvProduto.RowCount > 0) {
                statusLabelQtdProd.ForeColor = Color.Green;
            } else {
                statusLabelQtdProd.ForeColor = Color.Red;
            }
        }

        private void preencheFornecedoresCB() {
            FornecedorBLL fBll = new FornecedorBLL();
            DataTable dt = fBll.getFornecedoresNames();
            for(int i = 0; i < dt.Rows.Count; i++) {
                cbFornecedores.Items.Add(dt.Rows[i].ItemArray[0] + " - " + dt.Rows[i].ItemArray[1]);
            }
        }

        private void showMessageBoxDbException(DbConnectionException ex) {
            MessageBox.Show("Houve um erro ao tentar conectar com o banco de dados! " + ex.Message, "Erro na conexão com o banco",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            Produto p = new Produto();
            p.fornecedor = new Fornecedor();
            if(preencherDTO(ref p)) {
                try {
                    switch(op) {
                        case Operacao.INSERIR_NOVO:
                            if(pBll.Insert(p)) {
                                MessageBox.Show("Produto adicionado!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadData(getSelectDataTable());
                                btnReset.PerformClick();
                            } else {
                                MessageBox.Show("Produto não foi adicionado", "Falhou",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                        case Operacao.EDITAR_EXISTENTE:
                            if(pBll.Update(p)) {
                                MessageBox.Show("Produto Editado!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadData(getSelectDataTable());
                                btnReset.PerformClick();
                            } else {
                                MessageBox.Show("Não foi possível editar o Produto", "Falhou",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                    }

                    dgvProduto_Resize(dgvProduto, EventArgs.Empty);

                } catch(DbConnectionException ex) {
                    showMessageBoxDbException(ex);
                } catch(Exception ex) {
                    MessageBox.Show("Ocorreu um erro ao salvar o produto! " + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("Preencha os campos corretamente!", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool preencherDTO(ref Produto p) {
            if(validaCampos()) {
                p.id = Convert.ToInt32(lblId.Text);
                p.fornecedor.id = Convert.ToInt32(cbFornecedores.SelectedItem.ToString().Split('-')[0].Trim());
                p.nome = txtNome.Text.Trim();
                p.preco = Convert.ToDouble(txtNumPreco.Value);
                p.qtd = Convert.ToInt32(txtNumQtd.Value);
                if(dateTimePicker1.CustomFormat == " ")
                    p.dataAquisicao = null;
                else 
                    p.dataAquisicao = dateTimePicker1.Value.Date;
                
                p.descricao = txtDescricao.Text.Trim();
                return true;
            }
            return false;
        }

        private bool validaCampos() {
            return !(   cbFornecedores.SelectedIndex == -1 
                        || String.IsNullOrEmpty(txtNome.Text.Trim())
                        || txtNumPreco.Value == 0 
                        || String.IsNullOrEmpty(txtNumPreco.Value.ToString().Trim()) 
                        || String.IsNullOrEmpty(txtNumQtd.Value.ToString().Trim())
                   );    
        }

        private void dgvProduto_Resize(object sender, EventArgs e) {
            if(load) {
                int width = dgvProduto.RowHeadersWidth;

                foreach(DataGridViewColumn col in dgvProduto.Columns) {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    width += col.Width;
                }

                if(width < dgvProduto.Width) {
                    /*foreach(DataGridViewColumn col in dgvProduto.Columns) {
                        col. = DataGridViewAutoSizeColumnMode.Fill;
                    }*/

                    dgvProduto.Columns[dgvProduto.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                } 
            }
        }

        private void btnMenuEditar_Click(object sender, EventArgs e) {
            btnMenuEditar.Enabled = false;
            gbProduto.Text = "Editar Produto";
            btnReset.Text = "Cancelar";
            op = Operacao.EDITAR_EXISTENTE;

            DataGridViewRow row = dgvProduto.SelectedRows[0];

            lblId.Text = row.Cells[0].Value.ToString();
            cbFornecedores.SelectedIndex = getComboBoxIndex(pBll.getFk(Convert.ToInt32(row.Cells[0].Value.ToString())));
            txtNome.Text = row.Cells[2].Value.ToString();
            txtNumPreco.Value = Convert.ToDecimal(row.Cells[3].Value);
            txtNumQtd.Value = Convert.ToDecimal(row.Cells[4].Value);
            if(String.IsNullOrEmpty(row.Cells[5].Value.ToString()))
                btnLimpaDate.PerformClick();
            else {
                dateTimePicker1_ValueChanged(dateTimePicker1, EventArgs.Empty);
                dateTimePicker1.Value = DateTime.Parse(row.Cells[5].Value.ToString());
            }
            txtDescricao.Text = row.Cells[6].Value.ToString();
        }

        public int getComboBoxIndex(int key) {
            for(int i = 0; i < cbFornecedores.Items.Count; i++) {
                string id = cbFornecedores.Items[i].ToString().Split('-')[0].Trim();
                if(Convert.ToInt32(id) == key) {
                    return i;
                }
            }
            return -1;
        }

        private void btnReset_Click(object sender, EventArgs e) {
            lblId.Text = pBll.GetAutoIncrement().ToString();
            cbFornecedores.SelectedIndex = -1;
            txtNome.Text = "";
            txtNumPreco.Value = 0;
            txtNumQtd.Value = 0;
            dateTimePicker1.Value = DateTime.Now.Date;
            txtDescricao.Text = "";
            if(op == Operacao.EDITAR_EXISTENTE) {
                btnMenuEditar.Enabled = true;
                gbProduto.Text = "Cadastrar Produto";
                btnReset.Text = "Limpar";
                op = Operacao.INSERIR_NOVO;
            }
        }

        private void btnMenuExcluir_Click(object sender, EventArgs e) {
            DialogResult resultado = MessageBox.Show("Confirma exlcusão?", "Excluir Pruduto",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultado == DialogResult.Yes) {
                try {
                    Produto f;
                    int deleteCount = 0;
                    for(int i = 0; i < dgvProduto.SelectedRows.Count; i++) {
                        int id = Convert.ToInt32(dgvProduto.SelectedRows[i].Cells[0].Value);
                        f = new Produto(id);
                        if(pBll.Delete(f))
                            deleteCount++;
                    }
                    loadData(getSelectDataTable());
                    dgvProduto_Resize(dgvProduto, EventArgs.Empty);
                    MessageBox.Show("Sucesso! " + deleteCount + " produto(s) removido(s)", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);     
                } catch(DbConnectionException ex) {
                    showMessageBoxDbException(ex);
                } catch(Exception ex) {
                    MessageBox.Show("Ocorreu um erro ao deletar o(s) produto(s)! " + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMenuAtualizar_Click(object sender, EventArgs e) {
            loadData(getSelectDataTable());
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

        private void btnLimpaDate_Click(object sender, EventArgs e) {
            dateTimePicker1.CustomFormat = " ";
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void dgvProduto_SelectionChanged(object sender, EventArgs e) {
            disableButtonsConditionally();
        }

        private void cbFornecedores_MouseHover(object sender, EventArgs e) {
            if(cbFornecedores.SelectedIndex != -1)
                toolTipCB.SetToolTip(cbFornecedores, cbFornecedores.SelectedItem.ToString());
        }
    }
}
