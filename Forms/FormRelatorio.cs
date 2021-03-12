using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BLL;

namespace SistemaBazarPep.Forms {
    public partial class FormRelatorio : Form {
        DateTime dInicio, dFim;
        RelatorioBLL rBll;
        VendaBLL vBll;
        bool load = false;
        public FormRelatorio(DateTime dIncio, DateTime dFim) {
            InitializeComponent();
            this.dInicio = dIncio;
            this.dFim = dFim;
            rBll = new RelatorioBLL();
            vBll = new VendaBLL();
        }

        private void FormRelatorio_Load(object sender, EventArgs e) {
            groupBox1.Text = "Relatório de Vendas entre " + dInicio.Date.ToShortDateString() + " e " + dFim.Date.ToShortDateString();
            loadData();
            load = true;
        }
        private void dgvVenda_CellClick(object sender, DataGridViewCellEventArgs e) {
            if(e.ColumnIndex == 0 && e.RowIndex != -1) {
                int id = Convert.ToInt32(dgvVenda.Rows[e.RowIndex].Cells[1].Value);
                DataTable dt = vBll.SelectItemVenda(id);
                Forms.FormListaProduto list = new FormListaProduto();
                list.preencheListView(dt);
                list.ShowDialog();
            }
        }

        public void loadData() {
            dgvVenda.DataSource = vBll.SelectBetweenDate(dInicio, dFim);

            MessageBox.Show(dInicio.ToString());
            Relatorio r = rBll.gerarNovo(dInicio, dFim);
            lblFaturamento.Text = r.faturamento.ToString();
            lblQtdVenda.Text = r.qtdVenda.ToString();

            if(!load && dgvVenda.Rows.Count > 0) {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                dgvVenda.Columns.Add(btn);
                btn.HeaderText = "Produtos";
                btn.Text = "Ver Produtos";
                btn.Name = "btnListaProd";
                btn.UseColumnTextForButtonValue = true;
            }
        }

    }
}
