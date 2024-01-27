using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmpTask.DAL;
using EmpTask.Models;

namespace EmpTask.Controllers
{
    public class ProMasterController : Controller
    {
        MasterDAL proDAL = new MasterDAL();

        // GET: ProMaster
        public ActionResult Index()
        {
            var projectList = proDAL.GetAllProjects();
            if (projectList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently employees not available in the Database.";
            }
            return View(projectList);
        }

        

        // GET: ProMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProMaster/Create
        [HttpPost]
        public ActionResult Create(ProjectMaster projectMaster)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = proDAL.InsertProject(projectMaster);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Project details saved successfully......";

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Project is already available / Unable to save the Employee details.";
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
    

        // GET: ProMaster/Edit/5
        public ActionResult Edit(int id)
        {
            var projects = proDAL.GetAllProjects(id).FirstOrDefault();

            if (projects == null)
            {
                TempData["InfoMessage"] = "Currently employees not available with ID" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(projects);
        }

        // POST: ProMaster/Edit/5
        [HttpPost,ActionName("Edit")]
        public ActionResult UpdateProject(ProjectMaster projectMaster)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = proDAL.UpdateProject(projectMaster);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Project details updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update project details.";
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

        // GET: ProMaster/Delete/5
        public ActionResult Delete(int id)
        {
            var employees = proDAL.GetAllProjects(id).FirstOrDefault();
            if (employees == null)
            {
                TempData["InfoMessage"] = "Project details not available with the product Id : " + id;
                return RedirectToAction("Index");
            }
            return View(employees);
        }




        // POST: ProMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            bool IsDeleted = proDAL.DeletePro(id);

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
