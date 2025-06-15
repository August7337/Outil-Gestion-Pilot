using System.IO;

namespace Outil_Gestion_Pilot.Models
{
    public class LogError
    {
        public static void Log(Exception ex, string msg)
        {
            string logFile = "error.log";
            string content = $"{DateTime.Now}:{msg}\n {ex.Message}\n{ex.StackTrace}\n";
            try
            {
                File.AppendAllText(logFile, content);
            }
            catch
            { }
        }
    }
}
