using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmpTask.DAL;
using EmpTask.Models;

namespace EmpTask.Controllers
{
    public class EmpWiseProjectController : Controller
    {
        EmpWiseProMasterDAL empWiseProMasterDAL = new EmpWiseProMasterDAL();
        MasterDAL proDAL = new MasterDAL();
        EmployeeDAL employeeDAL = new EmployeeDAL();

        // GET: EmpWiseProject
        public ActionResult Index()
        {
            var EmpWisProList = empWiseProMasterDAL.GetEmpProjects();

            if (EmpWisProList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently employees not available in the Database ";
            }
            return View(EmpWisProList);
        }

        

        // GET: EmpWiseProject/Create
        public ActionResult Create()
        {
            var projectList = proDAL.GetAllProjects();
            var employeeList = employeeDAL.GetAllEmployees();
            ViewBag.AssignDate = DateTime.Now;
            ViewBag.ProjectList = new SelectList(projectList, "RowId", "ProjectName", "Rate");
            ViewBag.projectDataList = projectList;
            ViewBag.employeeList = new SelectList(employeeList, "EmpId", "EmployeeName");
            return View();
        }

        // POST: EmpWiseProject/Create
        [HttpPost]
        public ActionResult Create(employeeWiseProMaster empWiseMaster)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = empWiseProMasterDAL.InsertPro(empWiseMaster);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Employee details saved successfully......";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Employee is already available / Unable to save the Employee details.";
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

        // GET: EmpWiseProject/Edit/5
        public ActionResult Edit(int id)
        {
            var employees = empWiseProMasterDAL.GetAllEmpProjects(id).FirstOrDefault();
            if (employees == null)
            {
                TempData["InfoMessage"] = "Employee not available with ID " + id.ToString();
                return RedirectToAction("Index");
            }
            var projectList = proDAL.GetAllProjects();
            var employeeList = employeeDAL.GetAllEmployees();
            ViewBag.AssignDate = DateTime.Now;
            ViewBag.ProjectList = new SelectList(projectList, "RowId", "ProjectName", "Rate");
            ViewBag.projectDataList = projectList;
            ViewBag.EmployeeList = new SelectList(employeeList, "EmpId", "EmployeeName");
            return View(employees);
            
        }

        // POST: EmpWiseProject/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateProject(employeeWiseProMaster empWiseMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = empWiseProMasterDAL.UpdateAllProject(empWiseMaster);

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

        // GET: EmpWiseProject/Delete/5
        public ActionResult Delete(int id)
        {

            var employees = empWiseProMasterDAL.GetAllEmpProjects(id).FirstOrDefault();
            if (employees == null)
            {
                TempData["InfoMessage"] = "Project details not available with the product Id : " + id;
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        // POST: EmpWiseProject/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteEmp(int id)
        {
            bool IsDeleted = empWiseProMasterDAL.DeleteEMPPro(id);

            if (IsDeleted)
            {
                TempData["SuccessMessage"] = "Project deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "unable to delete project. Please try again.";


            }
            return RedirectToAction("Index");

        }
    }
}
