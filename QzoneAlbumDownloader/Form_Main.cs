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
                WebBrowser_Login.Navigate("http://www.qzone.com");
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
                });
                Thread.Sleep(1000);
                Invoke((EventHandler)delegate
                {
                    TabControl_Main.SelectedTab = TabPage_Login;
                });
            }
            catch (ThreadAbortException)
            {

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
            }
        }

        #endregion

    }
}
