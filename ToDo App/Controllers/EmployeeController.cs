using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDo_App.Models;

namespace ToDo_App.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewAll"));
        }
        //  ../Employee/Edit - insert
        //  ../Employee/AddorEdit/id
        [HttpGet]
        public ActionResult AddorEdit(int id=0)
        {
            if (id == 0)
                return View();
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmployeeID", id);
                return View(DapperORM.ReturnList<EmployeeModel>("EmployeeViewByID",param).FirstOrDefault<EmployeeModel>());
            }
        }
        [HttpPost]
        public ActionResult AddorEdit(EmployeeModel emp)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeID", emp.EmployeeID);
            param.Add("@Name", emp.Name);
            param.Add("@Position", emp.Position);
            param.Add("@Age", emp.Age);
            param.Add("@Salary", emp.Salary);

            DapperORM.ExecuteWithoutReturn("EmployeeAddorEdit", param);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("EmployeeID", id);
            DapperORM.ExecuteWithoutReturn("EmployeeDeletebyID", param);
            return RedirectToAction("Index");
        }
    }
}
