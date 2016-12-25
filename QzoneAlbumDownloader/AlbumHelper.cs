using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace QzoneAlbumDownloader
{
    public static class AlbumHelper
    {

        /// <summary>
        /// 检测目标空间是否公开
        /// </summary>
        /// <param name="QQNumber"></param>
        /// <returns></returns>
        public static bool CheckIsPublic(string QQNumber)
        {
            try
            {
                string res = RequestHelper.GetResponse(
                    string.Format("http://photo.qq.com/fcgi-bin/fcg_list_album?uin={0}", QQNumber),
                    "", "", "GBK");
                string check_str = @"<err>\n<errCode>(.\d+?)</errCode>\n<errMsg>(.+?)</errMsg>\n<msg>(.+?)</msg>\n<ret>(.\d+?)</ret>\n</err>";
                Regex reg = new Regex(check_str);
                MatchCollection mc = reg.Matches(res);
                if (mc.Count == 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 检测是否拥有目标空间访问权限
        /// </summary>
        /// <param name="QQNumber"></param>
        /// <param name="Cookie"></param>
        /// <returns></returns>
        public static bool CheckHasAccess(string QQNumber, string Cookie)
        {
            try
            {
                string res = RequestHelper.GetResponse(
                    string.Format("http://photo.qq.com/fcgi-bin/fcg_list_album?uin={0}", QQNumber),
                    "", "", "GBK");
                return true;
            }
            catch
            {
                throw;
            }
        }

        

    }
}
