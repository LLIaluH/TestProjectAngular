using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_Classes
{
    public class Employee
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfEmployment { get; set; }

        private double _wage;
        public double Wage {
            get => _wage;
            set => _wage = (double)Math.Round(value, 2); 
        }

        public bool Validate()
        {
            var ageEmployee = Support.GetAge(DateOfBirth, DateTime.Now);
            var ageAtStartOfWork = Support.GetAge(DateOfBirth, DateOfEmployment);
            return (ageEmployee < 18 || ageAtStartOfWork < 18 || Department.Length < 3 || Name.Length < 8 || _wage < 0);
        }
    }
}
