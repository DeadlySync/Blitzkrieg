using Blitzkrieg.DataBase;
using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Blitzkrieg.Controllers
{
    public class DatabaseController
    {
        private static readonly string NAME = "Blitzkrieg";

        public void SaveUserConfig(frmMain parentForm)
        {
            try
            {
                using (var context = new dataEntities())
                {
                    var conf = context.UserConfig.OrderByDescending(c => c.DateAdd).FirstOrDefault();
                    context.UserConfig.Remove(conf);
                    context.SaveChanges();

                    conf.IsMaximized = parentForm.WindowState == FormWindowState.Maximized;
                    conf.RssFeedTreeWidth = parentForm.RssTreeView.Width;
                    conf.ScreenHeight = parentForm.Height;
                    conf.ScreenWidth = parentForm.Width;
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

        public void SaveTorConfig(frmMain parentForm)
        {
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

            using (var context = new dataEntities())
            {
                parentForm.Text += " - Saving...";
                parentForm.Update();

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
                torConfig.IsForceDown = parentForm.TorChkForceDown.Checked;
                torConfig.IsStop100 = parentForm.TorChkStop100.Checked;
                torConfig.UpdateRate = Convert.ToInt64(update);
                torConfig.RefreshRate = Convert.ToInt64(refresh);

                context.TorrentClient.Add(torConfig);
                context.SaveChanges();
            }

            parentForm.Text = NAME;
            parentForm.Update();
        }

        public void LoadFeedTree(frmMain parentForm)
        {
            parentForm.RssTreeView.Nodes[0].Nodes.Clear();
            var thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    using (var context = new dataEntities())
                    {
                        var feeds = context.RssFeeds.OrderBy(f => f.FeedAlias).ToList();

                        if (feeds == null)
                            return;

                        parentForm.Invoke(new Action(() =>
                        {
                            foreach (var item in feeds)
                            {
                                parentForm.RssTreeView.Nodes[0].Nodes.Add(item.FeedAlias);
                            }

                            parentForm.RssTreeView.ExpandAll();

                            parentForm.Text = NAME;

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
                    MessageBox.Show(ex.Message);
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
                    using (var context = new dataEntities())
                    {
                        var torConfig = context.TorrentClient.OrderByDescending(c => c.DateAdd).FirstOrDefault();

                        if (torConfig == null)
                            return;

                        parentForm.Invoke(new Action(() =>
                        {
                            parentForm.TorAddress.Text = torConfig.Address;
                            parentForm.TorPort.Text = torConfig.Port.ToString();
                            parentForm.TorUser.Text = torConfig.Username;
                            parentForm.TorPass.Text = torConfig.Password;
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
                    MessageBox.Show(ex.Message);
                }
            }));

            thread.Name = "Load uTorrent Config";
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
