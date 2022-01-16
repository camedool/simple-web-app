namespace SimpleWebApp.WebApi.Settings;

/// <summary>
/// Common interface for any appsettings option.
/// </summary>
public interface IAppSetting
{
    /// <summary>
    /// Path to  appsettings option.
    /// </summary>
    static string AppSettingsPath => "This is default appsettings path. Overwrite it!";
}
