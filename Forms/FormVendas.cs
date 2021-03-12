using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using DTO;
using Exceptions;

//to do: Editar, Busca, listagem de produto no dgv

namespace SistemaBazarPep.Forms {
    public partial class FormVendas : Form {
        VendaBLL vBll;
        Operacao op = Operacao.INSERIR_NOVO;
        TipoSelect ts = TipoSelect.ALL;
        bool txtMenuPesquisaHasDefaultValue = true;
        bool load = false;
        ListView produtos = new ListView();
        double precoTotal = 0;

        public enum Operacao {
            INSERIR_NOVO, EDITAR_EXISTENTE
        }

        public enum TipoSelect {
            ALL, SEARCH
        }

        public FormVendas() {
            InitializeComponent();
            vBll = new VendaBLL();
        }

        private void FormVendas_Load(object sender, EventArgs e) {
            loadData(getSelectDataTable());
            disableButtonsConditionally();
            load = true;
        }

        private void loadData(DataTable dt) {
            dgvVenda.DataSource = dt;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            if(!load) {
                dgvVenda.Columns.Add(btn);
                btn.HeaderText = "Produtos";
                btn.Text = "Ver Produtos";
                btn.Name = "btnListaProd";
                btn.UseColumnTextForButtonValue = true;
            }
            lblId.Text = Convert.ToString(vBll.GetAutoIncrement());
            statusLabelQtdVenda.Text = Convert.ToString(dgvVenda.RowCount);
            if(dgvVenda.RowCount > 0) {
                statusLabelQtdVenda.ForeColor = Color.Green;
            } else {
                statusLabelQtdVenda.ForeColor = Color.Red;
            }
        }

