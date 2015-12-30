using Blitzkrieg.Controllers;
using Blitzkrieg.DataBase;
using Blitzkrieg.Views;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Blitzkrieg
{
    /*
            int timeout = (int)TimeSpan.FromSeconds(3).TotalMilliseconds;
            this.notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            this.notifyIcon.BalloonTipTitle = "Background Execution";
            this.notifyIcon.BalloonTipText = "Double click on Icon to restore.";
            this.notifyIcon.ShowBalloonTip(timeout);
    */
    public partial class frmMain : Form
    {
        private static readonly string NAME = "Blitzkrieg";
        private DatabaseController dbController = null;
        private bool UserClickedExit = false;

        public frmMain()
        {
            //TODO: CHECK THIS DAMN DATABASE ADDRESS!
            //TODO: Check what to display on main Grid.
            //TODO: Add context menu to TreeView for editing feeds.
            dbController = new DatabaseController();
            InitializeComponent();
        }

        public uTorrentObject TorObject { get; set; }
        public TreeView RssTreeView { get { return this.FeedsTree; } }
        public TabControl MainTabControl { get { return this.mainTabs; } }

        public string uTorFullUrl { get; set; }

        private void OnLoad(object sender, EventArgs e)
        {
            TorObject = new uTorrentObject();
            
            //uTorrent DataBind
            this.txtAddress.DataBindings.Add(new Binding("Text", TorObject, "TorAddress"));
            this.txtPort.DataBindings.Add(new Binding("Text", TorObject, "TorPort"));
            this.txtUser.DataBindings.Add(new Binding("Text", TorObject, "TorUser"));
            this.txtPass.DataBindings.Add(new Binding("Text", TorObject, "TorPass"));
            this.chkForceDownload.DataBindings.Add(new Binding("Checked", TorObject, "TorChkForceDown"));
            this.chkStop100.DataBindings.Add(new Binding("Checked", TorObject, "TorChkStop100"));
            this.txtTorRefresh.DataBindings.Add(new Binding("Text", TorObject, "TorRefresh"));
            this.txtUpSeconds.DataBindings.Add(new Binding("Text", TorObject, "TorUpSeconds"));

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
                ChangeNameStatus(" - Loading...");

                dbController.LoadSystem(this);
                dbController.LoadFeedTree(this);

                if (dbController.LoadTorConfig(this))
                {
                    EnableDisableControllers(this.mainTabs.TabPages["uTorTab"], false);
                    this.btnTorSave.Enabled = false;
                    this.btnTorEdit.Visible = true;
                    this.btnTorEdit.Enabled = true;
                    this.Update();
                }
                else
                {
                    this.btnTorSave.Enabled = true;
                    this.btnTorEdit.Enabled = false;
                    this.btnTorEdit.Visible = false;
                    this.Update();
                }
            }
        }

        private void OnAddFeedFinished(object sender, EventArgs e)
        {
            dbController.LoadFeedTree(this);
        }

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            frmAddFeed frm = new frmAddFeed();
            frm.OnClosedForm += OnAddFeedFinished;
            frm.ShowDialog(this);
            frm.Dispose();
            frm = null;
        }

        private void btnTorSave_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider.Clear();

                var error = false;
                TorObject.TorAddress = txtAddress.Text;
                TorObject.TorPort = txtPort.Text;
                TorObject.TorUser = txtUser.Text;
                TorObject.TorPass = txtPass.Text;
                TorObject.TorUpSeconds = txtUpSeconds.Text;
                TorObject.TorRefresh = txtTorRefresh.Text;

                string fullUri = string.Empty;

                #region [ Data validation ]

                if (string.IsNullOrEmpty(TorObject.TorAddress))
                {
                    errorProvider.SetError(txtAddress, "You must inform an Address.");
                    error = true;
                }

                if (string.IsNullOrEmpty(TorObject.TorPort))
                {
                    errorProvider.SetError(txtPort, "You must inform an Connection Port.");
                    error = true;
                }
                else
                {
                    int exit = 0;
                    if (!Int32.TryParse(TorObject.TorPort, out exit))
                    {
                        errorProvider.SetError(txtPort, "Not a number.");
                        error = true;
                    }
                }

                if (string.IsNullOrEmpty(TorObject.TorUser))
                {
                    errorProvider.SetError(txtUser, "You must inform an Username.");
                    error = true;
                }

                if (string.IsNullOrEmpty(TorObject.TorPass))
                {
                    errorProvider.SetError(txtPass, "You must inform a Password.");
                    error = true;
                }

                if (string.IsNullOrEmpty(TorObject.TorUpSeconds))
                {
                    errorProvider.SetError(txtUpSeconds, "You must inform a RSS Update Frequency.");
                    error = true;
                }
                else
                {
                    int exit = 0;
                    if (!Int32.TryParse(TorObject.TorUpSeconds, out exit))
                    {
                        errorProvider.SetError(txtUpSeconds, "Not a number.");
                        error = true;
                    }
                    else
                    {
                        if (exit < 1)
                        {
                            errorProvider.SetError(txtUpSeconds, "Minimum acceptable value is 1 (one) minute.");
                            error = true;
                        }
                    }
                }

                if (string.IsNullOrEmpty(TorObject.TorRefresh))
                {
                    errorProvider.SetError(txtTorRefresh, "You must inform a Refresh Rate for uTorrent.");
                    error = true;
                }
                else
                {
                    int exit = 0;
                    if (!Int32.TryParse(TorObject.TorRefresh, out exit))
                    {
                        errorProvider.SetError(txtTorRefresh, "Not a number.");
                        error = true;
                    }
                    else
                    {
                        if (exit < 5)
                        {
                            errorProvider.SetError(txtTorRefresh, "Minimum acceptable value is 5 (five) seconds.");
                            error = true;
                        }
                    }
                }

                if (error)
                    return;

                if (!string.IsNullOrEmpty(TorObject.TorAddress) && !string.IsNullOrEmpty(TorObject.TorPort))
                {
                    if (TorObject.TorAddress.StartsWith("http://"))
                        error = false;
                    else
                    {
                        if (TorObject.TorAddress.StartsWith("https://"))
                        {
                            error = false;
                        }
                        else
                        {
                            errorProvider.SetError(txtAddress, "You must inform http:// or https://");
                            error = true;
                            return;
                        }
                    }

                    if (TorObject.TorAddress.StartsWith("http://"))
                    {
                        fullUri = "http://" + 
                            TorObject.TorUser + ":" + 
                            TorObject.TorPass + "@" + 
                            TorObject.TorAddress.Substring(7, TorObject.TorAddress.Length - 7) + ":" + 
                            TorObject.TorPort + "/gui";
                    }
                    else if (TorObject.TorAddress.StartsWith("https://"))
                    {
                        fullUri = "https://" + 
                            TorObject.TorUser + ":" + 
                            TorObject.TorPass + "@" + 
                            TorObject.TorAddress.Substring(7, TorObject.TorAddress.Length - 7) + ":" + 
                            TorObject.TorPort + "/gui";
                    }

                    Uri uriResult;
                    bool result = Uri.TryCreate(fullUri, UriKind.Absolute, out uriResult)
                        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                    if (!result)
                    {
                        errorProvider.SetError(txtAddress, "Invalid URL. Please check.");
                        return;
                    }
                    else
                        TorObject.uTorFullUrl = fullUri;
                }

                #endregion [ Data validation ]

                var task = new System.Threading.Tasks.Task(async () =>
                {
                    await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(5));

                    ChangeNameStatus();
                });

                if (dbController.SaveTorConfig(this))
                {
                    ChangeNameStatus(" - uTorrent Configuration Saved.");

                    task.Start();

                    EnableDisableControllers(this.mainTabs.TabPages["uTorTab"], false);
                    this.btnTorSave.Enabled = false;
                    this.btnTorEdit.Visible = true;
                    this.btnTorEdit.Enabled = true;

                    dbController.LoadTorConfig(this);
                }
                else
                {
                    ChangeNameStatus(" - Error Saving uTorrent Configuration.");
                    this.btnTorSave.Enabled = false;
                    this.btnTorEdit.Visible = true;
                    this.btnTorEdit.Enabled = true;

                    task.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "This Application will now close. \r\nReason: " + ex.Message,
                        "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitMenuItem_Click(null, null);
            }
        }

        public void EnableDisableControllers(object parentControl, bool enable)
        {
            var controls = parentControl.GetType().GetProperties().FirstOrDefault(p => p.Name == "Controls");
            IEnumerable controlList = (IEnumerable)controls.GetValue(parentControl, null);

            foreach (var item in controlList)
            {
                if (item is GroupBox)
                    EnableDisableControllers(item, enable);
                else
                    ((Control)item).Enabled = enable;
            }
            
            this.Update();
        }

        public void ChangeNameStatus(string Status = "")
        {
            this.Invoke(new Action(() =>
            {
                if (string.IsNullOrEmpty(Status))
                    this.Text = NAME;
                else
                {
                    this.Text = NAME;
                    this.Update();

                    this.Text += Status;
                }

                this.Update();
            }));
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
            //TODO: Check how to handle "save" misspress and "update"
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            //TODO: Check all RSS Filters.
            var b = new Connection.DownloadFeed();
            var a = b.DownloadFavicon("http://www.tokyotosho.info/rss.php?entries=450");
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

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UserClickedExit)
            {
                this.notifyIcon.Dispose();
                this.notifyIcon = null;
                return;
            }

            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UserClickedExit = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        public void ExitMenuItem_Click(object sender, EventArgs e)
        {
            UserClickedExit = true;
            dbController.SaveUserConfig(this);
            Application.Exit();
        }

        private void btnTorEdit_Click(object sender, EventArgs e)
        {
            EnableDisableControllers(this.mainTabs.TabPages["uTorTab"], true);
            this.btnTorEdit.Enabled = false;
            this.btnTorEdit.Visible = false;
            this.btnTorSave.Enabled = true;
        }
    }
}
