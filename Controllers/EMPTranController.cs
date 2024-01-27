using EmpTask.DAL;
using EmpTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpTask.Controllers
{
    public class EMPTranController : Controller
    {

        EmpTranDAL empTranDAL = new EmpTranDAL();
        MasterDAL proDAL = new MasterDAL();
        EmpWiseProMasterDAL empWiseProMasterDAL = new EmpWiseProMasterDAL();

        public ActionResult Index()
        {
            var EmpTranProList = empTranDAL.GetAlEmpTran();
            if (EmpTranProList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently employees not available in the Database ";
            }
            return View(EmpTranProList);
        }



        // GET: EmpWiseProject/Create
        public ActionResult Create()
        {
            var projectList = proDAL.GetAllProjects();
            var empWisetList = empWiseProMasterDAL.GetEmpProjects();
            ViewBag.ProjectList = new SelectList(projectList, "RowId","RowId");
            ViewBag.EmpWisetList = new SelectList(empWisetList, "RowId", "RowId");
            return View();
        }

        // POST: EmpWiseProject/Create
        [HttpPost]
        public ActionResult Create(EMPTran eMPTran)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = empTranDAL.InsertTran( eMPTran);

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
            var employees = empTranDAL.GetAllTran(id).FirstOrDefault();
            if (employees == null)
            {
                TempData["InfoMessage"] = "Employee not available with ID " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(employees);

        }

        // POST: EmpWiseProject/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Update(EMPTran eMPTran)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = empTranDAL.UpdateTran( eMPTran);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Tran details updated successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update tran details.";
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

            var employees = empTranDAL.GetAllTran(id).FirstOrDefault();
            if (employees == null)
            {
                TempData["InfoMessage"] = "Tran details not available with the product Id : " + id;
                return RedirectToAction("Index");
            }
            return View(employees);
        }

        // POST: EmpWiseProject/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteEmpTran(int id)
        {
            bool IsDeleted = empTranDAL.DeleteTran(id);

            if (IsDeleted)
            {
                TempData["SuccessMessage"] = "Tran deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "unable to delete project. Please try again.";


            }
            return RedirectToAction("Index");

        }
    }
}