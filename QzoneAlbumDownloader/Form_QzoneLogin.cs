using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QzoneAlbumDownloader
{
    public partial class Form_QzoneLogin : Form
    {

        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("wininet.dll", SetLastError = true)]
        internal static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        /// <summary>
        /// 清空 Session
        /// </summary>
        public void ResetSession()
        {
            InternetSetOption(IntPtr.Zero, 42, IntPtr.Zero, 0);
        }

        public string Cookie;

        public string QQNumber;

        public Form_QzoneLogin()
        {
            InitializeComponent();
        }

        private void Form_QzoneLogin_Shown(object sender, EventArgs e)
        {
            ResetSession();
            WebBrowser_Login.ScriptErrorsSuppressed = true;
            WebBrowser_Login.Navigate(@"http://xui.ptlogin2.qq.com/cgi-bin/xlogin?proxy_url=http%3A//qzs.qq.com/qzone/v6/portal/proxy.html&daid=5&&hide_title_bar=1&low_login=0&qlogin_auto_login=1&no_verifyimg=1&link_target=blank&appid=549000912&style=22&target=self&s_url=http%3A%2F%2Fqzs.qq.com%2Fqzone%2Fv5%2Floginsucc.html%3Fpara%3Dizone&pt_qr_app=手机QQ空间&pt_qr_link=http%3A//z.qzone.com/download.html&self_regurl=http%3A//qzs.qq.com/qzone/v6/reg/index.html&pt_qr_help_link=http%3A//z.qzone.com/download.html");
        }

        private void WebBrowser_Login_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string reg_str = @"http://user.qzone.qq.com/(\d+)";
            Regex reg = new Regex(reg_str);
            MatchCollection mc = reg.Matches(WebBrowser_Login.Url.ToString());
            if (mc.Count == 0)
                return;
            else
            {
                Visible = false;
                QQNumber = mc[0].Groups[1].ToString();
                Cookie = WebBrowser_Login.Document.Cookie;
                WebBrowser_Login.Stop();
                WebBrowser_Login.Navigate("");
                Close();
            }
        }

        private void Form_QzoneLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
