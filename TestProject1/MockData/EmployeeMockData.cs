using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.MockData
{
    public class EmployeeMockData
    {
        public static AddEmployee GetEmployeeMockData() 
        {
            return new AddEmployee
            {
                Name = "Test",
                Email="kraza0000@gmail.com",
                DoB= DateTime.Now,
                Department = "Red",
            };
        
        
        
        }


        public static AddEmployee AddEmployeeMockData()
        {
            return new AddEmployee
            {
                Name = "",
                Email = "kraza0000",
                DoB = DateTime.Now,
                Department = "Red",
            };



        }

    }
}
