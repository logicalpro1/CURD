using Demo.Data;
using Demo.Enums;
using Demo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Xml.Linq;

namespace Demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeesApiDbContext dbContext;
        public EmployeeController(EmployeesApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var data= await dbContext.Employees.ToListAsync();
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.Success.ToString();
                responseModel.Data = data;
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
               
                responseModel.StatusCode = 500;
                responseModel.Message = MessagesEnum.Failed.ToString();
                responseModel.Data = "Internal server error"+ex;
                return Ok(responseModel);
            }
        }

        [HttpGet("SearchEmployeebyName")]
        public async Task<IActionResult> SearchEmployeebyName(string Name)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var emp = await dbContext.Employees.Where(s => s.Name == Name).ToListAsync();
                if (emp != null)
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Success.ToString();
                    responseModel.Data = emp;
                    return Ok(responseModel);
                }
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.NotExist.ToString();
                responseModel.Data = NotFound();
                return Ok(responseModel);

            }
            catch (Exception ex)
            {

                responseModel.StatusCode = 500;
                responseModel.Message = MessagesEnum.Failed.ToString();
                responseModel.Data = "Internal server error" + ex;
                return Ok(responseModel);
            }


           
        }


        private static bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }




        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(AddEmployee obj) 
        {

            ResponseModel responseModel = new ResponseModel();
            try
            {
                bool validEmail = IsValidEmail(obj.Email);
                if (validEmail && obj.Name.Length > 0 && obj.Department.Length > 0 && obj.DoB < DateTime.Now)
                {
                     var Employee = new Employee()
                    {
                        Id = Guid.NewGuid(),
                        Name = obj.Name,
                        Email = obj.Email,
                        DoB = obj.DoB,
                        Department = obj.Department,
                    };
                    await dbContext.Employees.AddAsync(Employee);
                    await dbContext.SaveChangesAsync();

                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Success.ToString();
                    responseModel.Data = Employee;
                    return Ok(responseModel);
                      
                }
                else
                {
                    responseModel.StatusCode = 403;
                    responseModel.Message = MessagesEnum.Failed.ToString();
                    responseModel.Data = "Invalid input, Please enter Name, Email, Department, Date of birth correctly";
                    return Ok(responseModel);
                }


              
               
            }
            catch (Exception ex)
            {

                responseModel.StatusCode = 500;
                responseModel.Message = MessagesEnum.Failed.ToString();
                responseModel.Data = "Internal server error" + ex;
                return Ok(responseModel);
            }
           
        }


        [HttpPut("EditEmployee")]
        public async Task<IActionResult> EditEmployee([FromBody] Employee obj)
        {

            ResponseModel responseModel = new ResponseModel();
            try
            {
                bool validEmail = IsValidEmail(obj.Email);
                if (validEmail && obj.Name.Length > 0 && obj.Department.Length > 0 && obj.DoB < DateTime.Now)
                {


                    var emp = await dbContext.Employees.FindAsync(obj.Id);
                    if (emp != null)
                    {
                        emp.Name = obj.Name;
                        emp.Email = obj.Email;
                        emp.DoB = obj.DoB;
                        emp.Department = obj.Department;
                        await dbContext.SaveChangesAsync();
                        responseModel.StatusCode = 200;
                        responseModel.Message = MessagesEnum.Success.ToString();
                        responseModel.Data = emp;
                        return Ok(responseModel);
                    }
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.NotExist.ToString();
                    responseModel.Data = NotFound();
                    return Ok(responseModel);
                }
                else
                {
                    responseModel.StatusCode = 403;
                    responseModel.Message = MessagesEnum.Failed.ToString();
                    responseModel.Data = "Invalid input, Please enter Name, Email, Department, Date of birth correctly ";
                    return Ok(responseModel);
                }

            }
            catch (Exception ex)
            {

                responseModel.StatusCode = 500;
                responseModel.Message = MessagesEnum.Failed.ToString();
                responseModel.Data = "Internal server error" + ex;
                return Ok(responseModel);
            }

        }

        [HttpDelete("DeleteEmployee/{guid:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid guid)
        {

            ResponseModel responseModel = new ResponseModel();
            try
            {
                var emp = await dbContext.Employees.FindAsync(guid);
                if (emp != null)
                {
                    dbContext.Remove(emp);
                    await dbContext.SaveChangesAsync();
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Success.ToString();
                    responseModel.Data = emp;
                    return Ok(responseModel);
                }
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.NotExist.ToString();
                responseModel.Data = NotFound();
                return Ok(responseModel);

            }
            catch (Exception ex)
            {

                responseModel.StatusCode = 500;
                responseModel.Message = MessagesEnum.Failed.ToString();
                responseModel.Data = "Internal server error" + ex;
                return Ok(responseModel);
            }

            
        }


        [HttpGet("SearchEmployeebyEmail")]
        public async Task<IActionResult> SearchEmployeebyEmail(string Email)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var emp = await dbContext.Employees.Where(s => s.Email == Email).ToListAsync();
                if (emp != null)
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Success.ToString();
                    responseModel.Data = emp;
                    return Ok(responseModel);
                }
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.NotExist.ToString();
                responseModel.Data = NotFound();
                return Ok(responseModel);

            }
            catch (Exception ex)
            {

                responseModel.StatusCode = 500;
                responseModel.Message = MessagesEnum.Failed.ToString();
                responseModel.Data = "Internal server error" + ex;
                return Ok(responseModel);
            }



        }

        [HttpGet("SearchEmployeebyDeparment")]
        public async Task<IActionResult> SearchEmployeebyDeparment(string Department)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var emp = await dbContext.Employees.Where(s => s.Department == Department).ToListAsync();
                if (emp != null)
                {
                    responseModel.StatusCode = 200;
                    responseModel.Message = MessagesEnum.Success.ToString();
                    responseModel.Data = emp;
                    return Ok(responseModel);
                }
                responseModel.StatusCode = 200;
                responseModel.Message = MessagesEnum.NotExist.ToString();
                responseModel.Data = NotFound();
                return Ok(responseModel);

            }
            catch (Exception ex)
            {

                responseModel.StatusCode = 500;
                responseModel.Message = MessagesEnum.Failed.ToString();
                responseModel.Data = "Internal server error" + ex;
                return Ok(responseModel);
            }



        }



    }
}
