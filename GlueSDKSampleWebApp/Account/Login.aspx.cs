/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlueSDKSampleWebApp.Glue;

namespace GlueSDKSampleWebApp.Account
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string authToken = GlueAPI.Login(LoginUser.UserName, LoginUser.Password);
            bool authenticated = !string.IsNullOrEmpty(authToken);
            if (authenticated)
            {
                e.Authenticated = true;
                Session["authToken"] = authToken;
            }
        }
    }

}
