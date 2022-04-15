using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App_Classes
{
    public static class dbConnection
    {
        public static bool GetConnection(out SqlConnection conn)
        {
            string dir = Directory.GetCurrentDirectory();//mdf

            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dir + @"\DB\TestProject.mdf;Integrated Security=True; Connect Timeout = 30";//mdf

            //const string connectionString = @"C:\Users\MIKHEEV_AV1\source\repos\Polyclinic\Database1.mdf";
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                Console.WriteLine("Подключение открыто");
                return true;
            }
            catch (SqlException ex)
            {
                Logs.LogWriteError(ex.Message);
            }
            conn = null;
            return false;
        }
    }
}
