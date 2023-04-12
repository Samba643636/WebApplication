using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CrudEmpMvcController : Controller
    {
        // GET: CrudEmpMvc
        HttpClient client = new HttpClient();
        public ActionResult Index()
        {
            List<Employee> emp_list = new List<Employee>();
            client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
            var response = client.GetAsync("CrudEmpAPi");
            response.Wait();

            var test = response.Result;
            if(test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Employee>>();
                display.Wait();
                emp_list = display.Result;
            }

            return View(emp_list);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee e)
        {
            if (ModelState.IsValid)
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
                var response = client.PostAsJsonAsync<Employee>("CrudEmpAPi", e);
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    TempData["alertMessage"] = "Employee Details Added sucessfully";
                    return RedirectToAction("Index");
                }
                return View("Create");
            }
            else
            {
                return View("Create");
            }
        }
        [HttpGet]
        public ActionResult Details(int Emp_id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
            var response = client.GetAsync("CrudEmpAPi?Emp_id=" + Emp_id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }
        [HttpGet]
        public ActionResult Edit(int Emp_id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
            var response = client.GetAsync("CrudEmpAPi?Emp_id=" + Emp_id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }
        [HttpPost]
        public ActionResult Edit(Employee e)
        {
            if (ModelState.IsValid)
            {
                client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
                var response = client.PutAsJsonAsync<Employee>("CrudEmpAPi", e);
                response.Wait();

                var test = response.Result;
                if (test.IsSuccessStatusCode)
                {
                    TempData["alertMessage"] = "Employee Details Updated sucessfully";
                    return RedirectToAction("Index");
                }
                return View("Edit");
            }
            else
            {
                return View("Edit");
            }
        }
       
        public ActionResult Delete(int Emp_id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
            var response = client.GetAsync("CrudEmpAPi?Emp_id=" + Emp_id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Emp_id)
        {
            client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
            var response = client.DeleteAsync("CrudEmpAPi?Emp_id=" + Emp_id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                TempData["alertMessage"] = "Employee Details Deleted sucessfully";
                return RedirectToAction("Index");
            }
            return View("Delete");
        }
        
        public ActionResult EmpSearch(string EmpName)
        {
            List<Employee> e = new List<Employee>();
            client.BaseAddress = new Uri("https://localhost:44380/api/CrudEmpAPi");
            var response = client.GetAsync("CrudEmpAPi?EmpName=" + EmpName);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Employee>>();
                display.Wait();
                e = display.Result;
                if (e.Count != 0)
                {
                    return View(e);
                }
                else
                {
                    ViewBag.message = "No Records Found";
                    return View();
                }
            }
            return View();
        }
    }
}