using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

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
        public static AccessState CheckHasAccess(string QQNumber, string Cookie, out string res)
        {
            try
            {
                res = RequestHelper.GetResponse(
                                string.Format("http://photo.qq.com/fcgi-bin/fcg_list_album?uin={0}", QQNumber),
                                Cookie, "", "GBK");
                if (res.IndexOf("<errCode>-900</errCode>") > 0)
                    return AccessState.NeedLogin;
                if (res.IndexOf("<errCode>-961</errCode>") > 0)
                    return AccessState.NoAccess;
                if (res.IndexOf("<errCode>-902</errCode>") > 0)
                    return AccessState.NumberError;
                return AccessState.OK;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 权限枚举
        /// </summary>
        public enum AccessState
        {
            OK,
            NeedLogin,
            NoAccess,
            NumberError,
            Error
        }

        /// <summary>
        /// 解析相册列表
        /// </summary>
        /// <param name="xmldata">相册列表XML数据</param>
        /// <param name="qqnumber">目标QQ号</param>
        /// <param name="cookie">Cookie</param>
        /// <returns></returns>
        public static List<AlbumInfo> ResolveAlbum(string xmldata, string qqnumber, string cookie)
        {
            List<AlbumInfo> res = new List<AlbumInfo>();
            try
            {
                XElement xe = XElement.Parse(xmldata);
                IEnumerable<XElement> elements = from ele in xe.Elements("album")
                                                 select ele;
                foreach (var ele in elements)
                {
                    AlbumInfo alb = new AlbumInfo()
                    {
                        ClassID = ele.Element("classid").Value,
                        Comment = Convert.ToInt32(ele.Element("comment").Value),
                        CreateTime = AlbumInfo.ConvertIntDateTime(ele.Element("createtime").Value).ToLongDateString(),
                        ID = ele.Element("id").Value,
                        LastUploadTime = AlbumInfo.ConvertIntDateTime(ele.Element("lastuploadtime").Value).ToLongDateString(),
                        ModifyTime = AlbumInfo.ConvertIntDateTime(ele.Element("modifytime").Value).ToLongDateString(),
                        Name = ele.Element("name").Value,
                        PreviewImagePath = ele.Element("pre").Value.Replace("/a/", "/m/"),
                        Total = Convert.ToInt32(ele.Element("total").Value)
                    };
                    res.Add(alb);
                }
            }
            catch
            {

            }
            return res;
        }

        /// <summary>
        /// 解析照片列表
        /// </summary>
        /// <param name="xmldata">照片列表XML数据</param>
        /// <returns></returns>
        public static bool ResolveImage(string xmldata, out List<ImageInfo> list, out Exception exc)
        {
            List<ImageInfo> res = new List<ImageInfo>();
            try
            {
                XElement xe = XElement.Parse(xmldata);
                IEnumerable<XElement> elements = from ele in xe.Elements("pic")
                                                 select ele;
                foreach (var ele in elements)
                {
                    ImageInfo img = new ImageInfo()
                    {
                        Height = Convert.ToInt32(ele.Element("height").Value),
                        ModifyTime = AlbumInfo.ConvertIntDateTime(ele.Element("modifytime").Value).ToLongDateString(),
                        Name = ele.Element("name").Value,
                        OriginURL = ele.Element("origin_url").Value,
                        Owner = ele.Element("owner").Value,
                        OwnerName = ele.Element("ownername").Value,
                        PhotoType = ele.Element("phototype").Value,
                        PreviewImagePath = ele.Element("pre").Value.Replace("/a/", "/m/"),
                        RawShootTime = ele.Element("rawshoottime").Value,
                        UploadTime = ele.Element("uploadtime").Value,
                        Width = Convert.ToInt32(ele.Element("width").Value)
                    };
                    res.Add(img);
                }
            }
            catch(Exception e)
            {
                exc = e;
                list = res;
                return false;
            }
            exc = null;
            list = res;
            return true;
        }

        /// <summary>
        /// 获取照片列表XML数据
        /// </summary>
        /// <param name="QQNumber">QQ号</param>
        /// <param name="Cookie">Cookie</param>
        /// <param name="AlbumID">相册ID</param>
        /// <returns></returns>
        public static string GetImageListXml(string QQNumber, string Cookie, string AlbumID)
        {
            try
            {
                return RequestHelper.GetResponse(string.Format("http://photo.qq.com/fcgi-bin/fcg_list_photo?uin={0}&albumid={1}",
                    QQNumber, AlbumID), Cookie, "", "GBK");
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="URL">图片URL</param>
        /// <param name="Cookie">Cookie</param>
        /// <returns></returns>
        public static Bitmap GetImageByURL(string URL, string Cookie = "")
        {
            return StreamToBitmap(RequestHelper.GetResponse(URL, Cookie, ""));
        }

        /// <summary>
        /// Stream流转换为Bitmap
        /// </summary>
        /// <param name="inStream">Stream流</param>
        /// <returns></returns>
        public static Bitmap StreamToBitmap(Stream inStream)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int sz = inStream.Read(buffer, 0, 1024);
                    if (sz == 0) break;
                    stream.Write(buffer, 0, sz);
                }
                stream.Position = 0;
                return new Bitmap((Image)new Bitmap(stream));
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
        }

    }
}
