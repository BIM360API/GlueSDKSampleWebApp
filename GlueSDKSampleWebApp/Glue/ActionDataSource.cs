/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GlueSDKSampleWebApp.Glue.Objects;
using Action = GlueSDKSampleWebApp.Glue.Objects.Action;

namespace GlueSDKSampleWebApp.Glue
{
    public class ActionDataSource
    {
        public List<Action> getActions(string projectId)
        {
            string authToken = HttpContext.Current.Session["authToken"] as string;
            if (!string.IsNullOrEmpty(projectId))
            {
                GetActionsResponse response = GlueAPI.GetActions(authToken, projectId);
                if (response != null)
                    return response.action_list;
            }
            return new List<Action>();
        }
    }
}