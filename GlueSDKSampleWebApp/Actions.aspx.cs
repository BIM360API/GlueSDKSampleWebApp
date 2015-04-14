/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using GlueSDKSampleWebApp.Glue;

namespace GlueSDKSampleWebApp
{
    public partial class Actions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["company"] = ConfigurationManager.AppSettings["company"];
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetSigningParams()
        {
            string authToken = HttpContext.Current.Session["authToken"] as string;
            return GlueAPI.GetSigningParams(authToken);
        }
    }
}