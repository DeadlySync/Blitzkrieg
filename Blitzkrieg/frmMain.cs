using Blitzkrieg.Controllers;
using Blitzkrieg.Views;
using System;
using System.Windows.Forms;

namespace Blitzkrieg
{
    public partial class frmMain : Form
    {
        private DatabaseController dbController = null;
        public frmMain()
        {
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
            this.Text += " - Loading...";
            this.Update();

            dbController.LoadSystem(this);
            dbController.LoadFeedTree(this);
            dbController.LoadTorConfig(this);
        }

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            frmAddFeed frm = new frmAddFeed();
            frm.OnClosedForm += OnAddFeedFinished;
            frm.ShowDialog(this);
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
            dbController.SaveTorConfig(this);
        }
    }
}
