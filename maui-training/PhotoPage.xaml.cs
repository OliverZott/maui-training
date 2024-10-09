namespace maui_training;

// TODO permission check for storage read / write


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

        // functionality to check all permissions

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
                    // Do something with the photo, e.g., display it or save it
                    var stream = await photo.OpenReadAsync();
                    // Process the stream
                    var filePath = Path.Combine(FileSystem.AppDataDirectory, "capturedPhoto.jpg");
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
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
