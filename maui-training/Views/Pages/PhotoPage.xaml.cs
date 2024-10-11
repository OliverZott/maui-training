using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;

namespace maui_training.Views.Pages;


public partial class PhotoPage : ContentPage
{
    public PhotoPage()
    {
        InitializeComponent();

        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        UpdateOrientationState(DeviceDisplay.MainDisplayInfo.Orientation);
    }

    private void OnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        UpdateOrientationState(e.DisplayInfo.Orientation);
    }

    private void UpdateOrientationState(DisplayOrientation orientation)
    {
        if (orientation == DisplayOrientation.Portrait)
        {
            VisualStateManager.GoToState(this, "Portrait");
        }
        else
        {
            VisualStateManager.GoToState(this, "Landscape");
        }
    }

    private async void OnTakePhotoButtonClicked(object sender, EventArgs e)
    {
        var status = await CheckAndRequestCameraPermission();
        if (status == PermissionStatus.Granted)
        {
            try
            {
                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    var statusReadPermission = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                    var statusWritePermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                    var mediaPermissionStatus = await Permissions.CheckStatusAsync<Permissions.Media>();

                    //var cacheDir = FileSystem.Current.CacheDirectory;
                    //var appDataDir = FileSystem.Current.AppDataDirectory;
                    //var commonPictures = Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures);    
                    //var myPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);



                    // ###################################################################################################### 

                    var uniqueFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                    //var photoStoragePath = Path.Combine(myPictures, uniqueFileName);

                    var cancellationToken = new CancellationToken();
                    using var sourceStream = await photo.OpenReadAsync();

                    //using var stream = new MemoryStream(Encoding.Default.GetBytes("Hello from the Community Toolkit!"));
                    var fileSaverResult = await FileSaver.Default.SaveAsync(uniqueFileName, sourceStream, cancellationToken);
                    if (fileSaverResult.IsSuccessful)
                    {
                        await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show(cancellationToken);
                    }
                    else
                    {
                        await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show(cancellationToken);
                    }





                    //// ######################################################################################################     
                    //#if ANDROID
                    //                    var visiblePath = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments).AbsoluteFile.Path.ToString();
                    //                    var storagePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                    //                    var storagePath2 = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
                    //                    var docsDirectory = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments);

                    //                    string localFilePath = Path.Combine(storagePath2, uniqueFileName);

                    //                    using var sourceStream = await photo.OpenReadAsync();
                    //                    using var localFileStream = File.OpenWrite(localFilePath);  

                    //                    await sourceStream.CopyToAsync(localFileStream);

                    //#endif



                    //// ######################################################################################################                       
                    //string localFilePath = Path.Combine(FileSystem.AppDataDirectory, "photo.jpg");     // photo.

                    //using var sourceStream = await photo.OpenReadAsync();
                    //using var localFileStream = File.OpenWrite(photoStoragePath);   // localFilePath
                    ////using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);  // more control but more complex

                    //await sourceStream.CopyToAsync(localFileStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        else
        {
            await DisplayAlert("Permission Denied", "Camera permission is required to take photos.", "OK");
        }
    }

    async Task SaveFile(CancellationToken cancellationToken, string fileName, Stream stream)
    {
        //using var stream = new MemoryStream(Encoding.Default.GetBytes("Hello from the Community Toolkit!"));
        var fileSaverResult = await FileSaver.Default.SaveAsync(fileName, stream, cancellationToken);
        if (fileSaverResult.IsSuccessful)
        {
            await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show(cancellationToken);
        }
        else
        {
            await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show(cancellationToken);
        }
    }

    private async Task<PermissionStatus> CheckAndRequestCameraPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Camera>();
        }
        return status;
    }

}
