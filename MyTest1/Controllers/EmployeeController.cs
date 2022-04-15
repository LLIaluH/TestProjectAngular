using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App_Classes;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace MyTest1.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("Employee")]
    public class EmployeeController : ControllerBase
    {
        //private readonly ILogger<EmployeeController> _logger;

        SqlConnection conn;
        SqlCommand cmd;
        private bool GetCon()
        {
            return dbConnection.GetConnection(out conn);
        }

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            //_logger = logger;
        }

        [HttpPost]
        [Route("GetOnFilter")]
        public IEnumerable<Employee> GetOnFilter(Employee e)//GetAllEmployee
        {
            if (GetCon())
            {
                var nulldatetime = Convert.ToDateTime("01.01.0001 00:00:00");
                string query;
                cmd = conn.CreateCommand();
                query = "SELECT * FROM Employee WHERE " +
                    "Department LIKE (N'%" + e.Department + "%') " +
                    "AND Name LIKE (N'%" + e.Name + "%') ";
                if (e.Wage != 0)
                {
                    query += "AND Wage LIKE (N'%" + e.Wage + "%') ";
                }
                if (e.DateOfBirth != nulldatetime)
                {
                    query += " AND DateOfBirth BETWEEN @date1 AND @date2 ";
                    cmd.Parameters.AddWithValue("date1", e.DateOfBirth.AddDays(-1));
                    cmd.Parameters.AddWithValue("date2", e.DateOfBirth.AddDays(1));
                }
                if (e.DateOfEmployment != nulldatetime)
                {
                    query += " AND DateOfEmployment BETWEEN @date3 AND @date4 ";
                    cmd.Parameters.AddWithValue("date3", e.DateOfEmployment.AddDays(-1));
                    cmd.Parameters.AddWithValue("date4", e.DateOfEmployment.AddDays(1));
                }
                cmd.CommandText = query;
                try
                {
                    var reader = cmd.ExecuteReader();
                    return Support.GetIEnum(reader);
                }
                catch (Exception ex)
                {
                    Logs.LogWriteError(ex.Message);
                    throw;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()//GetAllEmployee
        {
            if (GetCon())
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employee";
                try
                {
                    var reader = cmd.ExecuteReader();
                    return Support.GetIEnum(reader);
                }
                catch (Exception ex)
                {
                    Logs.LogWriteError(ex.Message);
                    throw;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (GetCon())
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Employee WHERE id=@id";
                cmd.Parameters.AddWithValue("id", id);
                try
                {
                    var reader = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logs.LogWriteError(ex.Message);
                    return StatusCode(500);
                    throw;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return Ok();
        }

        [HttpPut]
        [Route("Insert")]
        public IActionResult Post(Employee e)
        {
            if (e.Validate())
            {
                return StatusCode(500);
            }
            if (GetCon())
            {
                dynamic reslult = new System.Dynamic.ExpandoObject();
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Employee (Department, Name, DateOfBirth, DateOfEmployment, Wage) VALUES (@Department, @Name, @DateOfBirth, @DateOfEmployment, @Wage)";
                cmd.Parameters.AddWithValue("Department", e.Department);
                cmd.Parameters.AddWithValue("Name", e.Name);
                cmd.Parameters.AddWithValue("DateOfBirth", e.DateOfBirth);
                cmd.Parameters.AddWithValue("DateOfEmployment", e.DateOfEmployment);
                cmd.Parameters.AddWithValue("Wage", e.Wage);
                try
                {
                    cmd.ExecuteNonQuery();
                    return StatusCode(200);
                }
                catch (Exception ex)
                {
                    Logs.LogWriteError(ex.Message);
                    return StatusCode(500);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }        
            return StatusCode(500);
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Put(Employee e)
        {
            if (e.Validate())
            {
                return StatusCode(500);
            }
            if (GetCon())
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Employee SET Department=@Department, Name=@Name, DateOfBirth=@DateOfBirth, DateOfEmployment=@DateOfEmployment, Wage=@Wage WHERE Id=@Id";
                cmd.Parameters.AddWithValue("Id", e.Id);
                cmd.Parameters.AddWithValue("Department", e.Department);
                cmd.Parameters.AddWithValue("Name", e.Name);
                cmd.Parameters.AddWithValue("DateOfBirth", e.DateOfBirth);
                cmd.Parameters.AddWithValue("DateOfEmployment", e.DateOfEmployment);
                cmd.Parameters.AddWithValue("Wage", e.Wage);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logs.LogWriteError(ex.Message);
                    return StatusCode(500);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return Ok();
        }
    }
}
