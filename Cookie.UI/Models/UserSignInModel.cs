﻿using Microsoft.Build.Framework;

namespace Cookie.UI.Models
{
    public class UserSignInModel
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; } = true;
    }
}
