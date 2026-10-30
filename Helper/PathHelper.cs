namespace SharpUtility.Helper
{
    public class PathHelper
    {
        public static string GetCommonPath(IEnumerable<string> paths)
        {
            var items = paths.Select(item => item.Split('/', '\\')).ToList();
            var maxLength = items.Max(item => item.Length);

            List<string> commons = new List<string>();

            for (var i = 0; i < maxLength; i++)
            {
                string current = null;

                var valid = true;

                foreach (var item in items)
                {
                    if (item.Length > i)
                    {
                        if (current == null)
                        {
                            current = item[i];
                        }
                        else if (current.ToLower() != item[i].ToLower())
                        {
                            valid = false;
                        }
                    }
                    else
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid && current != null)
                {
                    commons.Add(current);
                }
            }

            return commons.Count > 0 ? string.Join(Path.DirectorySeparatorChar, commons) : null;
        }

        public static string GetFileRealPath(string path)
        {
            FileInfo info = new FileInfo(path);

            string link = info.LinkTarget;

            if (link != null)
            {
                FileSystemInfo? target = File.ResolveLinkTarget(path, true);

                return target.FullName;
            }

            return info.FullName;
        }

        public static IEnumerable<string> GetParents(string path)
        {
            string current = Path.GetDirectoryName(path);

            while (current != null)
            {
                yield return current;

                current = Path.GetDirectoryName(current);
            }
        }

        public static string ResolvePath(string cacheDir)
        {
            string expanded = ExpandUser(cacheDir);

            return Path.GetFullPath(expanded);
        }

        private static string ExpandUser(string path)
        {
            if (string.IsNullOrEmpty(path) || !path.StartsWith("~"))
            {
                return path;
            }

            string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (path.Length == 1)
            {
                return userHome;
            }

            if (path == "/" || path == "\\")
            {
                return Path.Combine(userHome, path.Substring(2));
            }

            return path;
        }

        public static List<string> GetAllParentDirectories(string path)
        {
            List<string> result = new List<string>();
            DirectoryInfo? dirInfo = Directory.GetParent(path);

            while (dirInfo != null)
            {
                result.Add(dirInfo.FullName);
                dirInfo = dirInfo.Parent;
            }

            return result;
        }
    }
}
