using System;
using System.Windows.Forms;

namespace SistemaBazarPep.Forms {
    public partial class FormDialogDataRelatorio : Form {
        public DateTime dIncio, dFim;
        public FormDialogDataRelatorio() {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e) {
            if(DateTime.Compare(dtpIncio.Value.Date, dtpFim.Value.Date) <= 0) {
                dIncio = dtpIncio.Value;
                dFim = dtpFim.Value;
                DialogResult = DialogResult.OK;
                Close();
            } else {
                MessageBox.Show("Data inicial não pode ser maior que a data final!", "Aviso", 
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
