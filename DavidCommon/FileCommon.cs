using System;
using System.Collections.Generic;
using System.IO;

namespace DavidCommon
{
    /// <summary>
    /// 描述：文件操作
    /// 创建时间：
    /// 修改记录： 
    /// </summary>
    public class FileCommon
    {
        ///<summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">当前文件的路径</param>
        /// <returns>是否删除成功</returns>
        public static bool FilePicDelete(string path)
        {
            bool ret = false;
            FileInfo file = new FileInfo(path);
            while (file.Exists)
            {
                file.Delete();
                return true;
            }
            return ret;
        }
        /// <summary>   
        /// 用递归方法删除文件夹目录及文件   
        /// </summary>   
        /// <param name="dir">带文件夹名的路径</param>   
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之   
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件                           
                    else
                        DeleteFolder(d); //递归删除子文件夹   
                }
                Directory.Delete(dir, true); //删除已空文件夹                    
            }
        }
        /// <summary>   
        /// 获取指定目录下的所有文件夹名   
        /// </summary>   
        /// <param name="path">目录路径 物理地址</param>   
        /// <returns>string，返回所有文件夹名字</returns>   
        public static string GetAllFolder(string path)
        {
            string folder_Names = "";
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (DirectoryInfo subdir in dir.GetDirectories())
                folder_Names += subdir.FullName + "#";

            return folder_Names;
        }
        /// <summary>
        /// 获取文件的物理地址
        /// 文件的虚拟地址如"~/main/aspx.cs"
        /// 返回的是文件的物理地址
        /// </summary>
        /// <param name="sSeverPath">文件的虚拟地址如"~/main/aspx.cs"</param>
        /// <returns></returns>
        public static string GetPyPath(string sSeverPath)
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + sSeverPath.Replace("~", "");
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <returns></returns>
        public static void CreateDirectory(string fileDir)
        {
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
        }
        static List<FileInfo> lsFile = new List<FileInfo>();
        public static List<FileInfo> SearchFile(string path, string extraName)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fis = di.GetFiles();
            foreach (FileInfo fi in fis)
            {
                if (fi.Name.Substring(fi.Name.LastIndexOf(".") + 1) == extraName)
                {
                    lsFile.Add(fi);
                }
            }
            DirectoryInfo[] dis = di.GetDirectories();
            foreach (DirectoryInfo dinfo in dis)
            {
                SearchFile(dinfo.FullName, extraName);
            }
            return lsFile;
        }

        /// <summary>
        /// 多线程异步删除文件
        /// </summary>
        /// <param name="lsPath"></param>
        public static void ThreadDeleteFile(List<string> lsPath)
        {
            foreach (var path in lsPath)
            {
                FileInfo file = new FileInfo(path);
                while (file.Exists)
                {
                    file.Delete();
                }
            }


        }
    }
}
