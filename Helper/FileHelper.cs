using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Security.Principal;

namespace SharpUtility.Helper
{
    public class FileHelper
    {
        public static void MakeDir(string directoryPath)
        {
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }            
        }

        public static void CreateFile(string filePath)
        {
            using (var fs = File.Create(filePath)) { }
        }

        public static bool IsNT()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }

        public static Dictionary<string, dynamic> ReadFileAsJson(string filePath, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;

            return JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(File.ReadAllText(filePath, encoding));
        }

        public static bool IsEqual(string filePath1, string filePath2)
        {
            FileInfo file1 = new FileInfo(filePath1);
            FileInfo file2 = new FileInfo(filePath2);

            return file1.Length == file2.Length && file1.LastWriteTime == file2.LastWriteTime;
        }

        public static bool IsZipFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length < 4)
                    {
                        return false;
                    }

                    byte[] header = new byte[4];
                    fs.Read(header, 0, 4);

                    return header[0] == 0x50 && header[1] == 0x4B
                           && header[2] == 0x03 && header[3] == 0x04;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static bool IsRegularFile(string path)
        {
            FileAttributes attrs = File.GetAttributes(path);

            if ((attrs & FileAttributes.Directory) != 0)
            {
                return false;
            }

            if ((attrs & FileAttributes.ReparsePoint) != 0)
            {
                return false;
            }

            return true;
        }

        public static long GetFileSize(string path)
        {
            return new FileInfo(path).Length;
        }

        public static bool IsSymlink(string path)
        {
            try
            {
                FileAttributes attrs = File.GetAttributes(path);

                return (attrs & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (DirectoryNotFoundException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
