/* Copyright 2014 Autodesk, Inc.  All rights reserved.
Use of this software is subject to the terms of the Autodesk license agreement provided at the time of installation or download, or which otherwise accompanies this software in 
either electronic or hard copy form.   */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using GlueSDKSampleWebApp.Glue.Objects;

namespace GlueSDKSampleWebApp.Glue
{
    public class InvalidTokenException : Exception {}

    public static class GlueAPI
    {
        //TODO: Add your developer keys and company/host name here
        private static string publicKey = ConfigurationManager.AppSettings["publicKey"];
        private static string secretKey = ConfigurationManager.AppSettings["privateKey"];
        private static string company = ConfigurationManager.AppSettings["company"];



        public static string Login(string userName, string password)
        {
            Dictionary<string,string> paramDict = new Dictionary<string,string>();
            paramDict.Add("login_name", userName);
            paramDict.Add("password", password);
            HttpWebResponse httpResponse = PostRequest("https://b4-staging.autodesk.com/api/security/v1/login.json", null, paramDict);
            string authToken = null;

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = streamReader.ReadToEnd();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Login response = ser.Deserialize<Login>(responseText);
                    authToken = response.auth_token;
                }
            }

            return authToken;
        }

        public static GetProjectsResponse GetProjects(string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new InvalidTokenException();
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            paramDict.Add("page_size", "100");

            HttpWebResponse httpResponse = GetRequest("https://b4-staging.autodesk.com/api/project/v1/list.json", authToken, paramDict);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = streamReader.ReadToEnd();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    GetProjectsResponse response = ser.Deserialize<GetProjectsResponse>(responseText);
                    return response;
                }
            }
            return null;
        }


        public static GetActionsResponse GetActions(string authToken, string projectId)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new InvalidTokenException();
            Dictionary<string,string> paramDict = new Dictionary<string, string>();
            paramDict.Add("page_size", "100");
            paramDict.Add("type", "model;view;mergedmodel;markup");
            paramDict.Add("project_id", projectId);

            HttpWebResponse httpResponse = GetRequest("https://b4-staging.autodesk.com/api/action/v1/search.json", authToken, paramDict);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string responseText = streamReader.ReadToEnd();
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    GetActionsResponse response = ser.Deserialize<GetActionsResponse>(responseText);
                    return response;
                }
            }
            return null;
        }

        private static HttpWebResponse GetRequest(string url, string authToken, Dictionary<string, string> paramDict)
        {
            AddSigningParameters(authToken, paramDict);

            HttpWebResponse httpResponse = null;
            string qs = FormatQueryString(paramDict);
            url = string.Format("{0}?{1}", url, qs);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            try
            {
                httpResponse = (HttpWebResponse) request.GetResponse();
            }
            catch (WebException ex)
            {
                return (HttpWebResponse)ex.Response;
            }
            return httpResponse;
        }

        private static HttpWebResponse PostRequest(string url, string authToken, Dictionary<string, string> paramDict)
        {
            AddSigningParameters(authToken, paramDict);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse httpResponse = null;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string qs = FormatQueryString(paramDict);
                streamWriter.Write(qs);
            }
            try
            {
                httpResponse = (HttpWebResponse) request.GetResponse();
            }
            catch (WebException ex)
            {
                return (HttpWebResponse)ex.Response;
            }
            return httpResponse;

        }

        public static string GetSigningParams(string authToken)
        {
            if (string.IsNullOrEmpty(authToken))
                throw new InvalidTokenException();
            Dictionary<string,string> paramDict = new Dictionary<string, string>();
            AddSigningParameters(authToken, paramDict);
            return FormatQueryString(paramDict);
        }

        private static void AddSigningParameters(string authToken, Dictionary<string, string> paramDict)
        {
            if (paramDict == null)
                return;
            string timestamp = GetTimestamp();
            paramDict.Add("company_id", company);
            paramDict.Add("api_key", publicKey);
            paramDict.Add("timestamp", timestamp);
            string sig = GetMd5Hash(publicKey + secretKey + timestamp);
            paramDict.Add("sig", sig);
            if (!string.IsNullOrEmpty(authToken))
                paramDict.Add("auth_token", authToken);
        }

        private static string GetMd5Hash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

        private static string GetTimestamp()
        {
            double unixTime = Math.Floor((DateTime.UtcNow - UnixEpoch).TotalSeconds);
            return unixTime.ToString();
        }

        private static string FormatQueryString(Dictionary<string,string> paramDict)
        {
            string qs = "";
            foreach (KeyValuePair<string, string> keyValuePair in paramDict)
            {
                qs += string.Format("&{0}={1}", keyValuePair.Key, HttpUtility.UrlEncode(keyValuePair.Value));
            }
            qs = qs.Remove(0,1);
            return qs;
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
    }
}