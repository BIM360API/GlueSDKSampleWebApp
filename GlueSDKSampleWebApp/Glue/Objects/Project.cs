/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlueSDKSampleWebApp.Glue.Objects
{
    public class Project
    {
        public string project_id { get; set; }
        public string project_name { get; set; }
        public string company_id { get; set; }
        public string created_date { get; set; }
        public string modify_date { get; set; }
        public string modify_user { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string thumbnail_modified_date { get; set; }
        public string has_views { get; set; }
        public string has_markups { get; set; }
        public string has_clashes { get; set; }
        public string last_activity_date { get; set; }
    }
}