using Blitzkrieg.Connection;
using Blitzkrieg.Controllers;
using Blitzkrieg.DataBase;
using Blitzkrieg.Views;
using System;
using System.Collections;
using System.Linq;
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

        //TODO: CODE DOCUMENTATION

        private static readonly string NAME = "Blitzkrieg";
        private bool UserClickedExit = false;

        private DatabaseController dbController = null;
        private Timer UpdateFeedTimer = null;
        private Timer RegressiveTimer = null;

        public frmMain()
        {
            dbController = new DatabaseController();
            InitializeComponent();
        }

        public uTorrentObject TorObject { get; set; }
        public TreeView RssTreeView { get { return this.FeedsTree; } }
        public TabControl MainTabControl { get { return this.mainTabs; } }
        public string uTorFullUrl { get; set; }

        #region [ Events ]

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

            if (!isLoggedIn)
                return;

            ChangeNameStatus(" - Loading...");

            dbController.LoadSystem(this);
            dbController.LoadFeedTree(this);

            if (dbController.LoadTorConfig(this))
            {
                EnableDisableControllers(this.mainTabs.TabPages["uTorTab"], false);
                this.btnTorSave.Enabled = false;
                this.btnTorEdit.Visible = true;
                this.btnTorEdit.Enabled = true;
                LoadDataGrid();
                this.Update();
                StartFeedUpdateTimer();
            }
            else
            {
                this.btnTorSave.Enabled = true;
                this.btnTorEdit.Enabled = false;
                this.btnTorEdit.Visible = false;
                this.Update();
            }
        }

        private void OnAddFeedFinished(object sender, EventArgs e)
        {
            dbController.LoadFeedTree(this);
            StartFeedUpdateTimer();
        }

        private void feedsContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.FeedsTree.SelectedNode.Name == "FeedsRoot")
            {
                this.feedsContextMenu.Items["editFeedToolStripMenuItem"].Enabled = false;
                this.feedsContextMenu.Items["deleteFeedToolStripMenuItem"].Enabled = false;
            }
            else
            {
                this.feedsContextMenu.Items["editFeedToolStripMenuItem"].Enabled = true;
                this.feedsContextMenu.Items["deleteFeedToolStripMenuItem"].Enabled = true;
            }
        }

        private void RssItemGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //TODO: make it check for Downloaded flag!
            if (!(sender is DataGridView))
                return;

            var dgv = (DataGridView)sender;
            var data = dgv.DataSource;

            var graphics = e.Graphics;
            var icon = rssGridImageList.Images[1];

            int xPosition = e.RowBounds.X;
            int yPosition = e.RowBounds.Y + ((dgv.Rows[e.RowIndex].Height - icon.Height) / 2);

            var rectangle = new System.Drawing.Rectangle(xPosition, yPosition, icon.Width, icon.Height);
            graphics.DrawImage(icon, rectangle);
        }

        #endregion [ Events ]

        #region [ Interface Clicks ]

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            frmAddFeed frm = new frmAddFeed();
            frm.FeedImageList = this.feedImageList;
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

                    UpdateFeedTimer.Stop();
                    UpdateFeedTimer.Dispose();
                    UpdateFeedTimer = null;

                    StartFeedUpdateTimer();
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
            //TODO: Add RSS Filters
            //TODO: Create RSS Filter Screen
            //TODO: Create RSS Filter Engine.

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

        private void exitItemNotifyContextMenu_Click(object sender, EventArgs e)
        {
            ExitMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Feed Tree View Menu Strip - Edit Command
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event Args</param>
        private void editFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.FeedsTree.SelectedNode.Name != "FeedsRoot")
            {
                frmAddFeed frm = new frmAddFeed(int.Parse(this.FeedsTree.SelectedNode.Name));
                frm.FeedImageList = this.feedImageList;
                frm.OnClosedForm += OnAddFeedFinished;
                frm.ShowDialog(this);
                frm.Dispose();
                frm = null;
            }
        }

        private void deleteFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.FeedsTree.SelectedNode.Name != "FeedsRoot")
            {
                var msgb = MessageBox.Show(this, "Are you sure you want to delete this Feed?", NAME, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (msgb == DialogResult.OK)
                {
                    dbController.DeleteFeed(this, int.Parse(this.FeedsTree.SelectedNode.Name));
                }
            }
        }

        private void addFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFeed_Click(sender, e);
        }

        private void updateFeedsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadDataGrid();
        }

        #endregion [ Interface Clicks ]

        #region [ Private Methods ]

        private void StartFeedUpdateTimer()
        {
            if (string.IsNullOrEmpty(TorObject.TorUpSeconds))
                return;

            if (dbController.CountFeeds() == 0)
                return;

            if (UpdateFeedTimer == null)
            {
                var time = TimeSpan.FromMinutes(double.Parse(TorObject.TorUpSeconds));
                var totalTime = int.Parse(TorObject.TorUpSeconds) * 60;

                UpdateFeedTimer = new Timer();
                UpdateFeedTimer.Interval = (int)time.TotalMilliseconds;
                UpdateFeedTimer.Tick += (sender, ev) =>
                {
                    totalTime = int.Parse(TorObject.TorUpSeconds) * 60;
                    LoadDataGrid();
                };
                UpdateFeedTimer.Start();

                RegressiveTimer = new Timer();
                RegressiveTimer.Interval = 1000;
                RegressiveTimer.Tick += (sender, ev) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        this.lblTimerUpdate.Text = "Next Feed Update: " + totalTime + " seconds.";
                        this.Update();
                    }));
                    totalTime--;
                };
                RegressiveTimer.Start();

            }
            else
            {
                UpdateFeedTimer.Stop();
                UpdateFeedTimer.Start();
            }
        }

        private void LoadDataGrid()
        {
            var thread = new System.Threading.Thread(() =>
            {
                ChangeNameStatus(" - Downlaoding Feed Updates...");
                dbController.SaveFeedItems();

                this.Invoke(new Action(() =>
                {
                    var data2 = dbController.LoadFeedItems();

                    SortableBindingList<Post> sbl2 = new SortableBindingList<Post>(data2);
                    this.RssItemGrid.DataSource = sbl2;

                    ChangeNameStatus();

                    this.Update();
                }));

            });

            thread.IsBackground = true;
            thread.Name = "Update Feeds";
            thread.Start();

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

        #endregion [ Private Methods ]
    }
}
