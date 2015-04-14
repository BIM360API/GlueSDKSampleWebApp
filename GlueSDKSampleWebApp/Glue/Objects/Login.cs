/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlueSDKSampleWebApp.Glue
{
    public class Login
    {
        public string auth_token
        {
            get; set;
        }

        public string user_id { get; set; }
    }
}