namespace Blitzkrieg
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Feeds");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.RssTab = new System.Windows.Forms.TabPage();
            this.RssGroup = new System.Windows.Forms.GroupBox();
            this.btnAddFeed = new System.Windows.Forms.Button();
            this.RssDataContainer = new System.Windows.Forms.SplitContainer();
            this.RssItemGrid = new System.Windows.Forms.DataGridView();
            this.btnAddFilter = new System.Windows.Forms.Button();
            this.btnDelFilter = new System.Windows.Forms.Button();
            this.btnCheckNone = new System.Windows.Forms.Button();
            this.btnCheckAll = new System.Windows.Forms.Button();
            this.RssFilterList = new System.Windows.Forms.CheckedListBox();
            this.FeedsTree = new System.Windows.Forms.TreeView();
            this.feedsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.updateFeedsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feedImageList = new System.Windows.Forms.ImageList(this.components);
            this.uTorTab = new System.Windows.Forms.TabPage();
            this.uTorGroup = new System.Windows.Forms.GroupBox();
            this.btnTorEdit = new System.Windows.Forms.Button();
            this.chkStop100 = new System.Windows.Forms.CheckBox();
            this.chkForceDownload = new System.Windows.Forms.CheckBox();
            this.uTorRefrash = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTorRefresh = new System.Windows.Forms.TextBox();
            this.btnTorSave = new System.Windows.Forms.Button();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UpdFreqGroup = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUpSeconds = new System.Windows.Forms.TextBox();
            this.DnsTab = new System.Windows.Forms.TabPage();
            this.DDNSGroup = new System.Windows.Forms.GroupBox();
            this.btnDnsEdit = new System.Windows.Forms.Button();
            this.UpdDnsFreq = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDnsUpdateMinutes = new System.Windows.Forms.TextBox();
            this.chkUpdDisconnect = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.LogGroup = new System.Windows.Forms.GroupBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.LogListBox = new System.Windows.Forms.ListBox();
            this.btnSaveDns = new System.Windows.Forms.Button();
            this.btnAddDomain = new System.Windows.Forms.Button();
            this.btnRemoveDomain = new System.Windows.Forms.Button();
            this.txtDnsPort = new System.Windows.Forms.TextBox();
            this.txtDnsHost = new System.Windows.Forms.TextBox();
            this.txtDnsAddress = new System.Windows.Forms.TextBox();
            this.txtDnsPass = new System.Windows.Forms.TextBox();
            this.txtDnsLogin = new System.Windows.Forms.TextBox();
            this.DomainListBox = new System.Windows.Forms.CheckedListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitItemNotifyContextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.rssGridImageList = new System.Windows.Forms.ImageList(this.components);
            this.lblTimerUpdate = new System.Windows.Forms.Label();
            this.mainTabs.SuspendLayout();
            this.RssTab.SuspendLayout();
            this.RssGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RssDataContainer)).BeginInit();
            this.RssDataContainer.Panel1.SuspendLayout();
            this.RssDataContainer.Panel2.SuspendLayout();
            this.RssDataContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RssItemGrid)).BeginInit();
            this.feedsContextMenu.SuspendLayout();
            this.uTorTab.SuspendLayout();
            this.uTorGroup.SuspendLayout();
            this.uTorRefrash.SuspendLayout();
            this.UpdFreqGroup.SuspendLayout();
            this.DnsTab.SuspendLayout();
            this.DDNSGroup.SuspendLayout();
            this.UpdDnsFreq.SuspendLayout();
            this.LogGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.notifyContextMenu.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabs
            // 
            this.mainTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabs.Controls.Add(this.RssTab);
            this.mainTabs.Controls.Add(this.uTorTab);
            this.mainTabs.Controls.Add(this.DnsTab);
            this.mainTabs.Location = new System.Drawing.Point(2, 3);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(882, 515);
            this.mainTabs.TabIndex = 0;
            // 
            // RssTab
            // 
            this.RssTab.Controls.Add(this.RssGroup);
            this.RssTab.Location = new System.Drawing.Point(4, 22);
            this.RssTab.Name = "RssTab";
            this.RssTab.Padding = new System.Windows.Forms.Padding(3);
            this.RssTab.Size = new System.Drawing.Size(874, 489);
            this.RssTab.TabIndex = 0;
            this.RssTab.Text = "RSS Feed";
            this.RssTab.UseVisualStyleBackColor = true;
            // 
            // RssGroup
            // 
            this.RssGroup.Controls.Add(this.btnAddFeed);
            this.RssGroup.Controls.Add(this.RssDataContainer);
            this.RssGroup.Controls.Add(this.FeedsTree);
            this.RssGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RssGroup.Location = new System.Drawing.Point(3, 3);
            this.RssGroup.Name = "RssGroup";
            this.RssGroup.Size = new System.Drawing.Size(868, 483);
            this.RssGroup.TabIndex = 0;
            this.RssGroup.TabStop = false;
            this.RssGroup.Text = "RSS Feed Configuration";
            // 
            // btnAddFeed
            // 
            this.btnAddFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFeed.Location = new System.Drawing.Point(7, 457);
            this.btnAddFeed.Name = "btnAddFeed";
            this.btnAddFeed.Size = new System.Drawing.Size(75, 23);
            this.btnAddFeed.TabIndex = 0;
            this.btnAddFeed.Text = "Add Feed";
            this.btnAddFeed.UseVisualStyleBackColor = true;
            this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
            // 
            // RssDataContainer
            // 
            this.RssDataContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RssDataContainer.Location = new System.Drawing.Point(203, 19);
            this.RssDataContainer.Name = "RssDataContainer";
            this.RssDataContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // RssDataContainer.Panel1
            // 
            this.RssDataContainer.Panel1.Controls.Add(this.RssItemGrid);
            // 
            // RssDataContainer.Panel2
            // 
            this.RssDataContainer.Panel2.Controls.Add(this.lblTimerUpdate);
            this.RssDataContainer.Panel2.Controls.Add(this.btnAddFilter);
            this.RssDataContainer.Panel2.Controls.Add(this.btnDelFilter);
            this.RssDataContainer.Panel2.Controls.Add(this.btnCheckNone);
            this.RssDataContainer.Panel2.Controls.Add(this.btnCheckAll);
            this.RssDataContainer.Panel2.Controls.Add(this.RssFilterList);
            this.RssDataContainer.Size = new System.Drawing.Size(659, 467);
            this.RssDataContainer.SplitterDistance = 227;
            this.RssDataContainer.TabIndex = 1;
            // 
            // RssItemGrid
            // 
            this.RssItemGrid.AllowUserToAddRows = false;
            this.RssItemGrid.AllowUserToDeleteRows = false;
            this.RssItemGrid.AllowUserToOrderColumns = true;
            this.RssItemGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.RssItemGrid.BackgroundColor = System.Drawing.SystemColors.Window;
            this.RssItemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RssItemGrid.Location = new System.Drawing.Point(0, 0);
            this.RssItemGrid.Name = "RssItemGrid";
            this.RssItemGrid.ReadOnly = true;
            this.RssItemGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RssItemGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.RssItemGrid.RowHeadersWidth = 20;
            this.RssItemGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.RssItemGrid.Size = new System.Drawing.Size(659, 227);
            this.RssItemGrid.TabIndex = 0;
            this.RssItemGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.RssItemGrid_RowPostPaint);
            // 
            // btnAddFilter
            // 
            this.btnAddFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddFilter.Location = new System.Drawing.Point(581, 207);
            this.btnAddFilter.Name = "btnAddFilter";
            this.btnAddFilter.Size = new System.Drawing.Size(75, 23);
            this.btnAddFilter.TabIndex = 3;
            this.btnAddFilter.Text = "Add Filter";
            this.btnAddFilter.UseVisualStyleBackColor = true;
            this.btnAddFilter.Click += new System.EventHandler(this.btnAddFilter_Click);
            // 
            // btnDelFilter
            // 
            this.btnDelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelFilter.Location = new System.Drawing.Point(500, 207);
            this.btnDelFilter.Name = "btnDelFilter";
            this.btnDelFilter.Size = new System.Drawing.Size(75, 23);
            this.btnDelFilter.TabIndex = 2;
            this.btnDelFilter.Text = "Delete Filter";
            this.btnDelFilter.UseVisualStyleBackColor = true;
            this.btnDelFilter.Click += new System.EventHandler(this.btnDelFilter_Click);
            // 
            // btnCheckNone
            // 
            this.btnCheckNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckNone.Location = new System.Drawing.Point(84, 207);
            this.btnCheckNone.Name = "btnCheckNone";
            this.btnCheckNone.Size = new System.Drawing.Size(75, 23);
            this.btnCheckNone.TabIndex = 1;
            this.btnCheckNone.Text = "Uncheck All";
            this.btnCheckNone.UseVisualStyleBackColor = true;
            this.btnCheckNone.Click += new System.EventHandler(this.btnCheckNone_Click);
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCheckAll.Location = new System.Drawing.Point(3, 207);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAll.TabIndex = 0;
            this.btnCheckAll.Text = "Check All";
            this.btnCheckAll.UseVisualStyleBackColor = true;
            this.btnCheckAll.Click += new System.EventHandler(this.btnCheckAll_Click);
            // 
            // RssFilterList
            // 
            this.RssFilterList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RssFilterList.FormattingEnabled = true;
            this.RssFilterList.Location = new System.Drawing.Point(0, 3);
            this.RssFilterList.Name = "RssFilterList";
            this.RssFilterList.Size = new System.Drawing.Size(659, 199);
            this.RssFilterList.TabIndex = 4;
            // 
            // FeedsTree
            // 
            this.FeedsTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FeedsTree.ContextMenuStrip = this.feedsContextMenu;
            this.FeedsTree.ImageIndex = 0;
            this.FeedsTree.ImageList = this.feedImageList;
            this.FeedsTree.Location = new System.Drawing.Point(7, 19);
            this.FeedsTree.MinimumSize = new System.Drawing.Size(190, 4);
            this.FeedsTree.Name = "FeedsTree";
            treeNode1.Checked = true;
            treeNode1.Name = "FeedsRoot";
            treeNode1.Text = "Feeds";
            this.FeedsTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.FeedsTree.SelectedImageIndex = 0;
            this.FeedsTree.Size = new System.Drawing.Size(190, 432);
            this.FeedsTree.TabIndex = 2;
            // 
            // feedsContextMenu
            // 
            this.feedsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFeedToolStripMenuItem,
            this.editFeedToolStripMenuItem,
            this.deleteFeedToolStripMenuItem,
            this.toolStripSeparator2,
            this.updateFeedsToolStripMenuItem});
            this.feedsContextMenu.Name = "feedsContextMenu";
            this.feedsContextMenu.Size = new System.Drawing.Size(146, 98);
            this.feedsContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.feedsContextMenu_Opening);
            // 
            // addFeedToolStripMenuItem
            // 
            this.addFeedToolStripMenuItem.Name = "addFeedToolStripMenuItem";
            this.addFeedToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.addFeedToolStripMenuItem.Text = "&Add Feed";
            this.addFeedToolStripMenuItem.Click += new System.EventHandler(this.addFeedToolStripMenuItem_Click);
            // 
            // editFeedToolStripMenuItem
            // 
            this.editFeedToolStripMenuItem.Name = "editFeedToolStripMenuItem";
            this.editFeedToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.editFeedToolStripMenuItem.Text = "&Edit Feed";
            this.editFeedToolStripMenuItem.Click += new System.EventHandler(this.editFeedToolStripMenuItem_Click);
            // 
            // deleteFeedToolStripMenuItem
            // 
            this.deleteFeedToolStripMenuItem.Name = "deleteFeedToolStripMenuItem";
            this.deleteFeedToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteFeedToolStripMenuItem.Text = "&Delete Feed";
            this.deleteFeedToolStripMenuItem.Click += new System.EventHandler(this.deleteFeedToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(142, 6);
            // 
            // updateFeedsToolStripMenuItem
            // 
            this.updateFeedsToolStripMenuItem.Name = "updateFeedsToolStripMenuItem";
            this.updateFeedsToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.updateFeedsToolStripMenuItem.Text = "&Update Feeds";
            this.updateFeedsToolStripMenuItem.Click += new System.EventHandler(this.updateFeedsToolStripMenuItem_Click);
            // 
            // feedImageList
            // 
            this.feedImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("feedImageList.ImageStream")));
            this.feedImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.feedImageList.Images.SetKeyName(0, "feedIcon.png");
            // 
            // uTorTab
            // 
            this.uTorTab.Controls.Add(this.uTorGroup);
            this.uTorTab.Location = new System.Drawing.Point(4, 22);
            this.uTorTab.Name = "uTorTab";
            this.uTorTab.Padding = new System.Windows.Forms.Padding(3);
            this.uTorTab.Size = new System.Drawing.Size(874, 489);
            this.uTorTab.TabIndex = 1;
            this.uTorTab.Text = "uTorrent";
            this.uTorTab.UseVisualStyleBackColor = true;
            // 
            // uTorGroup
            // 
            this.uTorGroup.Controls.Add(this.btnTorEdit);
            this.uTorGroup.Controls.Add(this.chkStop100);
            this.uTorGroup.Controls.Add(this.chkForceDownload);
            this.uTorGroup.Controls.Add(this.uTorRefrash);
            this.uTorGroup.Controls.Add(this.btnTorSave);
            this.uTorGroup.Controls.Add(this.txtPass);
            this.uTorGroup.Controls.Add(this.txtUser);
            this.uTorGroup.Controls.Add(this.txtPort);
            this.uTorGroup.Controls.Add(this.txtAddress);
            this.uTorGroup.Controls.Add(this.label4);
            this.uTorGroup.Controls.Add(this.label3);
            this.uTorGroup.Controls.Add(this.label2);
            this.uTorGroup.Controls.Add(this.label1);
            this.uTorGroup.Controls.Add(this.UpdFreqGroup);
            this.uTorGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uTorGroup.Location = new System.Drawing.Point(3, 3);
            this.uTorGroup.Name = "uTorGroup";
            this.uTorGroup.Size = new System.Drawing.Size(868, 483);
            this.uTorGroup.TabIndex = 0;
            this.uTorGroup.TabStop = false;
            this.uTorGroup.Text = "uTorrent Web Configuration";
            // 
            // btnTorEdit
            // 
            this.btnTorEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTorEdit.Location = new System.Drawing.Point(703, 457);
            this.btnTorEdit.Name = "btnTorEdit";
            this.btnTorEdit.Size = new System.Drawing.Size(75, 23);
            this.btnTorEdit.TabIndex = 13;
            this.btnTorEdit.Text = "Edit";
            this.btnTorEdit.UseVisualStyleBackColor = true;
            this.btnTorEdit.Click += new System.EventHandler(this.btnTorEdit_Click);
            // 
            // chkStop100
            // 
            this.chkStop100.AutoSize = true;
            this.chkStop100.Location = new System.Drawing.Point(6, 149);
            this.chkStop100.Name = "chkStop100";
            this.chkStop100.Size = new System.Drawing.Size(106, 17);
            this.chkStop100.TabIndex = 6;
            this.chkStop100.Text = "Stop when 100%";
            this.chkStop100.UseVisualStyleBackColor = true;
            // 
            // chkForceDownload
            // 
            this.chkForceDownload.AutoSize = true;
            this.chkForceDownload.Location = new System.Drawing.Point(6, 126);
            this.chkForceDownload.Name = "chkForceDownload";
            this.chkForceDownload.Size = new System.Drawing.Size(215, 17);
            this.chkForceDownload.TabIndex = 5;
            this.chkForceDownload.Text = "uTorrent Force Download (Ignore Limits)";
            this.chkForceDownload.UseVisualStyleBackColor = true;
            // 
            // uTorRefrash
            // 
            this.uTorRefrash.Controls.Add(this.label16);
            this.uTorRefrash.Controls.Add(this.label17);
            this.uTorRefrash.Controls.Add(this.txtTorRefresh);
            this.uTorRefrash.Location = new System.Drawing.Point(9, 183);
            this.uTorRefrash.Name = "uTorRefrash";
            this.uTorRefrash.Size = new System.Drawing.Size(400, 68);
            this.uTorRefrash.TabIndex = 7;
            this.uTorRefrash.TabStop = false;
            this.uTorRefrash.Text = "uTorrent Refresh Rate";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(182, 28);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "seconds.";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(98, 13);
            this.label17.TabIndex = 1;
            this.label17.Text = "Update Frequency:";
            // 
            // txtTorRefresh
            // 
            this.txtTorRefresh.Location = new System.Drawing.Point(110, 25);
            this.txtTorRefresh.Name = "txtTorRefresh";
            this.txtTorRefresh.Size = new System.Drawing.Size(66, 20);
            this.txtTorRefresh.TabIndex = 0;
            // 
            // btnTorSave
            // 
            this.btnTorSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTorSave.Location = new System.Drawing.Point(784, 457);
            this.btnTorSave.Name = "btnTorSave";
            this.btnTorSave.Size = new System.Drawing.Size(75, 23);
            this.btnTorSave.TabIndex = 12;
            this.btnTorSave.Text = "Save";
            this.btnTorSave.UseVisualStyleBackColor = true;
            this.btnTorSave.Click += new System.EventHandler(this.btnTorSave_Click);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(70, 100);
            this.txtPass.Name = "txtPass";
            this.txtPass.PasswordChar = '*';
            this.txtPass.Size = new System.Drawing.Size(146, 20);
            this.txtPass.TabIndex = 4;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(70, 74);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(146, 20);
            this.txtUser.TabIndex = 3;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(70, 48);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(66, 20);
            this.txtPort.TabIndex = 2;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(70, 22);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(339, 20);
            this.txtAddress.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Address:";
            // 
            // UpdFreqGroup
            // 
            this.UpdFreqGroup.Controls.Add(this.label6);
            this.UpdFreqGroup.Controls.Add(this.label5);
            this.UpdFreqGroup.Controls.Add(this.txtUpSeconds);
            this.UpdFreqGroup.Location = new System.Drawing.Point(9, 279);
            this.UpdFreqGroup.Name = "UpdFreqGroup";
            this.UpdFreqGroup.Size = new System.Drawing.Size(400, 68);
            this.UpdFreqGroup.TabIndex = 8;
            this.UpdFreqGroup.TabStop = false;
            this.UpdFreqGroup.Text = "RSS Update Frequency";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "minutes.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Update Frequency:";
            // 
            // txtUpSeconds
            // 
            this.txtUpSeconds.Location = new System.Drawing.Point(110, 25);
            this.txtUpSeconds.Name = "txtUpSeconds";
            this.txtUpSeconds.Size = new System.Drawing.Size(66, 20);
            this.txtUpSeconds.TabIndex = 0;
            // 
            // DnsTab
            // 
            this.DnsTab.Controls.Add(this.DDNSGroup);
            this.DnsTab.Location = new System.Drawing.Point(4, 22);
            this.DnsTab.Name = "DnsTab";
            this.DnsTab.Padding = new System.Windows.Forms.Padding(3);
            this.DnsTab.Size = new System.Drawing.Size(874, 489);
            this.DnsTab.TabIndex = 2;
            this.DnsTab.Text = "DNS";
            this.DnsTab.UseVisualStyleBackColor = true;
            // 
            // DDNSGroup
            // 
            this.DDNSGroup.Controls.Add(this.btnDnsEdit);
            this.DDNSGroup.Controls.Add(this.UpdDnsFreq);
            this.DDNSGroup.Controls.Add(this.label13);
            this.DDNSGroup.Controls.Add(this.LogGroup);
            this.DDNSGroup.Controls.Add(this.btnSaveDns);
            this.DDNSGroup.Controls.Add(this.btnAddDomain);
            this.DDNSGroup.Controls.Add(this.btnRemoveDomain);
            this.DDNSGroup.Controls.Add(this.txtDnsPort);
            this.DDNSGroup.Controls.Add(this.txtDnsHost);
            this.DDNSGroup.Controls.Add(this.txtDnsAddress);
            this.DDNSGroup.Controls.Add(this.txtDnsPass);
            this.DDNSGroup.Controls.Add(this.txtDnsLogin);
            this.DDNSGroup.Controls.Add(this.DomainListBox);
            this.DDNSGroup.Controls.Add(this.label12);
            this.DDNSGroup.Controls.Add(this.label11);
            this.DDNSGroup.Controls.Add(this.label10);
            this.DDNSGroup.Controls.Add(this.label9);
            this.DDNSGroup.Controls.Add(this.label8);
            this.DDNSGroup.Controls.Add(this.label7);
            this.DDNSGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DDNSGroup.Location = new System.Drawing.Point(3, 3);
            this.DDNSGroup.Name = "DDNSGroup";
            this.DDNSGroup.Size = new System.Drawing.Size(868, 483);
            this.DDNSGroup.TabIndex = 0;
            this.DDNSGroup.TabStop = false;
            this.DDNSGroup.Text = "Dynamic DNS Client Configuration";
            // 
            // btnDnsEdit
            // 
            this.btnDnsEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDnsEdit.Location = new System.Drawing.Point(703, 460);
            this.btnDnsEdit.Name = "btnDnsEdit";
            this.btnDnsEdit.Size = new System.Drawing.Size(75, 23);
            this.btnDnsEdit.TabIndex = 18;
            this.btnDnsEdit.Text = "Edit";
            this.btnDnsEdit.UseVisualStyleBackColor = true;
            // 
            // UpdDnsFreq
            // 
            this.UpdDnsFreq.Controls.Add(this.label14);
            this.UpdDnsFreq.Controls.Add(this.label15);
            this.UpdDnsFreq.Controls.Add(this.txtDnsUpdateMinutes);
            this.UpdDnsFreq.Controls.Add(this.chkUpdDisconnect);
            this.UpdDnsFreq.Location = new System.Drawing.Point(6, 182);
            this.UpdDnsFreq.Name = "UpdDnsFreq";
            this.UpdDnsFreq.Size = new System.Drawing.Size(305, 85);
            this.UpdDnsFreq.TabIndex = 17;
            this.UpdDnsFreq.TabStop = false;
            this.UpdDnsFreq.Text = "DNS Update Frequency";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(182, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "minutes.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(98, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "Update Frequency:";
            // 
            // txtDnsUpdateMinutes
            // 
            this.txtDnsUpdateMinutes.Location = new System.Drawing.Point(110, 49);
            this.txtDnsUpdateMinutes.Name = "txtDnsUpdateMinutes";
            this.txtDnsUpdateMinutes.Size = new System.Drawing.Size(66, 20);
            this.txtDnsUpdateMinutes.TabIndex = 5;
            // 
            // chkUpdDisconnect
            // 
            this.chkUpdDisconnect.AutoSize = true;
            this.chkUpdDisconnect.Location = new System.Drawing.Point(7, 20);
            this.chkUpdDisconnect.Name = "chkUpdDisconnect";
            this.chkUpdDisconnect.Size = new System.Drawing.Size(183, 17);
            this.chkUpdDisconnect.TabIndex = 0;
            this.chkUpdDisconnect.Text = "Update Only when Disconnected";
            this.chkUpdDisconnect.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 102);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(326, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "i.e: /RemoteUpdate.sv?login={login}&password={pass}&host={host}";
            this.label13.UseMnemonic = false;
            // 
            // LogGroup
            // 
            this.LogGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogGroup.Controls.Add(this.btnClearLog);
            this.LogGroup.Controls.Add(this.LogListBox);
            this.LogGroup.Location = new System.Drawing.Point(338, 19);
            this.LogGroup.Name = "LogGroup";
            this.LogGroup.Size = new System.Drawing.Size(521, 436);
            this.LogGroup.TabIndex = 15;
            this.LogGroup.TabStop = false;
            this.LogGroup.Text = "DNS Log";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(443, 407);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(75, 23);
            this.btnClearLog.TabIndex = 16;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // LogListBox
            // 
            this.LogListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogListBox.FormattingEnabled = true;
            this.LogListBox.Location = new System.Drawing.Point(7, 20);
            this.LogListBox.Name = "LogListBox";
            this.LogListBox.Size = new System.Drawing.Size(511, 381);
            this.LogListBox.TabIndex = 0;
            // 
            // btnSaveDns
            // 
            this.btnSaveDns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveDns.Location = new System.Drawing.Point(784, 460);
            this.btnSaveDns.Name = "btnSaveDns";
            this.btnSaveDns.Size = new System.Drawing.Size(75, 23);
            this.btnSaveDns.TabIndex = 14;
            this.btnSaveDns.Text = "Save";
            this.btnSaveDns.UseVisualStyleBackColor = true;
            this.btnSaveDns.Click += new System.EventHandler(this.btnSaveDns_Click);
            // 
            // btnAddDomain
            // 
            this.btnAddDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDomain.Location = new System.Drawing.Point(155, 460);
            this.btnAddDomain.Name = "btnAddDomain";
            this.btnAddDomain.Size = new System.Drawing.Size(75, 23);
            this.btnAddDomain.TabIndex = 13;
            this.btnAddDomain.Text = "Add Domain";
            this.btnAddDomain.UseVisualStyleBackColor = true;
            this.btnAddDomain.Click += new System.EventHandler(this.btnAddDomain_Click);
            // 
            // btnRemoveDomain
            // 
            this.btnRemoveDomain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveDomain.Location = new System.Drawing.Point(236, 460);
            this.btnRemoveDomain.Name = "btnRemoveDomain";
            this.btnRemoveDomain.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveDomain.TabIndex = 12;
            this.btnRemoveDomain.Text = "Remove";
            this.btnRemoveDomain.UseVisualStyleBackColor = true;
            this.btnRemoveDomain.Click += new System.EventHandler(this.btnRemoveDomain_Click);
            // 
            // txtDnsPort
            // 
            this.txtDnsPort.Location = new System.Drawing.Point(70, 53);
            this.txtDnsPort.Name = "txtDnsPort";
            this.txtDnsPort.Size = new System.Drawing.Size(56, 20);
            this.txtDnsPort.TabIndex = 11;
            // 
            // txtDnsHost
            // 
            this.txtDnsHost.Location = new System.Drawing.Point(70, 27);
            this.txtDnsHost.Name = "txtDnsHost";
            this.txtDnsHost.Size = new System.Drawing.Size(241, 20);
            this.txtDnsHost.TabIndex = 10;
            // 
            // txtDnsAddress
            // 
            this.txtDnsAddress.Location = new System.Drawing.Point(70, 77);
            this.txtDnsAddress.Name = "txtDnsAddress";
            this.txtDnsAddress.Size = new System.Drawing.Size(241, 20);
            this.txtDnsAddress.TabIndex = 9;
            // 
            // txtDnsPass
            // 
            this.txtDnsPass.Location = new System.Drawing.Point(67, 156);
            this.txtDnsPass.Name = "txtDnsPass";
            this.txtDnsPass.Size = new System.Drawing.Size(112, 20);
            this.txtDnsPass.TabIndex = 8;
            // 
            // txtDnsLogin
            // 
            this.txtDnsLogin.Location = new System.Drawing.Point(67, 130);
            this.txtDnsLogin.Name = "txtDnsLogin";
            this.txtDnsLogin.Size = new System.Drawing.Size(112, 20);
            this.txtDnsLogin.TabIndex = 7;
            // 
            // DomainListBox
            // 
            this.DomainListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DomainListBox.FormattingEnabled = true;
            this.DomainListBox.Location = new System.Drawing.Point(6, 286);
            this.DomainListBox.Name = "DomainListBox";
            this.DomainListBox.Size = new System.Drawing.Size(305, 169);
            this.DomainListBox.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 270);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Domain:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Password:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Username:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Address:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Port:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Host:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyContextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Blitzkrieg";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyContextMenu
            // 
            this.notifyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.exitItemNotifyContextMenu});
            this.notifyContextMenu.Name = "notifyContextMenu";
            this.notifyContextMenu.Size = new System.Drawing.Size(93, 32);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(89, 6);
            // 
            // exitItemNotifyContextMenu
            // 
            this.exitItemNotifyContextMenu.Name = "exitItemNotifyContextMenu";
            this.exitItemNotifyContextMenu.Size = new System.Drawing.Size(92, 22);
            this.exitItemNotifyContextMenu.Text = "&Exit";
            this.exitItemNotifyContextMenu.Click += new System.EventHandler(this.exitItemNotifyContextMenu_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(884, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.ExitMenuItem.Text = "&Exit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.mainTabs);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(884, 518);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(884, 542);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // rssGridImageList
            // 
            this.rssGridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("rssGridImageList.ImageStream")));
            this.rssGridImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.rssGridImageList.Images.SetKeyName(0, "checked.png");
            this.rssGridImageList.Images.SetKeyName(1, "pending.png");
            // 
            // lblTimerUpdate
            // 
            this.lblTimerUpdate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTimerUpdate.AutoSize = true;
            this.lblTimerUpdate.Location = new System.Drawing.Point(248, 212);
            this.lblTimerUpdate.Name = "lblTimerUpdate";
            this.lblTimerUpdate.Size = new System.Drawing.Size(158, 13);
            this.lblTimerUpdate.TabIndex = 3;
            this.lblTimerUpdate.Text = "Next Feed Update: 60 seconds.";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 542);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(900, 580);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blitzkrieg";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.mainTabs.ResumeLayout(false);
            this.RssTab.ResumeLayout(false);
            this.RssGroup.ResumeLayout(false);
            this.RssDataContainer.Panel1.ResumeLayout(false);
            this.RssDataContainer.Panel2.ResumeLayout(false);
            this.RssDataContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RssDataContainer)).EndInit();
            this.RssDataContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RssItemGrid)).EndInit();
            this.feedsContextMenu.ResumeLayout(false);
            this.uTorTab.ResumeLayout(false);
            this.uTorGroup.ResumeLayout(false);
            this.uTorGroup.PerformLayout();
            this.uTorRefrash.ResumeLayout(false);
            this.uTorRefrash.PerformLayout();
            this.UpdFreqGroup.ResumeLayout(false);
            this.UpdFreqGroup.PerformLayout();
            this.DnsTab.ResumeLayout(false);
            this.DDNSGroup.ResumeLayout(false);
            this.DDNSGroup.PerformLayout();
            this.UpdDnsFreq.ResumeLayout(false);
            this.UpdDnsFreq.PerformLayout();
            this.LogGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.notifyContextMenu.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabs;
        private System.Windows.Forms.TabPage uTorTab;
        private System.Windows.Forms.TabPage DnsTab;
        private System.Windows.Forms.TabPage RssTab;
        private System.Windows.Forms.GroupBox RssGroup;
        private System.Windows.Forms.GroupBox uTorGroup;
        private System.Windows.Forms.GroupBox DDNSGroup;
        private System.Windows.Forms.SplitContainer RssDataContainer;
        private System.Windows.Forms.TreeView FeedsTree;
        private System.Windows.Forms.CheckedListBox RssFilterList;
        private System.Windows.Forms.Button btnAddFilter;
        private System.Windows.Forms.Button btnDelFilter;
        private System.Windows.Forms.Button btnCheckNone;
        private System.Windows.Forms.Button btnCheckAll;
        private System.Windows.Forms.DataGridView RssItemGrid;
        private System.Windows.Forms.GroupBox UpdFreqGroup;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUpSeconds;
        private System.Windows.Forms.Button btnTorSave;
        private System.Windows.Forms.Button btnSaveDns;
        private System.Windows.Forms.Button btnAddDomain;
        private System.Windows.Forms.Button btnRemoveDomain;
        private System.Windows.Forms.TextBox txtDnsPort;
        private System.Windows.Forms.TextBox txtDnsHost;
        private System.Windows.Forms.TextBox txtDnsAddress;
        private System.Windows.Forms.TextBox txtDnsPass;
        private System.Windows.Forms.TextBox txtDnsLogin;
        private System.Windows.Forms.CheckedListBox DomainListBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox LogGroup;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.ListBox LogListBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox UpdDnsFreq;
        private System.Windows.Forms.CheckBox chkUpdDisconnect;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDnsUpdateMinutes;
        private System.Windows.Forms.GroupBox uTorRefrash;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTorRefresh;
        private System.Windows.Forms.Button btnAddFeed;
        private System.Windows.Forms.CheckBox chkStop100;
        private System.Windows.Forms.CheckBox chkForceDownload;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.Button btnDnsEdit;
        public System.Windows.Forms.Button btnTorEdit;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitItemNotifyContextMenu;
        private System.Windows.Forms.ContextMenuStrip feedsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem addFeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editFeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem updateFeedsToolStripMenuItem;
        public System.Windows.Forms.ImageList feedImageList;
        private System.Windows.Forms.ImageList rssGridImageList;
        private System.Windows.Forms.Label lblTimerUpdate;
    }
}

