using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaBazarPep.Forms {
    public partial class FormListaProduto : Form {
        public FormListaProduto() {
            InitializeComponent();
        }

        public void preencheListView(DataTable dt) {
            foreach(DataRow row in dt.Rows) {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for(int i = 1; i < dt.Columns.Count; i++) {
                    item.SubItems.Add(row[i].ToString());
                }
                listView1.Items.Add(item);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
