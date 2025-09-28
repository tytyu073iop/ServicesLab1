using System.IO;

public static class PathHelper
{

    public static string GetFilesDirectory(string fileName)
    {
        // Get the application's base directory
        string baseDirectory = AppContext.BaseDirectory;

        // Navigate up to the project's parent folder
        // The number of .Parent calls needed depends on your build output structure
        string projectParentDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.Parent.FullName;

        string databasePath = Path.Combine(projectParentDirectory, fileName);
        return databasePath;
    }
}