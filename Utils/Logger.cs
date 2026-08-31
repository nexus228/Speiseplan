using System.Diagnostics;

namespace Speiseplan.Utils
{

    public static class Logger
    {
        public static void Info(string message)
        {
            Debug.WriteLine($"Speiseplan 🟢 [INFO] {message}");
        }

        public static void Warning(string message)
        {
            Debug.WriteLine($"Speiseplan 🟡 [WARN] {message}");
        }

        public static void Error(string message)
        {
            Debug.WriteLine($"Speiseplan 🔴 [ERROR] {message}");
        }
    }
}
