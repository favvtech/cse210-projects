using System;
using System.Globalization;
using System.IO;

namespace Mindfulness
{
    public sealed class SessionLogger
    {
        private readonly string _logPath;

        public SessionLogger(string logPath)
        {
            _logPath = logPath;
        }

        public void Log(string activityName, int durationSeconds, DateTime startUtc, DateTime endUtc)
        {
            var line = string.Join(",",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                activityName.Replace(',', ';'),
                durationSeconds.ToString(CultureInfo.InvariantCulture),
                startUtc.ToString("o", CultureInfo.InvariantCulture),
                endUtc.ToString("o", CultureInfo.InvariantCulture)
            );

            var directory = Path.GetDirectoryName(_logPath);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.AppendAllText(_logPath, line + Environment.NewLine);
        }
    }
}


