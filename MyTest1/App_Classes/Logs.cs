using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace App_Classes
{
    public static class Logs
    {
        const string ErrorLogPath = "ErrorLog.txt";
        static string path = Directory.GetCurrentDirectory() + @"/LogFiles/";

    public static void LogWriteError(string exText)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            try
            {
                using (StreamWriter sw = new StreamWriter(path + ErrorLogPath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(date + " | " + exText + "\n");
                }
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
