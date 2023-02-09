using Demo.Controllers;
using Demo.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.MockData;

namespace TestProject1.TestController
{
    public class TestEmployeeController: IDisposable
    {

        private readonly EmployeesApiDbContext _dbContext;


        public TestEmployeeController()
        {
            var options = new DbContextOptionsBuilder<EmployeesApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _dbContext = new EmployeesApiDbContext(options);
            _dbContext.Database.EnsureCreated();
        }

       

        [Fact]
        public async Task GetEmployees_ShouldReturn200Status()
        {
           
            var sut = new EmployeeController(_dbContext);
            var result = await sut.GetEmployees();

            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);


        }


        [Fact]
        public async Task AddEmployee_ShouldReturn200Status()
        {

            //Act
            var sut = new EmployeeController(_dbContext);
            var result = await sut.AddEmployee(EmployeeMockData.GetEmployeeMockData());

            //Assert
            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(200);


        }


        [Fact]
        public async Task AddEmployee_ShouldReturn403Status()
        {

            //Act
            var sut = new EmployeeController(_dbContext);
            var result = await sut.AddEmployee(EmployeeMockData.AddEmployeeMockData());

            //Assert



            result.GetType().Should().Be(typeof(OkObjectResult));
            (result as OkObjectResult).StatusCode.Should().Be(403);


        }

     





        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();   
        }

    }
}
