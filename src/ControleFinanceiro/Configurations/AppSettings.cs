namespace ControleFinanceiro.Configurations
{
    public class AppSettings
    {
        private static string DatabaseName = "ControleFinanceiro.db";
        private static string DatabaseDirectory = FileSystem.AppDataDirectory;
        public static string DatabasePath = Path.Combine(DatabaseDirectory, DatabaseName);
    }
}
