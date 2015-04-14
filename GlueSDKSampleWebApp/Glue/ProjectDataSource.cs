/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GlueSDKSampleWebApp.Glue.Objects;

namespace GlueSDKSampleWebApp.Glue
{
    public class ProjectDataSource
    {
        public List<Project> getProjects()
        {
            string authToken = HttpContext.Current.Session["authToken"] as string;
            GetProjectsResponse response = GlueAPI.GetProjects(authToken);
            if (response != null)
                return response.project_list;

            return null;
        }
    }
}