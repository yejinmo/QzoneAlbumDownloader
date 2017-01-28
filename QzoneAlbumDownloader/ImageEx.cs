using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Text;

namespace QzoneAlbumDownloader
{
    public class ImageEx
    {

        /// <summary>
        /// 缩小图片
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intWidth">缩小至宽度</param>
        /// <param name="intHeight">缩小至高度</param>
        public static Bitmap SmallPic(Bitmap strOldPic, int intWidth, int intHeight)
        {
            Bitmap objNewPic;
            try
            {
                objNewPic = new Bitmap(strOldPic, intWidth, intHeight);
                return objNewPic;
            }
            catch (Exception exp) { throw exp; }
        }

        /// <summary>
        /// 按比例缩小图片，自动计算高度
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件a名(包括路径)</param>
        /// <param name="intWidth">缩小至宽度</param>
        public static Bitmap SmallPic(Bitmap strOldPic, int intWidth)
        {

            Bitmap objNewPic;
            try
            {
                int intHeight = (int)(((intWidth * 1.0000) / strOldPic.Width) * strOldPic.Height);
                objNewPic = new Bitmap(strOldPic, intWidth, intHeight);
                return objNewPic;
            }
            catch (Exception exp) { throw exp; }
        }

        /// <summary>
        /// 按比例缩小图片，自动计算宽度
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intHeight">缩小至高度</param>
        public static Bitmap SmallPicH(Bitmap strOldPic, int intHeight)
        {
            Bitmap objNewPic;
            try
            {
                int intWidth = (int)(((intHeight * 1.0000) / strOldPic.Height) * strOldPic.Width);
                objNewPic = new Bitmap(strOldPic, intWidth, intHeight);
                return objNewPic;
            }
            catch (Exception exp) { throw exp; }
        }

        public static Bitmap CutImage(Bitmap sourceBitmap, Rectangle rc)
        {
            if (rc.Bottom < 0)
                return null;
            Bitmap TempsourceBitmap = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top);
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(sourceBitmap, 0, 0, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }

