<# Cleanup & Rebuild script

- deletes obj and bin folder
- clean solution
- build solution
- delete app from android emulator

manual delet app from android emulator:
& "C:\Program Files (x86)\Android\android-sdk\platform-tools\adb.exe" -s emulator-5554 uninstall oz.maui.training
#>


# Define the path to your project and solution file
$projectPath = "D:\Entw\VS22\maui-training\maui-training"
$solutionPath = "D:\Entw\VS22\maui-training\maui-training.sln"


function Write-Log {
    param (
        [string]$message
    )
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Write-Output "$timestamp - $message"
}


Write-Log "---------------- Deleting bin and obj folders... ----------------"
Get-ChildItem -Path $projectPath -Include bin, obj -Recurse | Remove-Item -Recurse -Force
Write-Log "Deleted bin and obj folders."

Write-Log "---------------- Running dotnet clean... ----------------"
dotnet clean $solutionPath
Write-Log "Completed dotnet clean."

Write-Log "---------------- Running dotnet build... ----------------"
dotnet build $solutionPath
Write-Log "Completed dotnet build."


Write-Log "---------------- Uninstalling app from emulatgor... ----------------"
# Path to Android SDK can be found in Xamarin options in vs 2022
$androidSdkPath = "C:\Program Files (x86)\Android\android-sdk"

# App package name can be found in project in Platforms\Android\AndroidManifest.xml
$appPackageName = "oz.maui.training"

# Uninstall the app
& "$androidSdkPath\platform-tools\adb.exe" -s emulator-5554 uninstall $appPackageName
Write-Log "Uninstalling app from emulatgor done."



# Emulator name can be found in android device manager (show in explorer and use directory name)
# $emulatorName = "pixel_3_q_10_0_-_api_29.avd"

# # To wipe the emulator device completely:
# & "C:\Path\To\Your\Android\Sdk\emulator\emulator.exe" -avd Your_Emulator_Name -wipe-data
