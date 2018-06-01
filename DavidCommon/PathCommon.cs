using System;

namespace DavidCommon
{
    public class PathCommon
    {
        /// <summary>
        /// 获取该模块所在的路径
        /// </summary>
        /// <param name="type">不传默认返回该模块所在的路径，其他返回上层目录</param>
        /// <returns></returns>
        public static string GetProcessPath(int type = 0)
        {
            try
            {
                string basePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                basePath = basePath.Substring(0, basePath.LastIndexOf("\\"));//本程序所在目录
                if (type != 0)//本程序所在目录上级目录
                {
                    basePath = basePath.Substring(0, basePath.LastIndexOf("\\"));
                }
                return basePath;
            }
            catch (Exception ex) { return ""; }
        }
        /// <summary>
        /// 获取当前模块所在文件夹保存路径
        /// </summary>
        /// <param name="filename">文件名字</param>
        /// <param name="suffix">后缀</param>
        /// <param name="type">不传默认返回该模块所在的路径，其他返回上层目录</param>
        /// <returns></returns>
        public static string GetSaveFileNamePath(string filename, string suffix, int type = 0)
        {
            try{ return string.Format(suffix.Contains(".")?"{0}\\{1}{2}": "{0}\\{1}.{2}", GetProcessPath(type), filename, suffix);}
            catch (Exception ex) { return ""; }

        }
    }
}