        public static Bitmap JoinImage(Bitmap sourceBitmap, Bitmap joinBitmap, Rectangle rc)
        {
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(joinBitmap, 0, 0, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }

        /// <summary>
        /// 铺满
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="joinBitmap"></param>
        /// <param name="rc"></param>
        /// <returns></returns>
        public static Bitmap JoinMImage(Bitmap sourceBitmap, Bitmap joinBitmap, Rectangle rc)
        {
            joinBitmap = SmallPic(joinBitmap, sourceBitmap.Width, sourceBitmap.Height);
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(joinBitmap, 0, 0, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }

        public static Bitmap JoinMImage(Bitmap sourceBitmap, Bitmap joinBitmap, Rectangle rc, int x, int y)
        {
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(joinBitmap, x, y, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }

        public static Bitmap JoinTxtImage(Bitmap sourceBitmap, string txt, Color color, int x, int y)
        {
            Bitmap TempsourceBitmap = sourceBitmap;
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            if (sourceBitmap.Width > 200)
                gr.DrawString(txt, new Font("微软雅黑", 13, FontStyle.Bold), new SolidBrush(color), new PointF(x, y));
            else
                gr.DrawString(txt, new Font("微软雅黑", 12, FontStyle.Bold), new SolidBrush(color), new PointF(x, y));
            gr.Dispose();
            return TempsourceBitmap;
        }

        /// <summary>
        /// 更改图片亮度
        /// </summary>
        /// <param name="a"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Bitmap BrightnessP(Bitmap a, int v)
        {
            System.Drawing.Imaging.BitmapData bmpData = a.LockBits(new Rectangle(0, 0, a.Width, a.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int bytes = a.Width * a.Height * 3;
            IntPtr ptr = bmpData.Scan0;
            int stride = bmpData.Stride;
            unsafe
            {
                byte* p = (byte*)ptr;
                int temp;
                for (int j = 0; j < a.Height; j++)
                {
                    for (int i = 0; i < a.Width * 3; i++, p++)
                    {
                        temp = (int)(p[0] + v);
                        temp = (temp > 255) ? 255 : temp < 0 ? 0 : temp;
                        p[0] = (byte)temp;

                    }
                    p += stride - a.Width * 3;
                }
            }
            a.UnlockBits(bmpData);
            return a;
        }

        #region 色彩处理  

        /// <summary>  
        /// 设置图形颜色  边缘的色彩更换成新的颜色  
        /// </summary>  
        /// <param name="p_Image">图片</param>  
        /// <param name="p_OldColor">老的边缘色彩</param>  
        /// <param name="p_NewColor">新的边缘色彩</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns>清理后的图形</returns>  
        public static Image SetImageColorBrim(Image p_Image, Color p_OldColor, Color p_NewColor, int p_Float)
        {
            int _Width = p_Image.Width;
            int _Height = p_Image.Height;

            Bitmap _NewBmp = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb);
            Graphics _Graphics = Graphics.FromImage(_NewBmp);
            _Graphics.DrawImage(p_Image, new Rectangle(0, 0, _Width, _Height));
            _Graphics.Dispose();

            BitmapData _Data = _NewBmp.LockBits(new Rectangle(0, 0, _Width, _Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _Data.PixelFormat = PixelFormat.Format32bppArgb;
            int _ByteSize = _Data.Stride * _Height;
            byte[] _DataBytes = new byte[_ByteSize];
            Marshal.Copy(_Data.Scan0, _DataBytes, 0, _ByteSize);

            int _Index = 0;
            #region 列  
            for (int z = 0; z != _Height; z++)
            {
                _Index = z * _Data.Stride;
                for (int i = 0; i != _Width; i++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);

                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                        _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                        _DataBytes[_Index] = (byte)p_NewColor.B;
                        _Index += 4;
                    }
                    else
                    {
                        break;
                    }
                }
                _Index = (z + 1) * _Data.Stride;
                for (int i = 0; i != _Width; i++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index - 1], _DataBytes[_Index - 2], _DataBytes[_Index - 3], _DataBytes[_Index - 4]);

                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index - 1] = (byte)p_NewColor.A;
                        _DataBytes[_Index - 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index - 3] = (byte)p_NewColor.G;
                        _DataBytes[_Index - 4] = (byte)p_NewColor.B;
                        _Index -= 4;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion

            #region 行  

            for (int i = 0; i != _Width; i++)
            {
                _Index = i * 4;
                for (int z = 0; z != _Height; z++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);
                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                        _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                        _DataBytes[_Index] = (byte)p_NewColor.B;
                        _Index += _Data.Stride;
                    }
                    else
                    {
                        break;
                    }
                }
                _Index = (i * 4) + ((_Height - 1) * _Data.Stride);
                for (int z = 0; z != _Height; z++)
                {
                    Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);
                    if (ScanColor(_Color, p_OldColor, p_Float))
                    {
                        _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                        _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                        _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                        _DataBytes[_Index] = (byte)p_NewColor.B;
                        _Index -= _Data.Stride;
                    }
                    else
                    {
                        break;
                    }
                }
            }


            #endregion
            Marshal.Copy(_DataBytes, 0, _Data.Scan0, _ByteSize);
            _NewBmp.UnlockBits(_Data);
            return _NewBmp;
        }

        /// <summary>  
        /// 设置图形颜色  所有的色彩更换成新的颜色  
        /// </summary>  
        /// <param name="p_Image">图片</param>  
        /// <param name="p_OdlColor">老的颜色</param>  
        /// <param name="p_NewColor">新的颜色</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns>清理后的图形</returns>  
        public static Image SetImageColorAll(Image p_Image, Color p_OdlColor, Color p_NewColor, int p_Float)
        {
            int _Width = p_Image.Width;
            int _Height = p_Image.Height;

            Bitmap _NewBmp = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb);
            Graphics _Graphics = Graphics.FromImage(_NewBmp);
            _Graphics.DrawImage(p_Image, new Rectangle(0, 0, _Width, _Height));
            _Graphics.Dispose();

