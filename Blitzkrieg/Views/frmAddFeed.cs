using Blitzkrieg.Connection;
using Blitzkrieg.Controllers;
using Blitzkrieg.DataBase;
using System;
using System.Windows.Forms;

namespace Blitzkrieg.Views
{
    public partial class frmAddFeed : Form
    {
        private DatabaseController dbController = null;
        private DownloadFeed feedDownloader = null;

        public EventHandler OnClosedForm;

        public ImageList FeedImageList { get; set; }
        public int FeedIndex { get; private set; }
        public string FeedAlias { get; private set; }
        public string FeedAddress { get; private set; }
        public int FeedPriority { get; set; }
        public bool IsActive { get; private set; }
        public string FeedIcon { get; private set; }
        public RssFeedObject RssObject { get; set; }

        public frmAddFeed()
        {
            FeedIndex = -1;
            InitializeComponent();
        }

        public frmAddFeed(int indexFeed)
        {
            FeedIndex = indexFeed;
            InitializeComponent();
        }

        private void btnEditFeedSave_Click(object sender, EventArgs e)
        {
            FeedAddress = txtAddress.Text;
            FeedAlias = txtAlias.Text;
            FeedPriority = Convert.ToInt32(cboPriority.SelectedItem.ToString());
            IsActive = chkIsActive.Checked;

            if (!string.IsNullOrEmpty(FeedAddress))
            {
                Uri uriResult;
                bool result = Uri.TryCreate(FeedAddress, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!result)
                {
                    errorProvider.SetError(txtAddress, "Invalid URL. Please check.");
                    return;
                }
            }

            if (string.IsNullOrEmpty(FeedAlias))
            {
                errorProvider.SetError(txtAlias, "You must inform a Feed Alias.");
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(this.FeedIcon))
                {
                    feedDownloader = new DownloadFeed();
                    this.FeedIcon = feedDownloader.DownloadFavicon(FeedAddress);
                }

                dbController = new DatabaseController();
                dbController.SaveFeeds(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
                        
            this.Close();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.OnClosedForm != null)
                OnClosedForm(sender, null);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.txtAddress.Enabled = true;

            RssObject = new RssFeedObject();
            dbController = new DatabaseController();

            int countFeed = dbController.CountFeeds();
            if (countFeed == 0)
                this.cboPriority.Items.Add(1);
            else
            {
                for (int i = 0; i <= countFeed; i++)
                    this.cboPriority.Items.Add(i + 1);
            }
            this.cboPriority.SelectedIndex = this.cboPriority.Items.Count - 1;

            if (FeedIndex > -1)
            {
                dbController.LoadSingleFeed(this, FeedIndex);
                this.txtAddress.Enabled = false;
            }

            //Rss Feed DataBind
            this.txtAddress.DataBindings.Add(new Binding("Text", RssObject, "RssAddress"));
            this.txtAlias.DataBindings.Add(new Binding("Text", RssObject, "RssAlias"));
            this.chkIsActive.DataBindings.Add(new Binding("Checked", RssObject, "RssActive"));
            this.cboPriority.DataBindings.Add(new Binding("SelectedIndex", RssObject, "RssPriority"));
            this.FeedIcon = RssObject.RssFeedIcon;
        }
    }
}
