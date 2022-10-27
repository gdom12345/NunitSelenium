namespace NunitSelenium.Framework
{
    internal class FileUtils
    {
        public static string getResourceFile(string resourcePath)
        {
            if (!resourcePath.StartsWith("\\"))
            {
                resourcePath = "\\" + resourcePath;
            }

            string fileData = File.ReadAllText(getResourcesFolder() + resourcePath);
            return fileData;
        }

        public static String getResourcesFolder()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string sourceDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            return sourceDirectory + "\\Resources";
        }

    }
}
