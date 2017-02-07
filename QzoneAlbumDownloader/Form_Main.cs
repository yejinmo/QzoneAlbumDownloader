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
            InitializeComponent();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            PictureBox_DetectHeadIMG.Image = Properties.Resources.ic_account_box_white_100px;
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

        private void TabControl_Main_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

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
                                var xml = AlbumHelper.GetImageListXml(UserInformation.TargetQQNumber, UserInformation.Cookie, alb.ID);
                                Label_Detect_Tip_SetText(string.Format
                                    ("正在获取 {0}/{1} 相册照片列表", album_index, UserInformation.AlbumList.Count), Color.White);
                                alb.Images = AlbumHelper.ResolveImage(xml);
                                album_index++;
                            }
                            Label_Detect_Tip_SetText("正在加载相册缩略图", Color.White);
                            LoadAlbumPage();
                            Invoke((EventHandler)delegate
                            {
                                TabControl_Main.SelectedTab = TabPage_Album;
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
                Invoke((EventHandler)delegate
                {
                    MessageBox.Show(this, "抓取数据时发生异常\n\n" + e.Message, "错误");
                });
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
                            PictureBox_DetectHeadIMG.Image = head;
                            TipTool.SetToolTip(Label_DetectName, string.Format("QQ号：{0}\n昵称：{1}", qqnumber, name));
                            TipTool.SetToolTip(PictureBox_DetectHeadIMG, string.Format("QQ号：{0}\n昵称：{1}", qqnumber, name));
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            Label_DetectName.Text = string.Empty;
                            PictureBox_DetectHeadIMG.Image = Properties.Resources.ic_account_box_white_100px;
                            TipTool.SetToolTip(Label_DetectName, "");
                            TipTool.SetToolTip(PictureBox_DetectHeadIMG, "");
                        });
                    }
                }));
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
                ThreadDetect = new Thread(new ThreadStart(Detect));
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
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            Label_Header_UserName.Text = string.Empty;
                            Button_Header_UserIMG.Image = Properties.Resources.ic_account_box_white_100px;
                        });
                    }
                })).Start();
                Label_Detect_Tip_SetText("登录成功");
                Button_Detect_Enter.PerformClick();
            }
            else
                Label_Detect_Tip_SetText("登录失败", Color.Red);

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
                TipTool.RemoveAll();
                foreach (var ctl in AlbumControlList)
                {
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
                        HintString = album.Total + "张"
                    };
                    FlowLayoutPanel_Album.Controls.Add(ctl);
                    AlbumControlList.Add(ctl);
                    ctl.BackColor = ctl.Parent.BackColor;
                    ctl.ReloadSize();
                    TipTool.SetToolTip(ctl, string.Format
                        ("相册名称：{0}\n创建时间：{1}\n照片总数：{2}\n修改时间：{3}\n最后一次上传时间：{4}\n评论数：{5}",
                        album.Name, album.CreateTime, album.Total, album.ModifyTime, album.LastUploadTime, album.Comment));
                    new Thread(new ThreadStart(delegate
                    {
                        var img = AlbumHelper.GetImageByURL(album.PreviewImagePath, UserInformation.Cookie);
                        ctl.Image = img;
                    })).Start();
                });
            }
            Invoke((EventHandler)delegate
            {
                ReloadAlbumPage();
            });
        }

        /// <summary>
        /// 重载相册页面
        /// </summary>
        private void ReloadAlbumPage()
        {
            int padding = 20;
            int left = 0;
            int top = 20;
            foreach (var ctl in AlbumControlList)
            {
                int temp = (left == 0 ? padding : left + ctl.Width + padding);
                if (temp + ctl.Width + padding < TabPage_Album.Width)
                {
                    ctl.Left = temp;
                    ctl.Top = top;
                    left = temp;
                }
                else
                {
                    top += ctl.Height + padding;
                    left = 0;
                    ctl.Left = padding;
                    ctl.Top = top;
                }
            }
        }

        private void TabPage_Album_Resize(object sender, EventArgs e)
        {
            ReloadAlbumPage();
        }

        #endregion

     }
}
