namespace WebApp;

public static class Config
{
    public static IConfiguration Configuration { get; set; }
    public static string? Api => Configuration["Api"];
    public static string GetApiUrl(string path) => $"{Api}/api/{path}";

}