using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            WebBrowser_Login.ScriptErrorsSuppressed = false;
            WebBrowser_Login.DocumentCompleted += WebBrowser_Login_DocumentCompleted;
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
            if (TabControl_Main.SelectedTab == TabPage_Login)
                //WebBrowser_Login.Navigate("http://www.qzone.com");
                WebBrowser_Login.Navigate(@"http://xui.ptlogin2.qq.com/cgi-bin/xlogin?proxy_url=http%3A//qzs.qq.com/qzone/v6/portal/proxy.html&daid=5&&hide_title_bar=1&low_login=0&qlogin_auto_login=1&no_verifyimg=1&link_target=blank&appid=549000912&style=22&target=self&s_url=http%3A%2F%2Fqzs.qq.com%2Fqzone%2Fv5%2Floginsucc.html%3Fpara%3Dizone&pt_qr_app=手机QQ空间&pt_qr_link=http%3A//z.qzone.com/download.html&self_regurl=http%3A//qzs.qq.com/qzone/v6/reg/index.html&pt_qr_help_link=http%3A//z.qzone.com/download.html");
        }

        #endregion

        #region Detect

        Thread ThreadDetect;

        private void Button_Detect_Enter_Click(object sender, EventArgs e)
        {
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
                Thread.Sleep(1000);
                var res = AlbumHelper.CheckIsPublic(UserInformation.TargetQQNumber);
                Invoke((EventHandler)delegate
                {
                    if (res)
                        ;
                    else
                    {
                        Label_Detect_Tip_SetText("目标空间非公开 请先登录", Color.Red);
                        Invoke((EventHandler)delegate
                        {
                            Button_Cancel.Visible = true;
                            Button_Login.Visible = true;
                        });
                    }
                    //TabControl_Main.SelectedTab = TabPage_Login;
                });
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
            Label_Detect_Tip_SetText(str, Color.Black);
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
            TabControl_Main.SelectedTab = TabPage_Login;
        }

        #endregion

        #region Login

        private void WebBrowser_Login_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string reg_str = @"\[http://(\d+).qzone.qq.com\]";
            Regex reg = new Regex(reg_str);
            MatchCollection mc = reg.Matches(WebBrowser_Login.DocumentTitle);
            if (mc.Count == 0)
                return;
            else
            {
                UserInformation.QQNumber = mc[0].Groups[1].ToString();
                UserInformation.Cookie = WebBrowser_Login.Document.Cookie;
                WebBrowser_Login.Stop();
                WebBrowser_Login.Navigate("");
                new Thread(new ThreadStart(delegate
                {
                    Invoke((EventHandler)delegate
                    {
                        MessageBox.Show(this, RequestHelper.GetResponse(
                            string.Format("http://photo.qq.com/fcgi-bin/fcg_list_album?uin={0}", UserInformation.TargetQQNumber),
                            UserInformation.Cookie, "", "GBK"));
                    });
                })).Start();
            }
        }

        #endregion

    }
}