            BitmapData _Data = _NewBmp.LockBits(new Rectangle(0, 0, _Width, _Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _Data.PixelFormat = PixelFormat.Format32bppArgb;
            int _ByteSize = _Data.Stride * _Height;
            byte[] _DataBytes = new byte[_ByteSize];
            Marshal.Copy(_Data.Scan0, _DataBytes, 0, _ByteSize);

            int _WhileCount = _Width * _Height;
            int _Index = 0;
            for (int i = 0; i != _WhileCount; i++)
            {
                Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);
                if (ScanColor(_Color, p_OdlColor, p_Float))
                {
                    _DataBytes[_Index + 3] = (byte)p_NewColor.A;
                    _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                    _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                    _DataBytes[_Index] = (byte)p_NewColor.B;
                }
                _Index += 4;
            }
            Marshal.Copy(_DataBytes, 0, _Data.Scan0, _ByteSize);
            _NewBmp.UnlockBits(_Data);
            return _NewBmp;
        }

        /// <summary>  
        /// 设置图形颜色  坐标的颜色更换成新的色彩 （漏斗）  
        /// </summary>  
        /// <param name="p_Image">新图形</param>  
        /// <param name="p_Point">位置</param>  
        /// <param name="p_NewColor">新的色彩</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns>清理后的图形</returns>  
        public static Image SetImageColorPoint(Image p_Image, Point p_Point, Color p_NewColor, int p_Float)
        {
            int _Width = p_Image.Width;
            int _Height = p_Image.Height;

            if (p_Point.X > _Width - 1) return p_Image;
            if (p_Point.Y > _Height - 1) return p_Image;

            Bitmap _SS = (Bitmap)p_Image;
            Color _Scolor = _SS.GetPixel(p_Point.X, p_Point.Y);
            Bitmap _NewBmp = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb);
            Graphics _Graphics = Graphics.FromImage(_NewBmp);
            _Graphics.DrawImage(p_Image, new Rectangle(0, 0, _Width, _Height));
            _Graphics.Dispose();

            BitmapData _Data = _NewBmp.LockBits(new Rectangle(0, 0, _Width, _Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _Data.PixelFormat = PixelFormat.Format32bppArgb;
            int _ByteSize = _Data.Stride * _Height;
            byte[] _DataBytes = new byte[_ByteSize];
            Marshal.Copy(_Data.Scan0, _DataBytes, 0, _ByteSize);


            int _Index = (p_Point.Y * _Data.Stride) + (p_Point.X * 4);

            Color _OldColor = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);

            if (_OldColor.Equals(p_NewColor)) return p_Image;
            Stack<Point> _ColorStack = new Stack<Point>(1000);
            _ColorStack.Push(p_Point);

            _DataBytes[_Index + 3] = (byte)p_NewColor.A;
            _DataBytes[_Index + 2] = (byte)p_NewColor.R;
            _DataBytes[_Index + 1] = (byte)p_NewColor.G;
            _DataBytes[_Index] = (byte)p_NewColor.B;

            do
            {
                Point _NewPoint = (Point)_ColorStack.Pop();

                if (_NewPoint.X > 0) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X - 1, _NewPoint.Y, _OldColor, p_NewColor, p_Float);
                if (_NewPoint.Y > 0) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X, _NewPoint.Y - 1, _OldColor, p_NewColor, p_Float);

