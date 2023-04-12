using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CrudEmpAPiController : ApiController
    {
        WebApi_CrudEntities db = new WebApi_CrudEntities();
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            try
            {
                List<Employee> list = db.Employees.ToList();
                return Ok(list);
            }catch(Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        public IHttpActionResult EmpInsert(Employee e)
        {
            try
            {
                db.Employees.Add(e);
                db.SaveChanges();
                return Ok();
            }catch(Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public IHttpActionResult GetEmployeesById(int Emp_id)
        {
            try
            {
                var Emp = db.Employees.Where(x => x.Emp_id == Emp_id).FirstOrDefault();
                return Ok(Emp);
            }catch(Exception ex)
            {
                return null;
            }
        }
        [HttpPut]
        public IHttpActionResult EmpUpdate(Employee e)
        {
            try
            {
                var emp = db.Employees.Where(x => x.Emp_id == e.Emp_id).FirstOrDefault();
                if (emp != null)
                {
                    emp.Emp_id = e.Emp_id;
                    emp.Emp_Name = e.Emp_Name;
                    emp.Emp_gender = e.Emp_gender;
                    emp.Emp_age = e.Emp_age;
                    emp.Emp_designation = e.Emp_designation;
                    emp.Emp_Salary = e.Emp_Salary;
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }

                return Ok();
            }catch(Exception ex)
            {
                return null;
            }
        }
        [HttpDelete]
        public IHttpActionResult EmpDelete(int Emp_id)
        {
            try
            {
                var emp = db.Employees.Where(x => x.Emp_id == Emp_id).FirstOrDefault();
                //db.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Ok();
            }catch(Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public IHttpActionResult EmpSearch(string EmpName)
        {
            try
            {
                var emp = db.Employees.Where(x => x.Emp_Name == EmpName).ToList();
                return Ok(emp);
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
