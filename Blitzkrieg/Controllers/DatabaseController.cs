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

        public bool SaveTorConfig(frmMain parentForm)
        {
            if (crypt == null)
                throw new Exception("User not logged on.");

            if (parentForm.TorObject == null)
                return false;

            using (var context = new dataEntities())
            {
                parentForm.ChangeNameStatus(" - Saving...");

                bool isNew = false;
                var torConfig = context.TorrentClient.OrderByDescending(t => t.DateAdd).FirstOrDefault();

                if (torConfig == null)
                {
                    torConfig = new TorrentClient();
                    torConfig.DateAdd = DateTime.Now;
                    isNew = true;
                }
                else
                {
                    if (parentForm.TorObject.TorPass == torConfig.Password.Substring(0, 6))
                        return false;
                    else
                        torConfig.DateUpdate = DateTime.Now;
                }

                torConfig.FullUri = crypt.Encrypt(parentForm.TorObject.uTorFullUrl, gstrKey);
                torConfig.Address = crypt.Encrypt(parentForm.TorObject.TorAddress, gstrKey);
                torConfig.Port = Convert.ToInt64(parentForm.TorObject.TorPort);
                torConfig.Username = crypt.Encrypt(parentForm.TorObject.TorUser, gstrKey);
                torConfig.Password = crypt.Encrypt(parentForm.TorObject.TorPass, gstrKey);
                torConfig.IsForceDown = parentForm.TorObject.TorChkForceDown;
                torConfig.IsStop100 = parentForm.TorObject.TorChkStop100;
                torConfig.UpdateRate = Convert.ToInt64(parentForm.TorObject.TorUpSeconds);
                torConfig.RefreshRate = Convert.ToInt64(parentForm.TorObject.TorRefresh);

                if (isNew)
                    context.TorrentClient.Add(torConfig);

                context.SaveChanges();
            }

            parentForm.ChangeNameStatus();

            return true;
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
                //check if there is any feed with the same priority
                var feeds = entities.RssFeeds.FirstOrDefault(f => f.FeedPriority == feedForm.FeedPriority);
                if (feeds != null)
                {
                    var lastFeed = entities.RssFeeds.OrderByDescending(f => f.FeedPriority).FirstOrDefault();
                    if (lastFeed != null)
                        feeds.FeedPriority = lastFeed.FeedPriority + 1;

                    entities.SaveChanges();
                }

                //create new feed
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

                            parentForm.ChangeNameStatus();

                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(parentForm, "This Application will now close. \r\nReason: " + ex.Message, 
                        "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    parentForm.ExitMenuItem_Click(null, null);
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

                            parentForm.ChangeNameStatus();
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(parentForm, "This Application will now close. \r\nReason: " + ex.Message,
                        "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    parentForm.ExitMenuItem_Click(null, null);
                }
            }));

            thread.Name = "Load User Config";
            thread.IsBackground = true;
            thread.Start();
        }

        public bool LoadTorConfig(frmMain parentForm)
        {
            try
            {
                if (crypt == null)
                    throw new Exception("User not logged in.");

                using (var context = new dataEntities())
                {
                    var torConfig = context.TorrentClient.OrderByDescending(c => c.DateAdd).FirstOrDefault();

                    if (torConfig == null)
                        return false;

                    if (parentForm.TorObject == null)
                        parentForm.TorObject = new uTorrentObject();

                    parentForm.TorObject.TorAddress = crypt.Decrypt(torConfig.Address, gstrKey);
                    parentForm.TorObject.TorPort = torConfig.Port.ToString();
                    parentForm.TorObject.TorUser = crypt.Decrypt(torConfig.Username, gstrKey);
                    parentForm.TorObject.TorPass = torConfig.Password.Substring(0, 6); //fake pass
                    parentForm.TorObject.TorChkForceDown = torConfig.IsForceDown.Value;
                    parentForm.TorObject.TorChkStop100 = torConfig.IsStop100.Value;
                    parentForm.TorObject.TorUpSeconds = torConfig.UpdateRate.ToString();
                    parentForm.TorObject.TorRefresh = torConfig.RefreshRate.ToString();

                    parentForm.uTorFullUrl = torConfig.FullUri;

                    parentForm.ChangeNameStatus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(parentForm, "This Application will now close. \r\nReason: " + ex.Message,
                        "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                parentForm.ExitMenuItem_Click(null, null);
            }

            return true;
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
    }
}
