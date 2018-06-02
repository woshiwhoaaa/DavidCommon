using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Drawing;

namespace DavidCommon
{
    public class ImageCommon
    {
        /// <summary>
        /// 获取图片大小
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void GetImgSize(string url, out int width, out int height)
        {
            try
            {
                WebClient wc = new WebClient();
                byte[] imageBytes = wc.DownloadData(url);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image MemoryImage = Image.FromStream(ms);
                    width = MemoryImage.Width;
                    height = MemoryImage.Height;
                    MemoryImage.Dispose();
                    ms.Close();
                    ms.Dispose();
                }
            }
            catch { width = 0; height = 0; }
        }
        /// <summary>
        /// 下载自定义大小的图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savepath">保存路径</param>
        /// <param name="widthsize">宽</param>
        /// <param name="heightsize">高</param>
        /// <param name="type">0:直接下载,1://同时小于等于宽高,2://同时等于宽高,3://大于等于宽，小于等于高,4://小于等于宽，大于等于高,5://同时大于等于宽高</param>
        /// <returns></returns>
        public static bool SaveImages(string url, string savepath, int widthsize = 0, int heightsize = 0, int type = 0)
        {
            bool b = true;
            try
            {
                int width;
                int height;
                WebClient wc = new WebClient();
                byte[] imageBytes = wc.DownloadData(url);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image MemoryImage = Image.FromStream(ms);
                    width = MemoryImage.Width;
                    height = MemoryImage.Height;
                    #region 逻辑判断
                    switch (type)
                    {
                        case 0:
                            break;
                        case 1://同时小于等于宽高
                            b = width <= widthsize && height <= heightsize ? true : false;
                            break;
                        case 2://同时等于宽高
                            b = width == widthsize && height == heightsize ? true : false;
                            break;
                        case 3://大于等于宽，小于等于高
                            b = width >= widthsize && height <= heightsize ? true : false;
                            break;
                        case 4://小于等于宽，大于等于高
                            b = width <= widthsize && height >= heightsize ? true : false;
                            break;
                        case 5://同时大于等于宽高
                            b = width >= widthsize && height >= heightsize ? true : false;
                            break;
                        default:
                            b = false;
                            break;
                    }
                    #endregion
                    if (b)
                    {
                        MemoryImage.Save(savepath);   //保存
                    }

                    MemoryImage.Dispose();
                    ms.Close();
                    ms.Dispose();
                }
            }
            catch { b = false; }
            return b;
        }

    }
}
