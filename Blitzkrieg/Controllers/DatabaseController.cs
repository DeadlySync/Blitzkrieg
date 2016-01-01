using Blitzkrieg.Connection;
using Blitzkrieg.DataBase;
using Blitzkrieg.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        private static string connectionString = 
            "metadata=res://*/DataBase.Database.csdl|res://*/DataBase.Database.ssdl|res://*/DataBase.Database.msl;" + 
            "provider=System.Data.SQLite.EF6;" + 
            "provider connection string=\"data source=" + Application.StartupPath + "\\DataBase\\data.sqlite\"";

        public void SaveUserConfig(frmMain parentForm)
        {
            try
            {
                using (var context = new dataEntities(connectionString))
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

            using (var context = new dataEntities(connectionString))
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
            using (var context = new dataEntities(connectionString))
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

            using (var entities = new dataEntities(connectionString))
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

                //check for feed update
                if (feedForm.FeedIndex > -1)
                {
                    var upFeed = entities.RssFeeds.FirstOrDefault(f => f.Id == feedForm.FeedIndex);
                    if (upFeed != null)
                    {
                        upFeed.FeedAlias = crypt.Encrypt(feedForm.FeedAlias, gstrKey);
                        upFeed.FeedUrl = crypt.Encrypt(feedForm.FeedAddress, gstrKey);
                        upFeed.IsActive = feedForm.IsActive;
                        upFeed.FeedPriority = feedForm.FeedPriority;
                        upFeed.DateUpdate = DateTime.Now;
                        upFeed.FeedIcon = feedForm.FeedIcon;

                        entities.SaveChanges();
                        return;
                    }
                }

                //create new feed
                var feed = new RssFeeds();

                feed.FeedAlias = crypt.Encrypt(feedForm.FeedAlias, gstrKey);
                feed.FeedUrl = crypt.Encrypt(feedForm.FeedAddress, gstrKey);
                feed.IsActive = feedForm.IsActive;
                feed.FeedPriority = feedForm.FeedPriority;
                feed.DateAdd = DateTime.Now;
                feed.FeedIcon = feedForm.FeedIcon;

                entities.RssFeeds.Add(feed);
                entities.SaveChanges();
            }
        }

        public void DeleteFeed(frmMain parentForm, int feedIndex)
        {
            try
            {
                if (crypt == null)
                    throw new Exception("User not logged in.");

                using (var context = new dataEntities(connectionString))
                {
                    var rssFeeds = context.RssFeeds.FirstOrDefault(f => f.Id == feedIndex);

                    if (rssFeeds == null)
                        return;

                    context.RssFeeds.Remove(rssFeeds);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(parentForm, "This Application will now close. \r\nReason: " + ex.Message,
                        "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
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

                    using (var context = new dataEntities(connectionString))
                    {
                        var feeds = context.RssFeeds.OrderBy(f => f.FeedPriority).ToList();

                        if (feeds == null)
                            return;

                        parentForm.Invoke(new Action(() =>
                        {
                            foreach (var item in feeds)
                            {
                                if (!parentForm.feedImageList.Images.ContainsKey(item.Id.ToString()))
                                {
                                    using (Stream m = new MemoryStream(Convert.FromBase64String(item.FeedIcon)))
                                    {
                                        using (Image img = Image.FromStream(m))
                                        {
                                            parentForm.feedImageList.Images.Add(item.Id.ToString(), img);
                                            m.Dispose();
                                            img.Dispose();
                                        }
                                    }
                                }

                                parentForm.RssTreeView.Nodes[0].Nodes.Add(
                                    item.Id.ToString(),
                                    crypt.Decrypt(item.FeedAlias, gstrKey));

                                var imageIndexKey = parentForm.feedImageList.Images.IndexOfKey(item.Id.ToString());
                                parentForm.RssTreeView.Nodes[0].Nodes[item.Id.ToString()].ImageIndex = imageIndexKey;
                                parentForm.RssTreeView.Nodes[0].Nodes[item.Id.ToString()].SelectedImageIndex = imageIndexKey;
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

        public List<RssFeeds> LoadFeeds()
        {
            using (var context = new dataEntities(connectionString))
            {
                var feeds = context.RssFeeds
                            .Where(f => f.IsActive == true)
                            .OrderBy(f => f.FeedPriority)
                            .ToList();

                var decryptFeed = feeds.Select(f => new RssFeeds
                {
                    FeedAlias = crypt.Decrypt(f.FeedAlias, gstrKey),
                    FeedUrl = crypt.Decrypt(f.FeedUrl, gstrKey),
                    FeedIcon = f.FeedIcon,
                    FeedPriority = f.FeedPriority,
                    DateAdd = f.DateAdd,
                    DateUpdate = f.DateUpdate,
                    Id = f.Id,
                    IsActive = f.IsActive
                }).ToList();

                return decryptFeed;
            }
        }

        public void LoadSingleFeed(frmAddFeed parentForm, int feedIndex)
        {
            try
            {
                if (crypt == null)
                    throw new Exception("User not logged in.");

                using (var context = new dataEntities(connectionString))
                {
                    var rssFeeds = context.RssFeeds.FirstOrDefault(f => f.Id == feedIndex);

                    if (rssFeeds == null)
                        return;

                    if (parentForm.RssObject == null)
                        parentForm.RssObject = new RssFeedObject();

                    parentForm.RssObject.RssAddress = crypt.Decrypt(rssFeeds.FeedUrl, gstrKey);
                    parentForm.RssObject.RssActive = rssFeeds.IsActive == null ? false : (bool)rssFeeds.IsActive;
                    parentForm.RssObject.RssAlias = crypt.Decrypt(rssFeeds.FeedAlias, gstrKey);
                    parentForm.RssObject.RssPriority = (int)rssFeeds.FeedPriority;
                    parentForm.RssObject.RssFeedIcon = rssFeeds.FeedIcon;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(parentForm, "This Application will now close. \r\nReason: " + ex.Message,
                        "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Method that Downloads all Recorded Feed Data and Parses it back.
        /// </summary>
        public void SaveFeedItems()
        {
            ReadFeed reader = new ReadFeed();
            var feeds = LoadFeeds();

            if (feeds != null && feeds.Count > 0)
            {
                using (var context = new dataEntities(connectionString))
                {
                    List<FeedItems> list = new List<FeedItems>();
                    var except = context.FeedItems.Select(db => db.Link).ToList();

                    foreach (var item in feeds)
                    {
                        if (list.Count > 0)
                        {
                            except.AddRange(list.Where(f => !except.Contains(f.Link)).Select(d => d.Link));
                        }

                        list.AddRange(
                            reader.ParseFeed(item.FeedAlias, item.FeedUrl)
                            .Where(f => !except.Contains(f.Link))
                            .Select(f => new FeedItems
                            {
                                DateAdd = DateTime.Now,
                                FromFeed = f.FromFeed,
                                IsDownloaded = f.Downloaded,
                                Link = f.Link,
                                Magnet = f.Magnet,
                                PublishDate = f.PublishedDate,
                                Title = f.Title
                            }));
                    }

                    if (list.Count > 0)
                    {
                        context.FeedItems.AddRange(list);
                        context.SaveChanges();
                    }
                }
            }
            reader = null;
        }

        /// <summary>
        /// Method to Load Feed Data from Database
        /// </summary>
        /// <returns>Returns a List of Parsed Feed Data called "Post"</returns>
        public List<Post> LoadFeedItems()
        {
            using (var context = new dataEntities(connectionString))
            {
                return context.FeedItems
                    .Select(f => new Post
                {
                    Done = "0.0%",
                    Downloaded = f.IsDownloaded == null ? false : (bool)f.IsDownloaded,
                    FromFeed = f.FromFeed,
                    Link = f.Link,
                    Magnet = f.Magnet,
                    PublishedDate = f.PublishDate,
                    Title = f.Title
                }).ToList().OrderByDescending(f => f.PublishedDate).ToList();
            }
        }

        public void LoadSystem(frmMain parentForm)
        {
            var thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    using (var context = new dataEntities(connectionString))
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

                using (var context = new dataEntities(connectionString))
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
                using (var context = new dataEntities(connectionString))
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

            using (var context = new dataEntities(connectionString))
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
                using (var context = new dataEntities(connectionString))
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
