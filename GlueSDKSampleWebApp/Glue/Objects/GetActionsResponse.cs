/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlueSDKSampleWebApp.Glue.Objects
{
    public class GetActionsResponse
    {
        public List<Action> action_list { get; set; }
        public int page { get; set; }
        public int page_size { get; set; }
        public int total_result_size { get; set; }
        public int more_pages { get; set; }
    }
}