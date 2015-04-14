/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlueSDKSampleWebApp.Glue.Objects
{
    public class Action
    {
        public string action_id { get; set; }
        public string project_id { get; set; }
        public string model_id { get; set; }
        public string version_id { get; set; }
        public string model_name { get; set; }
        public string subject { get; set; }
        public string type { get; set; }
        public string type_object_id { get; set; }
        public string type_object_name { get; set; }
        public string created_by { get; set; }
        public string created_date { get; set; }

    }
}