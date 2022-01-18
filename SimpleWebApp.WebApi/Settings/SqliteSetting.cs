namespace SimpleWebApp.WebApi.Settings;

public sealed class SqliteSetting : IAppSetting
{
    public static string AppSettingsPath => "Sqlite";
    public string FileName { get; set; } = string.Empty;
}
