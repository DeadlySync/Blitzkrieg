using Blitzkrieg.DataBase;
using Blitzkrieg.Views;
using System;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace App
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
        }
        private void OnLoad(object sender, EventArgs e)
        {
            this.Text += " - Loading...";
            this.Update();
            LoadSystem();
            LoadFeedTree();
            LoadTorConfig();
        }

        private void btnAddFeed_Click(object sender, EventArgs e)
        {
            frmAddFeed frm = new frmAddFeed();
            frm.OnClosedForm += OnAddFeedFinished;
            frm.ShowDialog(this);
        }

        private void OnAddFeedFinished(object sender, EventArgs e)
        {
            LoadFeedTree();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                using (var context = new dataEntities())
                {
                    var conf = context.UserConfig.OrderByDescending(c => c.DateAdd).FirstOrDefault();
                    context.UserConfig.Remove(conf);
                    context.SaveChanges();

                    conf.IsMaximized = this.WindowState == FormWindowState.Maximized;
                    conf.RssFeedTreeWidth = this.FeedsTree.Width;
                    conf.ScreenHeight = this.Height;
                    conf.ScreenWidth = this.Width;
                    conf.DateUpdate = DateTime.Now;

                    context.UserConfig.Add(conf);
                    context.SaveChanges();
                }
            }
            catch
            {
                //does nothing.
                //i am exiting, after all...              
            }
        }

        private void LoadFeedTree()
        {
            this.FeedsTree.Nodes[0].Nodes.Clear();
            var thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    using (var context = new dataEntities())
                    {
                        var feeds = context.RssFeeds.OrderBy(f => f.FeedAlias).ToList();

                        if (feeds == null)
                            return;

                        this.Invoke(new Action(() =>
                        {
                            this.Text = "Blitzkrieg";

                            foreach (var item in feeds)
                            {
                                this.FeedsTree.Nodes[0].Nodes.Add(item.FeedAlias);
                            }

                            this.FeedsTree.ExpandAll();

                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));

            thread.Name = "Load Feeds";
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoadSystem()
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    using (var context = new dataEntities())
                    {
                        var conf = context.UserConfig.OrderByDescending(c => c.DateAdd).FirstOrDefault();

                        if (conf == null)
                            return;

                        this.Invoke(new Action(() =>
                        {
                            this.WindowState = conf.IsMaximized == true ? FormWindowState.Maximized : FormWindowState.Normal;

                            if (conf.RssFeedTreeWidth != null)
                                this.FeedsTree.Width = (int)conf.RssFeedTreeWidth;

                            if (conf.ScreenHeight != null)
                                this.Height = (int)conf.ScreenHeight;

                            if (conf.ScreenWidth != null)
                                this.Width = (int)conf.ScreenWidth;

                            this.Text = "Blitzkrieg";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));

            thread.Name = "Load User Config";
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoadTorConfig()
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    using (var context = new dataEntities())
                    {
                        var torConfig = context.TorrentClient.OrderByDescending(c => c.DateAdd).FirstOrDefault();

                        if (torConfig == null)
                            return;

                        this.Invoke(new Action(() =>
                        {
                            txtAddress.Text = torConfig.Address;
                            txtPort.Text = torConfig.Port.ToString();
                            txtUser.Text = torConfig.Username;
                            txtPass.Text = torConfig.Password;
                            chkForceDownload.Checked = torConfig.IsForceDown.Value;
                            chkStop100.Checked = torConfig.IsStop100.Value;
                            txtUpSeconds.Text = torConfig.UpdateRate.ToString();
                            txtTorRefresh.Text = torConfig.RefreshRate.ToString();

                            this.Text = "Blitzkrieg";
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));

            thread.Name = "Load uTorrent Config";
            thread.IsBackground = true;
            thread.Start();
        }

        private void btnTorSave_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            var error = false;
            var address = txtAddress.Text;
            var port = txtPort.Text;
            var user = txtUser.Text;
            var pass = txtPass.Text;
            var update = txtUpSeconds.Text;
            var refresh = txtTorRefresh.Text;

            string url = string.Empty;
            string fullUri = string.Empty;

            if (string.IsNullOrEmpty(address))
            {
                errorProvider.SetError(txtAddress, "You must inform an Address.");
                error = true;
            }

            if (string.IsNullOrEmpty(port))
            {
                errorProvider.SetError(txtPort, "You must inform an Connection Port.");
                error = true;
            }
            else
            {
                int exit = 0;
                if (!Int32.TryParse(port, out exit))
                {
                    errorProvider.SetError(txtPort, "Not a number.");
                    error = true;
                }
            }

            if (string.IsNullOrEmpty(user))
            {
                errorProvider.SetError(txtUser, "You must inform an Username.");
                error = true;
            }

            if (string.IsNullOrEmpty(pass))
            {
                errorProvider.SetError(txtPass, "You must inform a Password.");
                error = true;
            }

            if (string.IsNullOrEmpty(update))
            {
                errorProvider.SetError(txtUpSeconds, "You must inform a RSS Update Frequency.");
                error = true;
            }
            else
            {
                int exit = 0;
                if (!Int32.TryParse(update, out exit))
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

            if (string.IsNullOrEmpty(refresh))
            {
                errorProvider.SetError(txtTorRefresh, "You must inform a Refresh Rate for uTorrent.");
                error = true;
            }
            else
            {
                int exit = 0;
                if (!Int32.TryParse(refresh, out exit))
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

            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(txtPort.Text))
            {
                if (address.StartsWith("http://"))
                    error = false;
                else
                {
                    if (address.StartsWith("https://"))
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
                

                if (address.StartsWith("http://"))
                {
                    fullUri = "http://" + user + ":" + pass + "@" + address.Substring(0, 7) + ":" + port + "/gui";
                }
                else if (address.StartsWith("https://"))
                {
                    fullUri = "https://" + user + ":" + pass + "@" + address.Substring(0, 7) + ":" + port + "/gui";
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
                    url = fullUri;
            }

            using (var context = new dataEntities())
            {
                this.Text += " - Saving...";
                this.Update();

                var torConfig = context.TorrentClient.OrderByDescending(t => t.DateAdd).FirstOrDefault();

                if (torConfig == null)
                {
                    torConfig = new TorrentClient();
                    torConfig.DateAdd = DateTime.Now;
                }
                else
                {
                    context.TorrentClient.Remove(torConfig);
                    context.SaveChanges();

                    torConfig.DateUpdate = DateTime.Now;
                }

                torConfig.FullUri = url;
                torConfig.Address = address;
                torConfig.Port = Convert.ToInt64(port);
                torConfig.Username = user;
                torConfig.Password = pass;
                torConfig.IsForceDown = chkForceDownload.Checked;
                torConfig.IsStop100 = chkStop100.Checked;
                torConfig.UpdateRate = Convert.ToInt64(update);
                torConfig.RefreshRate = Convert.ToInt64(refresh);

                context.TorrentClient.Add(torConfig);
                context.SaveChanges();
            }

            this.Text = "Blitzkrieg";
            this.Update();
        }
    }
}
