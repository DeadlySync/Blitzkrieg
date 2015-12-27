using Blitzkrieg.Controllers;
using System.Linq;
using System;
using System.Windows.Forms;

namespace Blitzkrieg.Views
{
    public partial class frmFirstRun : Form
    {
        public EventHandler OnUserLogin;

        private DatabaseController dbController = null;
        private bool HasPassword = false;

        public frmFirstRun()
        {
            InitializeComponent();
        }

        private void frmFirstRun_Load(object sender, System.EventArgs e)
        {
            dbController = new DatabaseController();
            HasPassword = dbController.LoadPassword();

            if (HasPassword == true)
            {
                this.lblText.Text = "Wellcome Back!\r\n\r\n" +
                    "Please inform your password.";
                this.Update();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PasswordController pc = null;
            string pass = this.txtPass.Text;

            if (HasPassword == false)
            {
                if (string.IsNullOrEmpty(pass))
                {
                    errorProvider.SetError(txtPass, "Please inform a Password.");
                    return;
                }

                if (pass.Length < 6)
                {
                    errorProvider.SetError(txtPass, "Password too short. Must have more than 6 characters.");
                    return;
                }

                if (!pass.Any(char.IsDigit))
                {
                    errorProvider.SetError(txtPass, "Password must contain at least one numeric char.");
                    return;
                }

                pc = new PasswordController();
                dbController.SavePassword(pc.CreateHash(pass));
            }

            this.Close();
        }

        private void frmFirstRun_FormClosing(object sender, FormClosingEventArgs e)
        {
            var isLoggedIn = dbController.MakeLogin(this.txtPass.Text);

            if (isLoggedIn == false)
            {
                MessageBox.Show(this, "Invalid Password.\r\nThis Application will now close.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
                return;
            }

            if (OnUserLogin != null)
                OnUserLogin(isLoggedIn, null);
        }
    }
}
