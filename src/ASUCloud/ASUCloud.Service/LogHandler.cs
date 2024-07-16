using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASUCloud.Service
{
    public static class LogHandler
    {
        private static bool HasCreatedDir = false;
        private static readonly string SQLPATH = Path.Combine(Directory.GetCurrentDirectory(), "Log", "log.sqlite3");

        private static void CreateLogDirectory()
        {
            string currentPath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(Path.Combine(currentPath, "Log")))
            {
                Directory.CreateDirectory(Path.Combine(currentPath, "Log"));
                HasCreatedDir = true;
            }
        }

        private static Logger BuildSerilog()
        {
            if (!HasCreatedDir)
                CreateLogDirectory();

            var logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo
                .SQLite(sqliteDbPath: SQLPATH, tableName: "log")
                .CreateLogger();

            Log.Logger = logger;
            return logger;
        }

        public static void LogMessage(object? sender, EventArgs<LogEvent> e)
        {
            using var logger = BuildSerilog();
            logger.Write(e.Value.Level, e.Value.Exception, e.Value.Message);
        }

    }
}