        private DataTable getSelectDataTable() {
            try {
                switch(ts) {
                    case TipoSelect.ALL:
                        return vBll.Select();
                    case TipoSelect.SEARCH:
                        return vBll.Search(txtMenuPesquisa.Text.Replace(",", "."));
                }
            } catch(DbConnectionException ex) {
                showMessageBoxDbException(ex);
            } catch(Exception ex) {
                MessageBox.Show("Houve um erro ao tentar carregar os dados das Vendas! " + ex.Message, "Erro",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        private void showMessageBoxDbException(DbConnectionException ex) {
            MessageBox.Show("Houve um erro ao tentar conectar com o banco de dados! " + ex.Message, "Erro na conexão com o banco",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void disableButtonsConditionally() {
            int qtd = dgvVenda.SelectedRows.Count;
/*            if(qtd == 1) {
                btnMenuEditar.Enabled = true;
            } else {
                btnMenuEditar.Enabled = false;
            }*/
            if(qtd >= 1) {
                btnMenuExcluir.Enabled = true;
            } else {
                btnMenuExcluir.Enabled = false;
            }
        }

        private void dgvVenda_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.ColumnIndex == 0 && e.RowIndex != -1){
                int id = Convert.ToInt32(dgvVenda.Rows[e.RowIndex].Cells[1].Value);
                DataTable dt = vBll.SelectItemVenda(id);
                Forms.FormListaProduto list = new FormListaProduto();
                list.preencheListView(dt);
                list.ShowDialog();
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

        private void btnLimpaDate_Click_1(object sender, EventArgs e) {
            dateTimePicker1.CustomFormat = " ";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void dgvVenda_SelectionChanged(object sender, EventArgs e) {
            disableButtonsConditionally();
        }

        private void btnMenuAtualizar_Click_1(object sender, EventArgs e) {
            loadData(getSelectDataTable());
        }


        private void btnMostraForm_Click(object sender, EventArgs e) {
            using(FormInserirProdutos formInserir = new FormInserirProdutos(produtos)) {
                if(formInserir.ShowDialog() == DialogResult.OK) {
                    precoTotal = formInserir.precoTotal;
                    lblPrecoTot.Text = precoTotal.ToString();

                    produtos = new ListView();
                    foreach(ListViewItem item in formInserir.listView1.Items) {
                        produtos.Items.Add((ListViewItem)item.Clone());
                    }
                }
            } 
        }

        private void btnReset_Click(object sender, EventArgs e) {
            produtos = new ListView();
            precoTotal = 0;
            lblPrecoTot.Text = precoTotal.ToString();
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            Venda v = null;
            if(preencherDTO(ref v)) {
                try {
                    switch(op) {
                        case Operacao.INSERIR_NOVO:
                            if(vBll.Insert(v) > 0) {
                                MessageBox.Show("Venda Adicionada! ", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadData(getSelectDataTable());
                                btnReset.PerformClick();
                            } else {
                                MessageBox.Show("Venda não foi adicionada.", "Falhou",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                        /*case Operacao.EDITAR_EXISTENTE:
                            if(pBll.Update(p)) {
                                MessageBox.Show("Produto Editado!", "Sucesso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadData(getSelectDataTable());
                                btnReset.PerformClick();
                            } else {
                                MessageBox.Show("Não foi possível editar o Produto", "Falhou",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;*/
                    }

                } catch(DbConnectionException ex) {
                    showMessageBoxDbException(ex);
                } catch(Exception ex) {
                    MessageBox.Show("Não foi possível adicionar a venda" + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("Preencha os campos corretamente!", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool preencherDTO(ref Venda v) {
            if(produtos.Items.Count > 0) {
                v = new Venda(produtos.Items.Count);
                v.id = Convert.ToInt32(lblId.Text);

                if(dateTimePicker1.CustomFormat == " ")
                    v.dataVenda = null;
                else
                    v.dataVenda = dateTimePicker1.Value;

                v.precoTotal = double.Parse(lblPrecoTot.Text);

                /*if(lblPrecoTot.Text.Contains(","))
                    v.precoTotal = double.Parse(lblPrecoTot.Text.Replace(",","."));
                else {
                    MessageBox.Show(lblPrecoTot.Text + ".00");
                }*/
                //v.precoTotal = double.Parse(lblPrecoTot.Text + ".00");

                for(int i = 0; i < produtos.Items.Count; i++) {
                    ListViewItem r = produtos.Items[i];
                    v.produtosVendidos[i] = new Produto();
                    v.produtosVendidos[i].id = Convert.ToInt32(r.SubItems[0].Text);
                    v.produtosVendidos[i].nome = r.SubItems[1].Text.Split('-')[0].Trim();
                    v.produtosVendidos[i].qtd = Convert.ToInt32(r.SubItems[2].Text);
                    v.produtosVendidos[i].preco = Convert.ToDouble(r.SubItems[3].Text);
                }
                return true;
            }
            return false;
        }

        private void btnMenuExcluir_Click(object sender, EventArgs e) {
            DialogResult resultado = MessageBox.Show("Confirma exlcusão?", "Excluir Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(resultado == DialogResult.Yes) {
                try {
                    Venda v;
                    int deleteCount = 0;
                    for(int i = 0; i < dgvVenda.SelectedRows.Count; i++) {
                        int id = Convert.ToInt32(dgvVenda.SelectedRows[i].Cells[1].Value);
                        DataTable dt = vBll.SelectItemVenda(id);
                        v = new Venda(dt.Rows.Count);
                        v.id = id;

                        for(int k = 0; k < dt.Rows.Count; k++) {
                            v.produtosVendidos[k] = new Produto();
                            if(!String.IsNullOrEmpty(dt.Rows[k].ItemArray[0].ToString().Trim()))
                                v.produtosVendidos[k].id = Convert.ToInt32(dt.Rows[k].ItemArray[0].ToString());

                            v.produtosVendidos[k].qtd = Convert.ToInt32(dt.Rows[k].ItemArray[2].ToString());
                        }

                        if(vBll.Delete(v))
                            deleteCount++;
                    }
                    loadData(getSelectDataTable());
                    MessageBox.Show("Sucesso! " + deleteCount + " vendas(s) removida(s)", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                } catch(DbConnectionException ex) {
                    showMessageBoxDbException(ex);
                } catch(Exception ex) {
                    MessageBox.Show("Ocorreu um erro ao deletar o(s) produto(s)! " + ex.Message, "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
