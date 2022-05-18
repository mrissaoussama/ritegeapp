using System;
using System.Collections.Generic;
using System.Text;

namespace RitegeDomain.Model
{
    public class LoginRequest
    {
        //public LoginRequest(string login, string password)
        //{
        //    Login = login;
        //    Password = password;
        //}

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
