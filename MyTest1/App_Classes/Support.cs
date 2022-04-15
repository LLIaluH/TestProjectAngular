using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App_Classes
{
    public static class Support
    {
        //TODO:
        //Доработать универсальный метод
        //public static IEnumerable<T> GetIEnum<T>(SqlDataReader reader)
        //{
        //    List<T> list = new List<T>();
        //    Type typeParameterType = typeof(T);
        //    PropertyInfo[] properties = typeParameterType.GetProperties(BindingFlags.Instance);

        //    while (reader.Read())
        //    {
        //        List<object> param = new List<object>();
        //        for (int i = 0; i < reader.FieldCount; i++)
        //        {
        //            param.Add(reader.GetValue(i));
        //        }
        //        list.Add((T)reader[0]);
        //    }
        //    return list.ToArray<T>();
        //}

        public static int GetAge(DateTime d1, DateTime d2)
        {
            var r = d2.Year - d1.Year;
            return d1.AddYears(r) <= d2 ? r : r - 1;
        }

        public static IEnumerable<Employee> GetIEnum(SqlDataReader reader)
        {
            List<Employee> list = new List<Employee>();

            while (reader.Read())
            {
                Employee em = new Employee();
                em.Id =                 (int)reader.GetValue(0);
                em.Department =         reader.GetValue(1).ToString();
                em.Name =               reader.GetValue(2).ToString();
                em.DateOfBirth =        (DateTime)reader.GetValue(3);
                em.DateOfEmployment =   (DateTime)reader.GetValue(4);
                em.Wage =               (double)reader.GetDecimal(5);
                list.Add(em);
            }
            return list.ToArray<Employee>();
        }
    }
}

