using System;
using System.Data;
using System.Windows.Forms;
using BLL;

namespace SistemaBazarPep.Forms {
    public partial class FormInserirProdutos : Form {
        public double precoTotal = 0;
        
        public FormInserirProdutos(ListView produtos) {
            InitializeComponent();
            preencheProdutosCB();
            foreach(ListViewItem item in produtos.Items) {
                listView1.Items.Add((ListViewItem)item.Clone());
                precoTotal += Convert.ToDouble(item.SubItems[3].Text);
                mudaEstoqueCB(getIndexByStrId(item.SubItems[0].Text), Convert.ToInt32(item.SubItems[2].Text));
            }
            lblPreco.Text = precoTotal.ToString();
            
        }

        private void FormInserirProdutos_Load(object sender, EventArgs e) {
            
        }

        public void preencheProdutosCB() {
            ProdutoBLL pBll = new ProdutoBLL();
            DataTable dt = pBll.GetProdutosNames();
            for(int i = 0; i < dt.Rows.Count; i++) {
                cbProdutos.Items.Add(dt.Rows[i].ItemArray[0] + " - " + dt.Rows[i].ItemArray[1] + " - "
                    + dt.Rows[i].ItemArray[2] + " - " + dt.Rows[i].ItemArray[3]);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnAdicionar_Click(object sender, EventArgs e) {
            if(cbProdutos.SelectedIndex == -1) {
                MessageBox.Show("Produto não pode ser vazio!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbProdutos.Focus();
                return;
            }
            if(String.IsNullOrEmpty(txtNumQtd.Value.ToString().Trim()) || txtNumQtd.Value == 0) {
                MessageBox.Show("Quantidade não pode ser nula/vazia!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumQtd.Focus();
                return;
            }
            if(String.IsNullOrEmpty(txtNumPreco.Value.ToString().Trim()) || txtNumPreco.Value == 0) {
                MessageBox.Show("Preço não pode ser nulo/vazio!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNumPreco.Focus();
                return;
            }

            string[] row = new string[5];
            string[] propiedadesProduto = cbProdutos.SelectedItem.ToString().Split('-');

            row[0] = propiedadesProduto[0].Trim();
            row[1] = cbProdutos.SelectedItem.ToString().Substring(propiedadesProduto[0].Length + 2);
            row[2] = txtNumQtd.Value.ToString();
            row[3] = Convert.ToString(txtNumPreco.Value * (1 - txtNumDesconto.Value / 100));
            if(txtNumDesconto.Value != 0) {
                row[4] = txtNumDesconto.Value + "%";
            }
            ListViewItem i = new ListViewItem(row);
            listView1.Items.Add(i);
            precoTotal += Convert.ToDouble(row[3]);
            lblPreco.Text = precoTotal.ToString();
            mudaEstoqueCB(cbProdutos.SelectedIndex, - Convert.ToInt32(row[2]));
            limpar();
        }

        private void btnRemover_Click(object sender, EventArgs e) {
            foreach(ListViewItem i in listView1.SelectedItems) {
                listView1.Items.Remove(i);
                precoTotal -= Convert.ToDouble(i.SubItems[3].Text);
                mudaEstoqueCB(getIndexByStrId(i.SubItems[0].Text), Convert.ToInt32(i.SubItems[2].Text));
            }
            lblPreco.Text = precoTotal.ToString();
        }

        public void limpar() {
            cbProdutos.SelectedIndex = -1;
            txtNumDesconto.Value = 0;
        }

        private void txtNumQtd_ValueChanged(object sender, EventArgs e) {
            if(cbProdutos.SelectedIndex != -1) {
                txtNumPreco.Value =
                    Convert.ToDecimal(cbProdutos.SelectedItem.ToString().Split('-')[2].Trim()) * txtNumQtd.Value;
            }
        }

        private void cbProdutos_SelectedIndexChanged(object sender, EventArgs e) {
            if(cbProdutos.SelectedIndex != -1) {
                int estoque = Convert.ToInt32(cbProdutos.SelectedItem.ToString().Split('-')[3].Trim());
                txtNumQtd_ValueChanged(txtNumQtd, EventArgs.Empty);
                txtNumQtd.Maximum = estoque;
                if(estoque > 0) {
                    txtNumQtd.Value = 1;
                }
            }
        }

        private void mudaEstoqueCB(int index, int qtd) {
            string[] row = cbProdutos.Items[index].ToString().Split('-');
            int qtdAtual = Convert.ToInt32(row[3].Trim());
            cbProdutos.Items[index] = row[0] + "-" + row[1] + "-" + row[2] + "- " + (qtdAtual + qtd); 
        }

        public int getIndexByStrId(string strId) {
            int id = Convert.ToInt32(strId);
            for(int i = 0; i < cbProdutos.Items.Count; i++) {
                if(Convert.ToInt32(cbProdutos.Items[i].ToString().Split('-')[0].Trim()) == id) {
                    return i;
                }
            }
            return -1;
        }
    }
}
