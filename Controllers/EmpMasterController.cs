using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmpTask.DAL;
using EmpTask.Models;

namespace EmpTask.Controllers
{
    public class EmpMasterController : Controller
    {
        EmployeeDAL employeeDAL = new EmployeeDAL();
        // GET: EmpMaster
        public ActionResult Index()
        {
            var employeeList = employeeDAL.GetAllEmployees();
            if (employeeList.Count ==0)
            {
                TempData["InfoMessage"] = "Currently employees not available in the Database ";
            }
            return View(employeeList);
        }

        

        // GET: EmpMaster/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EmpMaster/Create
        [HttpPost]
        public ActionResult Create(EmpMaster empMaster)
        {
            try
            {
                // Check if the email is unique

                bool isEmailNotUnique = employeeDAL.IsEmailNotUnique(empMaster.Email);
                
                if (!isEmailNotUnique)
                {
                    TempData["ErrorMessage"] =  "Email already exists. Please use a different email address.";
                    return View(empMaster);
                } 

                if (empMaster.Designation == "Peon" && (empMaster.Salary < 5000 || empMaster.Salary > 10000))
                {
                    TempData["ErrorMessage"] = "Salary for Peon Designation must be between 5000 to 10000";
                    return View(empMaster);
                }

                if (empMaster.Designation == "Manager" && (empMaster.Salary < 10000 || empMaster.Salary > 20000))
                {
                    TempData["ErrorMessage"] = "Salary for Manager Designation must be between 10000 to 20000";
                    return View(empMaster);
                }

                
                if (ModelState.IsValid)
                {
                    bool IsInserted = employeeDAL.InsertEmp(empMaster);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Employee details saved successfully......";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Employee is already available / Unable to save the Employee details.";
                    }
                }

                return View(empMaster);

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(empMaster);
            }
        }

        // GET: EmpMaster/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = employeeDAL.GetEmpById(id).FirstOrDefault();
            if (employees == null)
            {
                TempData["InfoMessage"] = "Employee not available with ID " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        // POST: EmpMaster/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateEmp(EmpMaster empMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = employeeDAL.UpdateEmp(empMaster);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Employee details updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update employee details.";
                    }


                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: EmpMaster/Delete/5
        public ActionResult Delete(int id)
        {
            var employees = employeeDAL.GetEmpById(id).FirstOrDefault();
            if (employees == null)
            {
                TempData["InfoMessage"] = "Employee details not available with the product Id : " + id;
                return RedirectToAction("Index");
            }
            return View(employees);
           
        }

        // POST: EmpMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            bool IsDeleted = employeeDAL.DeleteEmp(id);
            if (IsDeleted)
            {
                TempData["SuccessMessage"] = "Employee deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete employee. Please try again.";
            }

            return RedirectToAction("Index");
        }
    }
}
