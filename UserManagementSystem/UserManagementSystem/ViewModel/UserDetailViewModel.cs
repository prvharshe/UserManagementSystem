using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserManagementSystem.ViewModel
{
    public class UserDetailViewModel
    {
        [DisplayName("Login Id")]
        public int LoginId { get; set; }
         [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Contact { get; set; }

        [DisplayName("Role Name")]
        public string RoleName { get; set; }



        //public List<UserDetail> UserDetailsList;

        //public List<RoleMaster> RoleDetails;

        //public UserDetailViewModel()
        //{
        //    UserDetailsList = new List<UserDetail>();
        //    RoleDetails = new List<RoleMaster>();
        //}


    }
}

