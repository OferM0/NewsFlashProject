using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using server.Model;

namespace Utilities
{
    public class LogFile : ILogger
    {
        private int fileNumber;
        private long fileSizeLimit = 5 * 1024 * 1024 * 1024L; // 5GB
        private string path = @"C:\Users\oferm\source\repos\C learning\SEMESTER 3\NewsFlash\server\server.MicroService\bin\Debug\LogFiles";
        private string fileEnding = "txt";
        public Queue<LogItem> eventsQueue { get; set; }
        public Queue<LogItem> errorsQueue { get; set; }
        Task task;
        Task task2;
        public bool stop = false;

        public void Init()
        {
            eventsQueue = new Queue<LogItem>();
            errorsQueue = new Queue<LogItem>();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] logFiles = Directory.GetFiles(path, $"logs_*.{fileEnding}");
            int lastFileNumber = 0;
            foreach (string file in logFiles)
            {
                string fileName = Path.GetFileName(file);
                int currentFileNumber = int.Parse(fileName.Split('_')[1].Split('.')[0]);
                lastFileNumber = Math.Max(lastFileNumber, currentFileNumber);
            }
            fileNumber = lastFileNumber + 1;

            // Create the log file
            string LastfileName = $"logs_{fileNumber}.{fileEnding}";
            string fullPath = Path.Combine(path, LastfileName);
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                writer.WriteLine("Logs started");
            }

            task = Task.Run(() =>
            {
                while (!stop)
                {
                    if (eventsQueue.Count > 0)
                    {
                        LogMessage(eventsQueue.Dequeue());
                    }
                    if (errorsQueue.Count > 0)
                    {
                        LogException(errorsQueue.Dequeue());
                    }
                    Thread.Sleep(100);
                }
            });

            task2 = Task.Run(() =>
            {
                while (!stop)
                {
                    try
                    {
                        if (new FileInfo(Path.Combine(path, $"logs_{fileNumber}.{fileEnding}")).Length >= fileSizeLimit)
                        {
                            LogCheckHouseKeeping();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is FileNotFoundException)
                        {
                            // Create the file if it doesn't exist
                            using (File.Create(Path.Combine(path, $"logs_{fileNumber}. {fileEnding}"))) { }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine($"Error occured while checking the log file: {ex.Message}");
                            Console.ResetColor();
                        }
                    }
                    Thread.Sleep(3600000);
                }
            });
        }

        public void LogMessage(LogItem log)
        {
            try
            {
                string fileName = $"logs_{fileNumber}. {fileEnding}";
                string fullPath = Path.Combine(path, fileName);
                using (StreamWriter writer = new StreamWriter(fullPath, true))
                {
                    writer.WriteLine($"{log.LogTime} : {log.Type} - {log.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occured while logging event file: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void LogException(LogItem log)
        {
            try
            {
                string fileName = $"logs_{fileNumber}. {fileEnding}";
                string fullPath = Path.Combine(path, fileName);
                using (StreamWriter writer = new StreamWriter(fullPath, true))
                {
                    writer.WriteLine($"{log.LogTime} : {log.Type} - {log.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occured while logging expection file: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void LogCheckHouseKeeping()
        {
            try
            {
                string oldFileName = $"logs_{fileNumber}. {fileEnding}";
                string newFileName = $"logs_{fileNumber + 1}.{fileEnding}";
                string fullPath = Path.Combine(path, newFileName);
                using (StreamWriter writer = new StreamWriter(fullPath, true))
                {
                    writer.WriteLine("Logs started");
                    writer.WriteLine($"File size limit reached for {oldFileName}. Logs will now be written to {newFileName}.");
                }
                fileNumber++;// i changed in the end beacause- i had problem of procceses try to do same thing, log message before creation or in middle of the writings above..
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Error occured while logging expection file: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
