using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QzoneAlbumDownloader
{

    public class AlbumInfo
    {

        /// <summary>
        /// 类别ID
        /// </summary>
        public string ClassID = string.Empty;

        /// <summary>
        /// 评论数
        /// </summary>
        public int Comment = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime = string.Empty;

        /// <summary>
        /// 相册ID
        /// </summary>
        public string ID = string.Empty;

        /// <summary>
        /// 最后一次上传时间
        /// </summary>
        public string LastUploadTime = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyTime = string.Empty;

        /// <summary>
        /// 相册名称
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 相册封面链接
        /// </summary>
        public string PreviewImagePath = string.Empty;

        /// <summary>
        /// 照片总数
        /// </summary>
        public int Total = 0;

        /// <summary>
        /// 照片列表
        /// </summary>
        public List<ImageInfo> Images = new List<ImageInfo>();

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="UnixTime">double 型数字</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertIntDateTime(double UnixTime)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = startTime.AddSeconds(UnixTime);
            return time;
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="UnixTime">string 型数字</param>
        /// <returns>DateTime</returns>
        public static DateTime ConvertIntDateTime(string UnixTime)
        {
            if (double.TryParse(UnixTime, out double res))
                return ConvertIntDateTime(res);
            else
                return ConvertIntDateTime(0);
        }

    }

    public class ImageInfo
    {

        /// <summary>
        /// 图片高度
        /// </summary>
        public int Height = 0;

        /// <summary>
        /// 图片宽度
        /// </summary>
        public int Width = 0;

        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string PreviewImagePath = string.Empty;

        /// <summary>
        /// 拍摄时间
        /// </summary>
        public string RawShootTime = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        public string ModifyTime = string.Empty;

        /// <summary>
        /// 上传时间
        /// </summary>
        public string UploadTime = string.Empty;

        /// <summary>
        /// 图片名称
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// 原图路径
        /// </summary>
        public string OriginURL = string.Empty;

        /// <summary>
        /// 拥有者
        /// </summary>
        public string Owner = string.Empty;

        /// <summary>
        /// 拥有者名称
        /// </summary>
        public string OwnerName = string.Empty;

        /// <summary>
        /// 图片类型
        /// </summary>
        public string PhotoType = string.Empty;

    }

}
