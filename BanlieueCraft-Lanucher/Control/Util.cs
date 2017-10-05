using System;
using System.IO;
using System.Threading;

namespace BanlieueCraft_Lanucher.Control
{
    public class Util
    {
        public static void DeleteFileIfExists(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (IOException)
                {
                    Thread.Sleep(500);
                    File.Delete(filePath);
                }
                catch (UnauthorizedAccessException)
                {
                    FileAttributes attributes = File.GetAttributes(filePath);
                    if ((attributes & FileAttributes.ReadOnly).Equals(FileAttributes.ReadOnly))
                    {
                        File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                        File.Delete(filePath);
                    }
                }
            }
        }
    }
}
