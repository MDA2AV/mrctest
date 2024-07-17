using Android.App;
using Android.Content.Res;
using Android.Runtime;
using System.Diagnostics;

namespace MRCTest
{
    [Application(UsesCleartextTraffic = true)]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            AssetManager assets = Platform.AppContext.Assets;

            List<string> dynamicList = new List<string>();
            GetAllStaticFiles(assets.List(""), dynamicList, "");

            CreateDirectories();
            CopyFiles(dynamicList);
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public List<string> GetAllStaticFiles(string[] elements, List<string> dynamicList, string baseDir){
            //For all elements if there is a file in it, get all files in it and add to global list
            foreach (string element in elements){
                //Debug.WriteLine($"Element: {element}");
                AssetManager assets = Platform.AppContext.Assets;
                string[] aux = assets.List(baseDir + element);
                if(aux.Length == 0){
                    //Debug.WriteLine($"Adding element {baseDir + element}");
                    dynamicList.Add(baseDir + element);
                }else{
                    //Debug.WriteLine("It is a folder, going inside element..");
                    GetAllStaticFiles(aux, dynamicList, baseDir + element + "/");
                }
            }
            return dynamicList;
        }
        public async Task CopyFiles(List<string> dynamicList)
        {
            foreach (string file in dynamicList)
            {
                //Debug.WriteLine($"File: {file}");
                if (File.Exists(FileSystem.Current.AppDataDirectory + "/" + file))
                {
                    //Debug.WriteLine($"Already exists, continuing..");
                    continue;
                }
                try
                {
                    // Open the source file
                    using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(file);
                    //Debug.WriteLine($"Creating file {Path.Combine(FileSystem.Current.AppDataDirectory, file)}");
                    // Create an output filename
                    string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, file);
                    // Copy the file to the AppDataDirectory
                    using FileStream outputStream = File.Create(targetFile);
                    await inputStream.CopyToAsync(outputStream);
                }catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }
        public void CreateDirectories()
        {
            // Get the path to the AppDataDirectory
            string appDataDirectory = FileSystem.AppDataDirectory;

            List<string> directories = ["assets",
                "assets/fonts",
                "assets/i18n",
                "assets/images",
                "assets/images/equipment",
                "assets/images/flags",
                "assets/images/icons",
                "assets/images/molecules",
                "assets/images/molecules/map",
                "assets/images/molecules/map/images",
                "assets/img",
                "webkit",
                "images"
            ];

            foreach (string dir in directories)
            {
                string folderPath = Path.Combine(appDataDirectory, dir);
                //Debug.WriteLine($"Adding directory: {folderPath}");
                Directory.CreateDirectory(folderPath);
                //Debug.WriteLine($"Added!");
            }
        }
    }
}