                if (_NewPoint.X < _Width - 1) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X + 1, _NewPoint.Y, _OldColor, p_NewColor, p_Float);
                if (_NewPoint.Y < _Height - 1) SetImageColorPoint(_DataBytes, _Data.Stride, _ColorStack, _NewPoint.X, _NewPoint.Y + 1, _OldColor, p_NewColor, p_Float);

            }
            while (_ColorStack.Count > 0);

            Marshal.Copy(_DataBytes, 0, _Data.Scan0, _ByteSize);
            _NewBmp.UnlockBits(_Data);
            return _NewBmp;
        }

        /// <summary>  
        /// SetImageColorPoint 循环调用 检查新的坐标是否符合条件 符合条件会写入栈p_ColorStack 并更改颜色  
        /// </summary>  
        /// <param name="p_DataBytes">数据区</param>  
        /// <param name="p_Stride">行扫描字节数</param>  
        /// <param name="p_ColorStack">需要检查的位置栈</param>  
        /// <param name="p_X">位置X</param>  
        /// <param name="p_Y">位置Y</param>  
        /// <param name="p_OldColor">老色彩</param>  
        /// <param name="p_NewColor">新色彩</param>  
        /// <param name="p_Float">溶差</param>  
        private static void SetImageColorPoint(byte[] p_DataBytes, int p_Stride, Stack<Point> p_ColorStack, int p_X, int p_Y, Color p_OldColor, Color p_NewColor, int p_Float)
        {

            int _Index = (p_Y * p_Stride) + (p_X * 4);
            Color _OldColor = Color.FromArgb(p_DataBytes[_Index + 3], p_DataBytes[_Index + 2], p_DataBytes[_Index + 1], p_DataBytes[_Index]);

            if (ScanColor(_OldColor, p_OldColor, p_Float))
            {
                p_ColorStack.Push(new Point(p_X, p_Y));

                p_DataBytes[_Index + 3] = (byte)p_NewColor.A;
                p_DataBytes[_Index + 2] = (byte)p_NewColor.R;
                p_DataBytes[_Index + 1] = (byte)p_NewColor.G;
                p_DataBytes[_Index] = (byte)p_NewColor.B;
            }
        }

        /// <summary>  
        /// 检查色彩(可以根据这个更改比较方式  
        /// </summary>  
        /// <param name="p_CurrentlyColor">当前色彩</param>  
        /// <param name="p_CompareColor">比较色彩</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns></returns>  
        private static bool ScanColor(Color p_CurrentlyColor, Color p_CompareColor, int p_Float)
        {
            int _R = p_CurrentlyColor.R;
            int _G = p_CurrentlyColor.G;
            int _B = p_CurrentlyColor.B;

            return (_R <= p_CompareColor.R + p_Float && _R >= p_CompareColor.R - p_Float) && (_G <= p_CompareColor.G + p_Float && _G >= p_CompareColor.G - p_Float) && (_B <= p_CompareColor.B + p_Float && _B >= p_CompareColor.B - p_Float);

        }

        /// <summary>  
        /// 设置图形颜色  所有的色彩更换成新的颜色  排除Alpha通道
        /// </summary>  
        /// <param name="p_Image">图片</param>  
        /// <param name="p_OdlColor">老的颜色</param>  
        /// <param name="p_NewColor">新的颜色</param>  
        /// <param name="p_Float">溶差</param>  
        /// <returns>清理后的图形</returns>  
        public static Image SetImageColorAllWithoutAlpha(Image p_Image, Color p_OdlColor, Color p_NewColor, int p_Float)
        {
            int _Width = p_Image.Width;
            int _Height = p_Image.Height;

            Bitmap _NewBmp = new Bitmap(_Width, _Height, PixelFormat.Format32bppArgb);
            Graphics _Graphics = Graphics.FromImage(_NewBmp);
            _Graphics.DrawImage(p_Image, new Rectangle(0, 0, _Width, _Height));
            _Graphics.Dispose();

            BitmapData _Data = _NewBmp.LockBits(new Rectangle(0, 0, _Width, _Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            _Data.PixelFormat = PixelFormat.Format32bppArgb;
            int _ByteSize = _Data.Stride * _Height;
            byte[] _DataBytes = new byte[_ByteSize];
            Marshal.Copy(_Data.Scan0, _DataBytes, 0, _ByteSize);

            int _WhileCount = _Width * _Height;
            int _Index = 0;
            for (int i = 0; i != _WhileCount; i++)
            {
                Color _Color = Color.FromArgb(_DataBytes[_Index + 3], _DataBytes[_Index + 2], _DataBytes[_Index + 1], _DataBytes[_Index]);
                if (ScanColor(_Color, p_OdlColor, p_Float))
                {
                    //_DataBytes[_Index + 3] = (byte)p_NewColor.A;
                    _DataBytes[_Index + 2] = (byte)p_NewColor.R;
                    _DataBytes[_Index + 1] = (byte)p_NewColor.G;
                    _DataBytes[_Index] = (byte)p_NewColor.B;
                }
                _Index += 4;
            }
            Marshal.Copy(_DataBytes, 0, _Data.Scan0, _ByteSize);
            _NewBmp.UnlockBits(_Data);
            return _NewBmp;
        }

        /// <summary>
        /// 改变颜色深浅
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="correctionFactor"> -1.0f[加深] ~ 1.0f[变亮] </param>
        /// <returns></returns>
        public static Color ChangeColor(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            if (red < 0) red = 0;

            if (red > 255) red = 255;

            if (green < 0) green = 0;

            if (green > 255) green = 255;

            if (blue < 0) blue = 0;

            if (blue > 255) blue = 255;



            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        #endregion

        private const int blurAmount = 10;
        public static Image ImageFromText(string strText, Font fnt, Color clrFore, Color clrBack)
        {
            Bitmap bmpOut = null;
            int sunNum = 50;  //¹âÔÎµÄÖµ
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                SizeF sz = g.MeasureString(strText, fnt);
                using (Bitmap bmp = new Bitmap((int)sz.Width, (int)sz.Height))
                using (Graphics gBmp = Graphics.FromImage(bmp))
                using (SolidBrush brBack = new SolidBrush(Color.FromArgb(sunNum, clrBack.R, clrBack.G, clrBack.B)))
                using (SolidBrush brFore = new SolidBrush(clrFore))
                {
                    gBmp.SmoothingMode = SmoothingMode.HighQuality;
                    gBmp.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    gBmp.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    gBmp.DrawString(strText, fnt, brBack, 0, 0);

                    bmpOut = new Bitmap(bmp.Width + blurAmount, bmp.Height + blurAmount);

                    using (Graphics gBmpOut = Graphics.FromImage(bmpOut))
                    {
                        gBmpOut.SmoothingMode = SmoothingMode.HighQuality;
                        gBmpOut.InterpolationMode = InterpolationMode.HighQualityBilinear;
                        gBmpOut.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                        //ÒõÓ°¹âÔÎ
                        for (int x = 0; x <= blurAmount; x++)
                        {
                            for (int y = 0; y <= blurAmount; y++)
                            {
                                gBmpOut.DrawImageUnscaled(bmp, x, y);
                            }
                        }


                        gBmpOut.DrawString(strText, fnt, brFore, blurAmount / 2, blurAmount / 2);
                    }
                }
            }

            return bmpOut;
        }

        private const int bytesPerPixel = 4;

        /// <summary>
        /// Change the opacity of an image
        /// </summary>
        /// <param name="originalImage">The original image</param>
        /// <param name="opacity">Opacity, where 1.0 is no opacity, 0.0 is full transparency</param>
        /// <returns>The changed image</returns>
        public static Image ChangeImageOpacity(Image originalImage, double opacity)
        {
            if ((originalImage.PixelFormat & PixelFormat.Indexed) == PixelFormat.Indexed)
            {
                // Cannot modify an image with indexed colors
                return originalImage;
            }

            Bitmap bmp = (Bitmap)originalImage.Clone();

            // Specify a pixel format.
            PixelFormat pxf = PixelFormat.Format32bppArgb;

            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, pxf);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            // This code is specific to a bitmap with 32 bits per pixels 
            // (32 bits = 4 bytes, 3 for RGB and 1 byte for alpha).
            int numBytes = bmp.Width * bmp.Height * bytesPerPixel;
            byte[] argbValues = new byte[numBytes];

            // Copy the ARGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, argbValues, 0, numBytes);

            // Manipulate the bitmap, such as changing the
            // RGB values for all pixels in the the bitmap.
            for (int counter = 0; counter < argbValues.Length; counter += bytesPerPixel)
            {
                // argbValues is in format BGRA (Blue, Green, Red, Alpha)

                // If 100% transparent, skip pixel
                if (argbValues[counter + bytesPerPixel - 1] == 0)
                    continue;

                int pos = 0;
                pos++; // B value
                pos++; // G value
                pos++; // R value

                argbValues[counter + pos] = (byte)(argbValues[counter + pos] * opacity);
            }

            // Copy the ARGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(argbValues, 0, ptr, numBytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            return bmp;
        }
    }
}
