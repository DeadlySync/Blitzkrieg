using Blitzkrieg.DataBase;
using Blitzkrieg.Views;
using System;
using System.Linq;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace Blitzkrieg.Controllers
{
    public class DatabaseController
    {
        private static SecureString gstrKey = null;
        private static CryptController crypt = null;

        private static readonly string NAME = "Blitzkrieg";

        public void SaveUserConfig(frmMain parentForm)
        {
            try
            {
                using (var context = new dataEntities())
                {
                    bool isNew = false;
                    var conf = context.UserConfig.OrderByDescending(c => c.DateAdd).FirstOrDefault();

                    if (conf == null)
                    {
                        conf = new UserConfig();
                        conf.DateAdd = DateTime.Now;
                        isNew = true;
                    }
                    else
                        conf.DateUpdate = DateTime.Now;

                    conf.IsMaximized = parentForm.WindowState == FormWindowState.Maximized;
                    conf.RssFeedTreeWidth = parentForm.RssTreeView.Width;
                    conf.ScreenHeight = parentForm.Height;
                    conf.ScreenWidth = parentForm.Width;

                    if (isNew)
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

        public void SaveTorConfig(frmMain parentForm)
        {
            if (crypt == null)
                throw new Exception("User not logged on.");

            parentForm.MainErrorProvider.Clear();
            var error = false;
            var address = parentForm.TorAddress.Text;
            var port = parentForm.TorPort.Text;
            var user = parentForm.TorUser.Text;
            var pass = parentForm.TorPass.Text;
            var update = parentForm.TorUpSeconds.Text;
            var refresh = parentForm.TorRefresh.Text;

            string url = string.Empty;
            string fullUri = string.Empty;

            #region [ Data validation ]

            if (string.IsNullOrEmpty(address))
            {
                parentForm.MainErrorProvider.SetError(parentForm.TorAddress, "You must inform an Address.");
                error = true;
            }

            if (string.IsNullOrEmpty(port))
            {
                parentForm.MainErrorProvider.SetError(parentForm.TorPort, "You must inform an Connection Port.");
                error = true;
            }
            else
            {
                int exit = 0;
                if (!Int32.TryParse(port, out exit))
                {
                    parentForm.MainErrorProvider.SetError(parentForm.TorPort, "Not a number.");
                    error = true;
                }
            }

            if (string.IsNullOrEmpty(user))
            {
                parentForm.MainErrorProvider.SetError(parentForm.TorUser, "You must inform an Username.");
                error = true;
            }

            if (string.IsNullOrEmpty(pass))
            {
                parentForm.MainErrorProvider.SetError(parentForm.TorPass, "You must inform a Password.");
                error = true;
            }

            if (string.IsNullOrEmpty(update))
            {
                parentForm.MainErrorProvider.SetError(parentForm.TorUpSeconds, "You must inform a RSS Update Frequency.");
                error = true;
            }
            else
            {
                int exit = 0;
                if (!Int32.TryParse(update, out exit))
                {
                    parentForm.MainErrorProvider.SetError(parentForm.TorUpSeconds, "Not a number.");
                    error = true;
                }
                else
                {
                    if (exit < 1)
                    {
                        parentForm.MainErrorProvider.SetError(parentForm.TorUpSeconds, "Minimum acceptable value is 1 (one) minute.");
                        error = true;
                    }
                }
            }

            if (string.IsNullOrEmpty(refresh))
            {
                parentForm.MainErrorProvider.SetError(parentForm.TorRefresh, "You must inform a Refresh Rate for uTorrent.");
                error = true;
            }
            else
            {
                int exit = 0;
                if (!Int32.TryParse(refresh, out exit))
                {
                    parentForm.MainErrorProvider.SetError(parentForm.TorRefresh, "Not a number.");
                    error = true;
                }
                else
                {
                    if (exit < 5)
                    {
                        parentForm.MainErrorProvider.SetError(parentForm.TorRefresh, "Minimum acceptable value is 5 (five) seconds.");
                        error = true;
                    }
                }
            }

            if (error)
                return;

            if (!string.IsNullOrEmpty(address) && !string.IsNullOrEmpty(port))
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
                        parentForm.MainErrorProvider.SetError(parentForm.TorAddress, "You must inform http:// or https://");
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
                    parentForm.MainErrorProvider.SetError(parentForm.TorAddress, "Invalid URL. Please check.");
                    return;
                }
                else
                    url = fullUri;
            }

            #endregion [ Data validation ]

            using (var context = new dataEntities())
            {
                parentForm.Text += " - Saving...";
                parentForm.Update();

                bool isNew = false;
                var torConfig = context.TorrentClient.OrderByDescending(t => t.DateAdd).FirstOrDefault();

                if (torConfig == null)
                {
                    torConfig = new TorrentClient();
                    torConfig.DateAdd = DateTime.Now;
                    isNew = true;
                }
                else
                    torConfig.DateUpdate = DateTime.Now;
                //TODO: check how to handle "save" misspress and "update";

                torConfig.FullUri = crypt.Encrypt(url, gstrKey);
                torConfig.Address = crypt.Encrypt(address, gstrKey);
                torConfig.Port = Convert.ToInt64(port);
                torConfig.Username = crypt.Encrypt(user, gstrKey);
                torConfig.Password = crypt.Encrypt(pass, gstrKey);
                torConfig.IsForceDown = parentForm.TorChkForceDown.Checked;
                torConfig.IsStop100 = parentForm.TorChkStop100.Checked;
                torConfig.UpdateRate = Convert.ToInt64(update);
                torConfig.RefreshRate = Convert.ToInt64(refresh);

                if (isNew)
                    context.TorrentClient.Add(torConfig);

                context.SaveChanges();
            }

            parentForm.Text = NAME;
            parentForm.Update();
        }

        public void SavePassword(string hash)
        {
            using (var context = new dataEntities())
            {
                var conf = context.UserConfig.OrderByDescending(c => c.DateAdd).FirstOrDefault();

                if (conf == null)
                {
                    conf = new UserConfig();

                    conf.Password = hash;
                    conf.DateAdd = DateTime.Now;

                    context.UserConfig.Add(conf);
                    context.SaveChanges();
                    return;
                }

                conf.Password = hash;
                conf.DateUpdate = DateTime.Now;

                context.SaveChanges();
            }
        }

        public void SaveFeeds(frmAddFeed feedForm)
        {
            if (crypt == null)
                throw new Exception("User not logged on.");

            using (var entities = new dataEntities())
            {
                var feed = new RssFeeds();

                feed.FeedAlias = crypt.Encrypt(feedForm.FeedAlias, gstrKey);
                feed.FeedUrl = crypt.Encrypt(feedForm.FeedAddress, gstrKey);
                feed.IsActive = feedForm.IsActive;
                feed.FeedPriority = feedForm.FeedPriority;
                feed.DateAdd = DateTime.Now;

                entities.RssFeeds.Add(feed);
                entities.SaveChanges();
            }
        }

        public void LoadFeedTree(frmMain parentForm)
        {
            parentForm.RssTreeView.Nodes[0].Nodes.Clear();
            var thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    if (crypt == null)
                        throw new Exception("User not logged in.");

                    using (var context = new dataEntities())
                    {
                        var feeds = context.RssFeeds.OrderBy(f => f.FeedPriority).ToList();

                        if (feeds == null)
                            return;

                        parentForm.Invoke(new Action(() =>
                        {
                            foreach (var item in feeds)
                            {
                                parentForm.RssTreeView.Nodes[0].Nodes.Add(crypt.Decrypt(item.FeedAlias, gstrKey));
                            }

                            parentForm.RssTreeView.ExpandAll();

                            parentForm.Text = NAME;

                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(parentForm, ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }));

            thread.Name = "Load Feeds";
            thread.IsBackground = true;
            thread.Start();
        }

        public int CountFeeds()
        {
            try
            {
                using (var context = new dataEntities())
                {
                    return context.RssFeeds.Count();
                }
            }
            catch
            {
                return 0;
            }
        }

        public void LoadSystem(frmMain parentForm)
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

                        parentForm.Invoke(new Action(() =>
                        {
                            parentForm.WindowState = conf.IsMaximized == true ? FormWindowState.Maximized : FormWindowState.Normal;

                            if (conf.RssFeedTreeWidth != null)
                                parentForm.RssTreeView.Width = (int)conf.RssFeedTreeWidth;

                            if (conf.ScreenHeight != null)
                                parentForm.Height = (int)conf.ScreenHeight;

                            if (conf.ScreenWidth != null)
                                parentForm.Width = (int)conf.ScreenWidth;

                            parentForm.Text = NAME;
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(parentForm, ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }));

            thread.Name = "Load User Config";
            thread.IsBackground = true;
            thread.Start();
        }

        public void LoadTorConfig(frmMain parentForm)
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    if (crypt == null)
                        throw new Exception("User not logged in.");

                    using (var context = new dataEntities())
                    {
                        var torConfig = context.TorrentClient.OrderByDescending(c => c.DateAdd).FirstOrDefault();

                        if (torConfig == null)
                            return;

                        parentForm.Invoke(new Action(() =>
                        {
                            parentForm.TorAddress.Text = crypt.Decrypt(torConfig.Address, gstrKey);
                            parentForm.TorPort.Text = torConfig.Port.ToString();
                            parentForm.TorUser.Text = crypt.Decrypt(torConfig.Username, gstrKey);
                            parentForm.TorPass.Text = torConfig.Password.Substring(0, 6); //fake pass
                            parentForm.TorChkForceDown.Checked = torConfig.IsForceDown.Value;
                            parentForm.TorChkStop100.Checked = torConfig.IsStop100.Value;
                            parentForm.TorUpSeconds.Text = torConfig.UpdateRate.ToString();
                            parentForm.TorRefresh.Text = torConfig.RefreshRate.ToString();

                            parentForm.Text = NAME;
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(parentForm, ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }));

            thread.Name = "Load uTorrent Config";
            thread.IsBackground = true;
            thread.Start();
        }

        public bool LoadPassword()
        {
            try
            {
                using (var context = new dataEntities())
                {
                    var password = context.UserConfig.Where(p => p.Password != null).Select(p => p.Password).FirstOrDefault();

                    if (string.IsNullOrEmpty(password))
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool MakeLogin(string pass)
        {
            if (string.IsNullOrEmpty(pass))
                return false;

            PasswordController pc = null;

            using (var context = new dataEntities())
            {
                var password = context.UserConfig.Where(p => p != null).Select(p => p.Password).FirstOrDefault();

                if (string.IsNullOrEmpty(password))
                {
                    return false;
                }

                pc = new PasswordController();
                var isValid = pc.ValidatePassword(pass, password);
                pass = string.Empty;

                if (isValid)
                {
                    var arrPass = pc.GetHash(password).ToCharArray();

                    unsafe
                    {
                        fixed (char* pChars = arrPass)
                        {
                            if (gstrKey == null)
                                gstrKey = new SecureString(pChars, arrPass.Length);
                        }
                    }

                    for (int i = 0; i < arrPass.Length; i++)
                    {
                        arrPass[i] = '\0';
                    }

                    gstrKey.MakeReadOnly();
                    crypt = new CryptController();
                }

                return isValid;
            }
        }
    }
}
