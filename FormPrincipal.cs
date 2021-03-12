using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaBazarPep {
    public partial class FormPrincipal : Form {
        private Form activeForm;
        private Button currentBtn;
        private Panel leftBorderBtn;

        public FormPrincipal() {
            InitializeComponent();

            leftBorderBtn = new Panel {
                Size = new Size(9, 75),
                BackColor = Color.FromArgb(10, 10, 50)
            };
            panelMenu.Controls.Add(leftBorderBtn);
        }

        private void FormPrincipal_Load(object sender, EventArgs e) {
           
        }

        public void ActivateButton(object sender) {
            if(sender != null) {
                DisableButton();
                currentBtn = (Button)sender;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }

        public void DisableButton() {
            if(currentBtn != null) {
                currentBtn.BackColor = Color.FromArgb(51, 51, 76);
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                leftBorderBtn.Visible = false;
            }
        }

        public void openChildForm(Form childForm, object btnSender) { 
            if(activeForm != null) {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelForms.Controls.Add(childForm);
            this.panelForms.Tag = childForm;
            childForm.BringToFront();
            lblTitle.Text = childForm.Text;
            activeForm.Show();
        }

        private void btnFornecedor_Click(object sender, EventArgs e) {
            openChildForm(new Forms.FormFornecedores(), sender);
            ActivateButton(sender);
        }

        private void btnProdutos_Click(object sender, EventArgs e) {
            openChildForm(new Forms.FormProdutos(), sender);
            ActivateButton(sender);
        }

        private void btnVendas_Click(object sender, EventArgs e) {
            openChildForm(new Forms.FormVendas(), sender);
            ActivateButton(sender);
        }

        private void btnRelatorio_Click(object sender, EventArgs e) {
            using(Forms.FormDialogDataRelatorio r = new Forms.FormDialogDataRelatorio()) {
                if(r.ShowDialog() == DialogResult.OK) {
                    openChildForm(new Forms.FormRelatorio(r.dIncio, r.dFim), sender);
                    ActivateButton(sender);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) {
            lblTitle.Text = "Início";
            if(activeForm != null) {
                activeForm.Close();
            }
            DisableButton();
        }
    }
}
