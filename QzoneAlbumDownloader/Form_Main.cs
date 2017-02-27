using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace QzoneAlbumDownloader
{
    public partial class Form_Main : Form
    {

        #region User Information

        public static class UserInformation
        {
            public static string QQNumber = string.Empty;
            public static string Cookie = string.Empty;
            public static string TargetQQNumber = string.Empty;
            public static List<AlbumInfo> AlbumList = new List<AlbumInfo>();
        }

        #endregion

        #region UI && Load

        public Form_Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            //ThreadPool.SetMinThreads(5, 5);
            //ThreadPool.SetMaxThreads(10, 10);
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            Button_Detect_HeadIMG.Image = Properties.Resources.ic_account_box_white_100px;
        }

        /// <summary>
        /// 加载引用dll
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }

        #endregion

        #region Global

        #endregion

        #region Detect

        Thread ThreadDetect;

        /// <summary>
        /// 解析相册
        /// </summary>
        private void Detect()
        {
            try
            {
                Invoke((EventHandler)delegate
                {
                    ProcessBar_Detect.Value = 0;
                    ProcessBar_Detect.Visible = true;
                    Text_Detect_Number.ReadOlay = true;
                    UserInformation.TargetQQNumber = Text_Detect_Number.Text;
                    Label_Detect_Tip_SetText("正在获取信息");
                    Label_Detect_Tip.Visible = true;
                });
                var DataRes = string.Empty;
                var CheckHasAccess = AlbumHelper.CheckHasAccess(UserInformation.TargetQQNumber, UserInformation.Cookie, out DataRes);
                switch (CheckHasAccess)
                {
                    case AlbumHelper.AccessState.OK:
                        {
                            Label_Detect_Tip_SetText("正在获取相册列表", Color.White);
                            UserInformation.AlbumList = AlbumHelper.ResolveAlbum(DataRes, UserInformation.TargetQQNumber, UserInformation.Cookie);
                            Label_Detect_Tip_SetText(string.Format("共获取到 {0} 个相册", UserInformation.AlbumList.Count), Color.White);
                            if (UserInformation.AlbumList.Count == 0)
                            {
                                Label_Detect_Tip_SetText(string.Format("未获取到任何开放相册"), Color.Red);
                                break;
                            }
                            int album_index = 1;
                            foreach (var alb in UserInformation.AlbumList)
                            {
                                ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                                {
                                    var xml = AlbumHelper.GetImageListXml(UserInformation.TargetQQNumber, UserInformation.Cookie, alb.ID);
                                    Label_Detect_Tip_SetText(string.Format
                                        ("正在获取 {0}/{1} 相册照片列表", album_index, UserInformation.AlbumList.Count), Color.White);
                                    if (AlbumHelper.ResolveImage(xml, out List<ImageInfo> list, out Exception e))
                                        alb.Images = list;
                                    else
                                    {
                                        Label_Detect_Tip_SetText("抓取数据时发生异常 - 网络异常", Color.Red);
                                        if(ThreadDetect != null)
                                            ThreadDetect.Abort();
                                        return;
                                    }
                                    album_index++;
                                }));
                            }
                            while (album_index <= UserInformation.AlbumList.Count) ;
                            Label_Detect_Tip_SetText("正在加载相册缩略图", Color.White);
                            LoadAlbumPage();
                            Thread.Sleep(100);
                            Invoke((EventHandler)delegate
                            {
                                TabControl_Main.SelectedTab = TabPage_Album;
                                Label_Detect_Tip.Visible = false;
                            });
                            break;
                        }
                    case AlbumHelper.AccessState.NeedLogin:
                        {
                            Label_Detect_Tip_SetText("目标空间非公开 请先登录", Color.Red);
                            Invoke((EventHandler)delegate
                            {
                                Button_Cancel.Visible = true;
                                Button_Login.Visible = true;
                            });
                            break;
                        }
                    case AlbumHelper.AccessState.NoAccess:
                        {
                            Label_Detect_Tip_SetText("没有访问权限 请切换账号", Color.Red);
                            Invoke((EventHandler)delegate
                            {
                                Button_Cancel.Visible = true;
                                Button_Login.Visible = true;
                            });
                            break;
                        }
                    case AlbumHelper.AccessState.NumberError:
                        {
                            Label_Detect_Tip_SetText("QQ号码错误", Color.Red);
                            break;
                        }
                }
            }
            catch (ThreadAbortException)
            {
                Label_Detect_Tip_SetText("");
            }
            catch (Exception e)
            {
                Label_Detect_Tip_SetText("抓取数据时发生异常", Color.Red);
            }
            finally
            {
                Invoke((EventHandler)delegate
                {
                    ProcessBar_Detect.Visible = false;
                    Button_Detect_Enter.Text = "访问";
                    Text_Detect_Number.ReadOlay = false;
                });
                ThreadDetect = null;
            }
        }

        private void Text_Detect_Number_TextChanged(object sender, EventArgs e)
        {
            Timer_GetUserHeadIMG.Enabled = false;
            Timer_GetUserHeadIMG.Enabled = true;
            if (ThreadGetUserHeadIMG.ThreadState == System.Threading.ThreadState.Running)
                CancellationTokenSourceGetUserHeadIMG.Cancel();
        }

        Thread ThreadGetUserHeadIMG;
        CancellationTokenSource CancellationTokenSourceGetUserHeadIMG;

        private void Timer_GetUserHeadIMG_Tick(object sender, EventArgs e)
        {
            try
            {
                CancellationTokenSourceGetUserHeadIMG = new CancellationTokenSource();
                ThreadGetUserHeadIMG = new Thread(new ThreadStart(delegate
                {
                    Bitmap head = new Bitmap(100, 100);
                    string name = string.Empty;
                    string qqnumber = string.Empty;
                    Invoke((EventHandler)delegate
                    {
                        qqnumber = Text_Detect_Number.Text;
                    });
                    var MethodSuccessed = PortraitHelper.GetUserPortrait(qqnumber, out head, out name);
                    if (CancellationTokenSourceGetUserHeadIMG.Token.IsCancellationRequested)
                        return;
                    if (MethodSuccessed)
                    {
                        Invoke((EventHandler)delegate
                        {
                            Label_DetectName.Text = name;
                            Button_Detect_HeadIMG.Image = head;
                            TipTool.SetToolTip(Label_DetectName, string.Format("点击上方头像打开QQ空间\nQQ号：{0}\n昵称：{1}", qqnumber, name));
                            TipTool.SetToolTip(Button_Detect_HeadIMG, string.Format("点击打开QQ空间\nQQ号：{0}\n昵称：{1}", qqnumber, name));
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            Label_DetectName.Text = string.Empty;
                            Button_Detect_HeadIMG.Image = Properties.Resources.ic_account_box_white_100px;
                            TipTool.SetToolTip(Label_DetectName, "");
                            TipTool.SetToolTip(Button_Detect_HeadIMG, "");
                        });
                    }
                }))
                {

                    IsBackground = true
                };
                ThreadGetUserHeadIMG.Start();
            }
            catch
            {

            }
            finally
            {
                Timer_GetUserHeadIMG.Enabled = false;
            }
        }

        private void Button_Detect_Enter_Click(object sender, EventArgs e)
        {
            Button_Cancel.Visible = false;
            Button_Login.Visible = false;
            if (ThreadDetect == null)
            {
                ThreadDetect = new Thread(new ThreadStart(Detect))
                {
                    IsBackground = true
                };
                ThreadDetect.Start();
                Button_Detect_Enter.Text = "取消";
            }
            else
            {
                ThreadDetect.Abort();
                ThreadDetect = null;
                Button_Detect_Enter.Text = "访问";

            }
        }

        private void Label_Detect_Tip_SetText(string str, Color col)
        {
            Invoke((EventHandler)delegate
            {
                Label_Detect_Tip.Text = str;
                Label_Detect_Tip.ForeColor = col;
            });
        }

        private void Label_Detect_Tip_SetText(string str)
        {
            Label_Detect_Tip_SetText(str, Color.White);
        }

        private void Text_Detect_Number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Button_Detect_Enter.PerformClick();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Label_Detect_Tip.Text = "";
            Button_Cancel.Visible = false;
            Button_Login.Visible = false;
        }

        private void Button_Login_Click(object sender, EventArgs e)
        {
            if (Login())
            {
                Label_Detect_Tip_SetText("登录成功");
                Button_Detect_Enter.PerformClick();
            }
            else
                Label_Detect_Tip_SetText("登录失败", Color.Red);
        }

        private void Button_Detect_HeadIMG_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(delegate
            {
                string target = string.Empty;
                Invoke((EventHandler)delegate
                {
                    target = Text_Detect_Number.Text;
                });
                Process.Start(string.Format("http://user.qzone.qq.com/{0}", target));
            })).Start();
        }

        /// <summary>
        /// 登录函数
        /// </summary>
        private bool Login()
        {
            bool LoginSucceed = false;
            var frm = new Form_QzoneLogin();
            frm.ShowDialog();
            UserInformation.Cookie = (LoginSucceed = !string.IsNullOrEmpty(frm.Cookie)) ? frm.Cookie : UserInformation.Cookie;
            UserInformation.QQNumber = (LoginSucceed = LoginSucceed && !string.IsNullOrEmpty(frm.QQNumber)) ? frm.QQNumber : UserInformation.QQNumber;
            frm.Dispose();
            if (LoginSucceed)
            {
                new Thread(new ThreadStart(delegate
                {
                    Bitmap head = new Bitmap(100, 100);
                    string name = string.Empty;
                    var MethodSuccessed = PortraitHelper.GetUserPortrait(UserInformation.QQNumber, out head, out name);
                    if (CancellationTokenSourceGetUserHeadIMG.Token.IsCancellationRequested)
                        return;
                    if (MethodSuccessed)
                    {
                        Invoke((EventHandler)delegate
                        {
                            Label_Header_UserName.Text = name;
                            Button_Header_UserIMG.Image = head;
                            TipTool.SetToolTip(Label_Header_UserName, string.Format("点击头像切换账号\nQQ号：{0}\n昵称：{1}", UserInformation.QQNumber, name));
                            TipTool.SetToolTip(Button_Header_UserIMG, string.Format("点击头像切换账号\nQQ号：{0}\n昵称：{1}", UserInformation.QQNumber, name));
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            Label_Header_UserName.Text = string.Empty;
                            Button_Header_UserIMG.Image = Properties.Resources.ic_account_box_white_100px;
                            TipTool.SetToolTip(Label_Header_UserName, string.Empty);
                            TipTool.SetToolTip(Button_Header_UserIMG, string.Empty);
                        });
                    }
                })).Start();
                return true;
            }
            else
                return false;
        }

        #endregion

        #region AlbumPage

        List<Controls.AlbumControl> AlbumControlList = new List<Controls.AlbumControl>();

        /// <summary>
        /// 加载相册页面
        /// </summary>
        private void LoadAlbumPage()
        {
            Invoke((EventHandler)delegate
            {
                for (int i = AlbumControlList.Count - 1; i >= 0; i--)
                {
                    var ctl = AlbumControlList[i];
                    TipTool.SetToolTip(ctl, string.Empty);
                    FlowLayoutPanel_Album.Controls.Remove(ctl);
                    AlbumControlList.Remove(ctl);
                }
            });
            foreach (var album in UserInformation.AlbumList)
            {
                Invoke((EventHandler)delegate
                {
                    var ctl = new Controls.AlbumControl()
                    {
                        Width = 200,
                        Title = album.Name,
                        ImageURL = album.PreviewImagePath,
                        Font = new Font("微软雅黑", 12),
                        ForeColor = Color.FromArgb(208, 214, 220),
                        HintFont = new Font("微软雅黑", 10),
                        HintForeColor = Color.FromArgb(152, 153, 155),
                        HintString = album.Total + "张",
                        LoadingImage = Properties.Resources.ic_wallpaper_black_48dp,
                        Tag = album.Images
                    };
                    ctl.Click += LoadPhotoList;
                    FlowLayoutPanel_Album.Controls.Add(ctl);
                    AlbumControlList.Add(ctl);
                    ctl.BackColor = ctl.Parent.BackColor;
                    ctl.ReloadSize();
                    TipTool.SetToolTip(ctl, string.Format
                        ("相册名称：{0}\n创建时间：{1}\n照片总数：{2}\n修改时间：{3}\n最后一次上传时间：{4}\n评论数：{5}",
                        album.Name, album.CreateTime, album.Total, album.ModifyTime, album.LastUploadTime, album.Comment));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate 
                    {
                        var img = AlbumHelper.GetImageByURL(album.PreviewImagePath, UserInformation.Cookie);
                        ctl.Image = img;
                        ctl.IsLoading = false;
                    }));
                });
            }
        }

        #endregion

        #region PhotoList

        List<Controls.AlbumControl> AlbumControlPhotoList = new List<Controls.AlbumControl>();

        /// <summary>
        /// 加载照片列表页面
        /// </summary>
        private void LoadPhotoList(object sender, EventArgs e)
        {
            var obj = (Controls.AlbumControl)sender;
            if (obj == null)
                return;
            new Thread(new ThreadStart(delegate
            {
                Invoke((EventHandler)delegate
                {
                    AlbumControl_PhotoList_Album.HintFont = obj.HintFont;
                    AlbumControl_PhotoList_Album.IsLoading = false;
                    AlbumControl_PhotoList_Album.HintForeColor = obj.HintForeColor;
                    AlbumControl_PhotoList_Album.ForeColor = obj.ForeColor;
                    AlbumControl_PhotoList_Album.Font = obj.Font;
                    AlbumControl_PhotoList_Album.Image = obj.Image;
                    AlbumControl_PhotoList_Album.Title = obj.Title;
                    AlbumControl_PhotoList_Album.HintString = obj.HintString;
                    FlowLayoutPanel_PhotoList.Visible = false;
                    TabControl_PhotoList.SelectedTab = TabPage_PhotoList_Loading;
                    TabControl_Main.SelectedTab = TabPage_PhotoList;
                    for (int i = AlbumControlPhotoList.Count - 1; i >= 0; i--)
                    {
                        var ctl = AlbumControlPhotoList[i];
                        TipTool.SetToolTip(ctl, string.Empty);
                        FlowLayoutPanel_PhotoList.Controls.Remove(ctl);
                        AlbumControlPhotoList.Remove(ctl);
                    }
                });
                foreach (var image in (List<ImageInfo>)obj.Tag)
                {
                    Invoke((EventHandler)delegate
                    {
                        var ctl = new Controls.AlbumControl()
                        {
                            Width = 200,
                            Title = image.Name,
                            ImageURL = image.PreviewImagePath,
                            Font = new Font("微软雅黑", 12),
                            ForeColor = Color.FromArgb(208, 214, 220),
                            HintFont = new Font("微软雅黑", 10),
                            HintForeColor = Color.FromArgb(152, 153, 155),
                            HintString = image.RawShootTime == "0" ? "" : image.RawShootTime,
                            LoadingImage = Properties.Resources.ic_wallpaper_black_48dp,
                            Tag = image
                        };
                        FlowLayoutPanel_PhotoList.Controls.Add(ctl);
                        AlbumControlPhotoList.Add(ctl);
                        ctl.BackColor = ctl.Parent.BackColor;
                        ctl.ReloadSize();
                        TipTool.SetToolTip(ctl, string.Format
                            ("照片名称：{0}\n拍摄时间：{1}\n上传时间：{2}\n修改时间：{3}\n图片宽度：{4}\n图片高度：{5}",
                            image.Name, image.RawShootTime, image.UploadTime, image.ModifyTime, image.Width, image.Height));
                    });
                }
                Invoke((EventHandler)delegate
                {
                    TabControl_PhotoList.SelectedTab = TabPage_PhotoList_Info;
                });
                Thread.Sleep(500);
                Invoke((EventHandler)delegate
                {
                    FlowLayoutPanel_PhotoList.Visible = true;
                });
                foreach (var ctl in AlbumControlPhotoList)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
                    {
                        try
                        {
                            GC.Collect();
                            if (ctl == null || ctl.Parent == null)
                                return;
                            ctl.Click += ShowImageViewerForm;
                            var img = AlbumHelper.GetImageByURL(((ImageInfo)ctl.Tag).PreviewImagePath, UserInformation.Cookie);
                            if (ctl == null || ctl.Parent == null)
                                return;
                            Invoke((EventHandler)delegate
                            {
                                ctl.Image = img;
                                ctl.IsLoading = false;
                            });
                        }
                        catch
                        { }
                    }));
                }
            })).Start();
        }

        /// <summary>
        /// 加载并显示照片查看器窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowImageViewerForm(object sender, EventArgs e)
        {
            var frm = new Form_ImageViewer((ImageInfo)((Controls.AlbumControl)sender).Tag)
            {
                Width = ((ImageInfo)((Controls.AlbumControl)sender).Tag).Width,
                Height = ((ImageInfo)((Controls.AlbumControl)sender).Tag).Height
            };
            frm.LoadImage();
            frm.Show();
        }

        #endregion

        #region Header

        TabPage LastTabPage;

        private void Button_Header_UserIMG_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Button_Header_Back_Click(object sender, EventArgs e)
        {
            if (LastTabPage != null)
                TabControl_Main.SelectedTab = LastTabPage;
        }

        private void TabControl_Main_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            LastTabPage = (sender as TabControl).SelectedTab;
        }

        #endregion

    }
}
