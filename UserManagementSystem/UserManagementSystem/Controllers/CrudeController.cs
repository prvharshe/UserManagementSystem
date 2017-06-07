using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserManagementSystem.Controllers
{
    public class CrudeController : Controller
    {
        //
        // GET: /Crude/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public ActionResult Edit(int LoginId)
        {
            using (EmployeeEntities3 dc = new EmployeeEntities3())
            {
                UserDetail u = dc.UserDetails.Find(LoginId);
                if (u == null)
                {
                    return HttpNotFound();
                }
                return View(u);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserDetail u)
        {
            using (EmployeeEntities3 dc = new EmployeeEntities3())
            {
                if (ModelState.IsValid)
                {
                    dc.Entry(u).State = EntityState.Modified;
                    dc.SaveChanges();
                    return RedirectToAction("AfterEdit");
                }
                return View();
            }
        }

        public ActionResult AfterEdit()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }

        public ActionResult Delete(int LoginId)
        {
            using (EmployeeEntities3 dc = new EmployeeEntities3())
            {
                UserDetail u = dc.UserDetails.Find(LoginId);
                if (u == null)
                {
                    ViewBag.Message = "No Records Found";
                }

                return View(u);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult AfterDelete(int LoginId)
        {
            using (EmployeeEntities3 dc = new EmployeeEntities3())
            {
                UserDetail u = dc.UserDetails.Find(LoginId);
                u.IsDeleted = true;
                u.IsActive = false;
                //dc.UserDetails.Remove(u);
                dc.SaveChanges();
                return RedirectToAction("AfterDelete");
            }

        }

        public ActionResult AfterDelete()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Delete");
            }
        }
    }
}
