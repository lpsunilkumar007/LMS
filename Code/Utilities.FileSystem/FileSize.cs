
//using System.IO;

//namespace Utilities
//{
//    public enum SizeIn
//    {
//        MB
//    }
//    public static class FileSize
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="path"></param>
//        /// <param name="isAbsolutePath">if path is : C:\Windows\calc.exe then true</param>
//        /// <returns></returns>
//        public static double GetfileSize(string path, SizeIn sizeIn)
//        {             
//            if (!File.Exists(path)) return 0;
//            var f = new FileInfo(path);
//            if (SizeIn.MB == sizeIn)
//            {
//                return ConvertBytesToMegabytes(f.Length);
//            }
//            return 0;
//        }       
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="bytes"></param>
//        /// <returns></returns>
//        public static double ConvertBytesToMegabytes(long bytes)
//        {
//            return (bytes / 1024f) / 1024f;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="kilobytes"></param>
//        /// <returns></returns>
//        public static double ConvertKilobytesToMegabytes(long kilobytes)
//        {
//            return kilobytes / 1024f;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="bytes"></param>
//        /// <returns></returns>
//        public static double ConvertBytesToMegabytes(double bytes)
//        {
//            return (bytes / 1024f) / 1024f;
//        }

//    }
//}
