using System;
using System.IO;
using System.Text;

namespace AutomatedBackup
{
    class Program
    {

        public static string DestinationPath = @"C:\Users\mr2\Documents\TestBackup\";
        static void Main(string[] args)
        {

            if (Directory.Exists(DestinationPath))
            {
                DestinationPath = SaveFile(DestinationPath,DateTime.Now);
            }
            else
            {
                Console.WriteLine("Cant find backup path, have you plugged in the drive?: " + DestinationPath);
                Console.ReadKey();
                System.Environment.Exit(0);
            }

            //Creates a list of string paths to folders to be backupped
            string[] lines = File.ReadAllLines(@"C:\Users\mr2\Documents\Test\Paths.txt");
            foreach (string sourcepath in lines)
            {
                if (Directory.Exists(sourcepath))
                {
                    Console.WriteLine("Found Path to: " + sourcepath);
                    foreach (string dir in Directory.GetDirectories(sourcepath, "*", System.IO.SearchOption.AllDirectories))
                    {
                        Console.WriteLine(dir);
                        Directory.CreateDirectory(Path.Combine(DestinationPath, dir.Substring(sourcepath.Length + 1)));
                    }
                    foreach (string file_name in Directory.GetFiles(sourcepath, "*", System.IO.SearchOption.AllDirectories))
                    {
                        File.Copy(file_name, Path.Combine(DestinationPath, file_name.Substring(sourcepath.Length + 1)));
                    }
                }
                else
                {
                    Console.WriteLine("Path Doesnt Exist: " + sourcepath);
                }
            }

            Console.WriteLine("Done, press anykey to finish");
            Console.ReadKey();
            //SaveFile(DateTime.Now);
        }
        public static string SaveFile(string Path, DateTime dateTime)
        {
            string new_path = Path + dateTime.Date.ToString("yyyyMMdd") + "Backuptest";
            Directory.CreateDirectory(new_path);
            return new_path;
        }
    }    
}
