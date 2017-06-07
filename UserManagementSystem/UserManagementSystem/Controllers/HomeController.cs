using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UserManagementSystem.Models;
using UserManagementSystem.ViewModel;
using WebMatrix.WebData;


namespace UserManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public string ModBy;
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Logout()
        {
           
            TempData["UserName"] = null;
            FormsAuthentication.SignOut();
            Session.Abandon();
          

            return RedirectToAction("login");



           
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        
        
        {
            return View();
        }

//        public ActionResult ResetPassword()
//        {
//            [AllowAnonymous]
//[HttpPost]
//public ActionResult ResetPassword(ResetPasswordModel model)
//{
//    string emailAddress = WebSecurity.GetEmail(model.UserName);
//    if (!string.IsNullOrEmpty(emailAddress))
//    {
//        string confirmationToken =
//            WebSecurity.GeneratePasswordResetToken(model.UserName);
//        dynamic email = new Email("ChngPasswordEmail");
//        email.To = emailAddress;
//        email.UserName = model.UserName;
//        email.ConfirmationToken = confirmationToken;
//        email.Send();
 
//        return RedirectToAction("ResetPwStepTwo");
//    }
 
//    return RedirectToAction("InvalidUserName");
//}
//        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDetail u)
        {
            if (ModelState.IsValid) // this is check validity
            {
                using (EmployeeEntities3 dc = new EmployeeEntities3())
                {

                    var v = dc.UserDetails.Where(a => a.UserName.Equals(u.UserName) && a.Password.Equals(u.Password)).FirstOrDefault();
                    if (v==null)
                    {
                        ViewBag.Error = "Invalid User Credentials";
                    }
                    if (v != null)
                    {
                        TempData["UserName"] = v.FirstName+' '+v.LastName;
                        TempData["LoginId"] = v.LoginId;
                        
                       if (v.IsDeleted == true)
                        {
                            ViewBag.Message = "Invalid User";
                        }
                        else
                        {
                           
                            Session["LogedUserID"] = v.LoginId.ToString();
                            return RedirectToAction("AfterLogin");

                        }
                        
                        return RedirectToAction("Register");

                    }
                }
            }
            return View(u);
        }
        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    



        public ActionResult Register()
        {
            
           return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDetail u)
        {
            if (ModelState.IsValid)
            {
                using (EmployeeEntities3 dc = new EmployeeEntities3())
                {
                    dc.UserDetails.Add(u);
                    //you should check duplicate registration here 
                    if (TempData.ContainsKey("LoginId"))
                    {
                        u.CreatedBy = Convert.ToString(TempData.Peek("LoginId"));
                    }
                    //u.CreatedOn = Convert.ToString( DateTime.Now);



                    dc.SaveChanges();
                    ModelState.Clear();
                    u = null;
                    return RedirectToAction("AfterRegister");
                }
            }
            return View(u);

        }

        public ActionResult AfterRegister()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult Retrieve(String search)
        {
            using (EmployeeEntities3 dc = new EmployeeEntities3())
            {
                //  if (SearchBy == UserName)
                //{
                List<UserDetailViewModel> userDetailsList = new List<UserDetailViewModel>();

                //userDetailsList = (from u in dc.UserDetails
                //                   where u.FirstName.Contains(search)
                //                   select u).ToList();

                var list = dc.UserDetails
               .Join(dc.RoleMasters,
        c => c.RoleId,
        o => o.RoleId,
        (c, o) => new UserDetailViewModel()
        {
            LoginId = c.LoginId,
            FirstName = c.FirstName,
            LastName = c.LastName,
            UserName = c.UserName,
            Password = c.Password,
            Contact = c.Contact,
          
            RoleName = o.RoleName
        }).ToList();

                userDetailsList = list;


                if (userDetailsList != null && userDetailsList.Count() > 0 && !string.IsNullOrEmpty(search))
                {
                    return View(userDetailsList.Where(x => x.FirstName.ToLower().Contains(search) || search == null).ToList());
                }
                else
                {

                    return View("Retrieve", userDetailsList);

                }

            }

        }




        //public ActionResult WelcomeUser()
        //{
        //    if (TempData.ContainsKey("FirstName"))
        //    {
        //        ViewBag.firstName = TempData.Peek("FirstName");
        //    }
            
        //}
        //{
        //    using (EmployeeEntities3 dc = new EmployeeEntities3())
        //    {
        //        UserDetailViewModel vm = new UserDetailViewModel();
        //List<UserDetail> userDetailsList = new List<UserDetail>();
        //var userDetailsList = (from u in dc.UserDetails
        //                       where u.FirstName.Contains(search)
        //                       select u).ToList();
        //{
        //    u.FirstName,
        //    u.LastName,
        //    u.UserName,
        //    u.IsActive,
        //    u.IsDeleted
        //};
        //var RetrieveViewModel = new RetrieveViewModel(userDetailsList);

        //if (userDetailsList != null && userDetailsList.Count() > 0)
        //{
        //    return View("Retrieve", userDetailsList);
        //}
        ////return View(dc.UserDetails.Where(x => x.UserName == search || search == null).ToList());

        ////}
        //else
        //{

        //    return View("retrieve", null);
        //}
    }
}








