using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QzoneAlbumDownloader
{
    public class PortraitHelper
    {

        /// <summary>
        /// 根据QQ号获取头像及昵称
        /// </summary>
        /// <param name="QQNumber">QQ号</param>
        /// <param name="HeadIMG">头像</param>
        /// <param name="UserName">昵称</param>
        /// <returns>操作是否成功</returns>
        public static bool GetUserPortrait(string QQNumber, out Bitmap HeadIMG, out string UserName, out string Number)
        {
            HeadIMG = new Bitmap(100, 100);
            try
            {
                UserName = "Unknown";
                Number = "0";
                string Get_URL = string.Format("http://r.pengyou.com/fcg-bin/cgi_get_portrait.fcg?uins={0}", QQNumber);
                string json = RequestHelper.GetResponse(Get_URL, "", "", "gbk");
                if (json.StartsWith("portraitCallBack"))
                {
                    Regex reg = new Regex("\"(.*?)\"");
                    MatchCollection mc = reg.Matches(json);
                    if (mc.Count == 3 && mc[0].Groups[1].ToString() == QQNumber)
                    {
                        Number = mc[0].Groups[1].ToString();
                        HeadIMG = AlbumHelper.GetImageByURL(mc[1].Groups[1].ToString());
                        UserName = mc[2].Groups[1].ToString();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                UserName = "Error";
                Number = "0";
                return false;
            }
        }

    }
}
