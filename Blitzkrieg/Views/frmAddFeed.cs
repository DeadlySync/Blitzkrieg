using Blitzkrieg.DataBase;
using System;
using System.Windows.Forms;

namespace Blitzkrieg.Views
{
    public partial class frmAddFeed : Form
    {
        public EventHandler OnClosedForm;

        public frmAddFeed()
        {
            InitializeComponent();
        }

        private void btnEditFeedSave_Click(object sender, EventArgs e)
        {
            string url = string.Empty;

            if (!string.IsNullOrEmpty(txtAddress.Text))
            {
                Uri uriResult;
                bool result = Uri.TryCreate(txtAddress.Text, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

                if (!result)
                {
                    errorProvider.SetError(txtAddress, "Invalid URL. Please check.");
                    return;
                }
                else
                    url = txtAddress.Text;
            }

            if (string.IsNullOrEmpty(txtAlias.Text))
            {
                errorProvider.SetError(txtAlias, "You must inform a Feed Alias.");
                return;
            }

            using (var entities = new dataEntities())
            {
                var feed = new RssFeeds();

                if (!string.IsNullOrEmpty(txtAlias.Text))
                    feed.FeedAlias = txtAlias.Text;

                feed.FeedUrl = url;
                feed.IsActive = chkIsActive.Checked;
                feed.DateAdd = DateTime.Now;

                entities.RssFeeds.Add(feed);
                entities.SaveChanges();
            }

            this.Close();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.OnClosedForm != null)
                OnClosedForm(sender, null);
        }
    }
}
