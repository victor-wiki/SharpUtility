namespace SharpUtility.Helper
{
    public class FolderHelper
    {
        public static bool HasSubFolder(string folder)
        {
            return new DirectoryInfo(folder).GetDirectories().Any();
        }
    }
}
