using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GaussLink.Data.DirStruct
{
    public class DirectoryStructure
    {
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            var items = new List<DirectoryItem>();

            #region Get Folders

            try
            {
                var dirs = Directory.GetDirectories(fullPath);


                if (dirs.Length > 0)
                {
                    foreach (string s in dirs)
                    {
                        if (!s.Contains("$Win") && !s.Contains("$RE") && !s.Contains("System Volume"))
                        {
                            items.Add(new DirectoryItem { FullPath = s, Type = DirectoryItemType.Folder });
                        }
                    }
                }
            }
            catch { }

            #endregion

            #region Get Files
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                {
                    foreach (string s in fs)
                    {
                        if (!s.EndsWith(".sys") && s.EndsWith(".out"))
                        {
                            items.Add(new DirectoryItem { FullPath = s, Type = DirectoryItemType.File });
                        }
                    }
                }
            }
            catch { }

            #endregion

            return items;
        }

        #region Helpers


        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;
            string s = path.Substring(lastIndex + 1);
            return s;
        }

        #endregion
    }
}

