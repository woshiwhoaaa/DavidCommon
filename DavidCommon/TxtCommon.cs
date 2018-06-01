using System.IO;

namespace DavidCommon
{
    public class TxtCommon
    {
        /// <summary>
        /// 向log文件夹写txt日志(追加)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="filenamepath">存储路径以及.txt</param>
        /// <param name="IsNewline">默认0，换行，其他值不换行</param>
        public static void AppendToTxt(string str, string filenamepath,int IsNewline=0)
        {
            try
            {
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(str + (IsNewline==0?"\r\n":""));
                using (FileStream fsWrite = new FileStream(filenamepath, FileMode.Append))
                {
                    fsWrite.Write(myByte, 0, myByte.Length);
                };
            }
            catch { }
        }
        /// <summary>
        /// 创建一个文件，存在则覆盖
        /// </summary>
        /// <param name="str"></param>
        /// <param name="filenamepath"></param>
        public static void CreateToTxt(string str, string filenamepath)
        {
            try
            {
                byte[] myByte = System.Text.Encoding.UTF8.GetBytes(str);
                using (FileStream fsWrite = new FileStream(filenamepath, FileMode.Create))
                {
                    fsWrite.Write(myByte, 0, myByte.Length);
                };
            }
            catch { }
        }


    }
}
