using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QzoneAlbumDownloader
{
    public partial class Form_QzoneLogin : Form
    {
        public string Cookie;

        public string QQNumber;

        public Form_QzoneLogin()
        {
            InitializeComponent();
        }

        private void Form_QzoneLogin_Shown(object sender, EventArgs e)
        {
            WebBrowser_Login.ScriptErrorsSuppressed = true;
            WebBrowser_Login.Navigate(@"http://xui.ptlogin2.qq.com/cgi-bin/xlogin?proxy_url=http%3A//qzs.qq.com/qzone/v6/portal/proxy.html&daid=5&&hide_title_bar=1&low_login=0&qlogin_auto_login=1&no_verifyimg=1&link_target=blank&appid=549000912&style=22&target=self&s_url=http%3A%2F%2Fqzs.qq.com%2Fqzone%2Fv5%2Floginsucc.html%3Fpara%3Dizone&pt_qr_app=手机QQ空间&pt_qr_link=http%3A//z.qzone.com/download.html&self_regurl=http%3A//qzs.qq.com/qzone/v6/reg/index.html&pt_qr_help_link=http%3A//z.qzone.com/download.html");
        }

        private void WebBrowser_Login_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string reg_str = @"\[http://(\d+).qzone.qq.com\]";
            Regex reg = new Regex(reg_str);
            MatchCollection mc = reg.Matches(WebBrowser_Login.DocumentTitle);
            if (mc.Count == 0)
                return;
            else
            {
                QQNumber = mc[0].Groups[1].ToString();
                Cookie = WebBrowser_Login.Document.Cookie;
                WebBrowser_Login.Stop();
                WebBrowser_Login.Navigate("");
                Close();
            }
        }
    }
}
