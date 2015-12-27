using Blitzkrieg.Controllers;
using Blitzkrieg.Views;
using System;
using System.Windows.Forms;

namespace Blitzkrieg
{
    public partial class frmMain : Form
    {
        private static readonly string NAME = "Blitzkrieg";
        private DatabaseController dbController = null;
        public frmMain()
        {
            //TODO: CHECK THIS DAMN DATABASE ADDRESS!
            //TODO: Check what to display on main Grid.
            //TODO: Add context menu to TreeView for editing feeds.
            dbController = new DatabaseController();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
        }

        public TreeView RssTreeView { get { return this.FeedsTree; } }
        public TextBox TorAddress { get { return this.txtAddress; } }
        public TextBox TorPort { get { return this.txtPort; } }
        public TextBox TorUser { get { return this.txtUser; } }
        public TextBox TorPass { get { return this.txtPass; } }
        public TextBox TorUpSeconds { get { return this.txtUpSeconds; } }
        public TextBox TorRefresh { get { return this.txtTorRefresh; } }
        public CheckBox TorChkForceDown { get { return this.chkForceDownload; } }
        public CheckBox TorChkStop100 { get { return this.chkStop100; } }
        public ErrorProvider MainErrorProvider { get { return this.errorProvider; } }

        private void OnLoad(object sender, EventArgs e)
        {
            frmFirstRun frm = new frmFirstRun();
            frm.Owner = this;
            frm.OnUserLogin += OnUserLogin;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(this);
            frm.Dispose();
            frm = null;
        }

        private void OnUserLogin(object sender, EventArgs e)
        {
            bool isLoggedIn = false;
            if (sender is bool)
                isLoggedIn = (bool)sender;

            if (isLoggedIn)
            {
                this.Text += " - Loading...";
                this.Update();

                dbController.LoadSystem(this);
                dbController.LoadFeedTree(this);
                dbController.LoadTorConfig(this);
            }
        }

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            frmAddFeed frm = new frmAddFeed();
            frm.OnClosedForm += OnAddFeedFinished;
            frm.ShowDialog(this);
            frm.Dispose();
            frm = null;
        }

        private void OnAddFeedFinished(object sender, EventArgs e)
        {
            dbController.LoadFeedTree(this);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            dbController.SaveUserConfig(this);
        }

        private void btnTorSave_Click(object sender, EventArgs e)
        {
            try
            {
                dbController.SaveTorConfig(this);
                dbController.LoadTorConfig(this);

                this.Text += " - uTorrent Configuration Saved.";
                this.Update();

                var task = new System.Threading.Tasks.Task(async () =>
                {
                    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));

                    this.Invoke(new Action(() =>
                    {
                        this.Text = NAME;
                        this.Update();
                    }));
                });

                task.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void btnAddDomain_Click(object sender, EventArgs e)
        {
            //TODO: Add DNS domain. http://www.codeproject.com/Articles/34650/How-to-use-the-Windows-NLM-API-to-get-notified-of
        }

        private void btnRemoveDomain_Click(object sender, EventArgs e)
        {
            //TODO: Remove DNS domain. http://www.codeproject.com/Articles/34650/How-to-use-the-Windows-NLM-API-to-get-notified-of
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            //TODO: Add and Clear DNS LOG
        }

        private void btnSaveDns_Click(object sender, EventArgs e)
        {
            //TODO: Save DNS Configuration
            //TODO: Check hot to handle "save" misspress and "update"
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            //TODO: Check all RSS Filters.
        }

        private void btnCheckNone_Click(object sender, EventArgs e)
        {
            //TODO: Uncheck all RSS Filters.
        }

        private void btnDelFilter_Click(object sender, EventArgs e)
        {
            //TODO: Delete all Checked RSS Filters. Create confirmation screen.
        }

        private void btnAddFilter_Click(object sender, EventArgs e)
        {
            //TODO: Add RSS Filter. Create RSS Filter Engine.
        }
    }
}
