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
        var statusCameraPermission = await CheckAndRequestCameraPermission();
        var statusWritePermission = await CheckAndRequestWritePermission();
        if (statusCameraPermission == PermissionStatus.Granted && statusWritePermission == PermissionStatus.Granted)
        {
            try
            {
                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    var uniqueFileName = $"{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                    using var sourceStream = await photo.OpenReadAsync();

                    await SaveFile(uniqueFileName, sourceStream);
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

    private async Task SaveFile(string fileName, Stream stream)
    {
        using var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;

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

    private async Task<PermissionStatus> CheckAndRequestWritePermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageWrite>();
        }
        return status;

    }
}
