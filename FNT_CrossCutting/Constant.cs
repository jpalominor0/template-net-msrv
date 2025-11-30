
namespace FNT_CrossCutting
{
    public static class Constants
    {
        public const int Active = 1;
        public const int Inactive = 0;
        public const string DescActive = "Activo";
        public const string DescInactive = "Inactivo";
        public const string DatabaseConnection = "DefaultConnection";
        public const string ExcelExtension = ".xlsx";
        public const string ExcelExtensionOld = ".xls";
        public const int NoDatabaseChanges = 0;
        public const string Proveedor = "Proveedor";
        public const string Negation = "NO";
        public const string Afirmative = "YES";
        public static bool IsInDevelopment { get; set; } = false;

        public const string OneDayCache = "1_DAY_CACHE";
        public const string CorsPolicy = "CorsPolicy";
        public const string AntiForgery = "X-XSRF-TOKEN";
        public const string SwaggerJsonPath = "../swagger/V1/swagger.json";
        public const string HeaderErrorMessage = "Error-Message";
        public static string JWT { get; set; } = "";
        public static string QueryString{ get; set; } = "";
        public static string RouteParams { get; set; } = "";
        public static string TraceIdentifier { get; set; } = "";
        public static string RequestBody { get; set; } = "";
        public static string Username { get; set; } = "";
        public static readonly string TimeZoneWindows = "SA Pacific Standard Time";
        public static readonly string TimeZoneLinux = "America/Lima";
        
    }
}
