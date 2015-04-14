
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="GlueSDKSampleWebApp._Default" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Select a Project
    </h2>
    <asp:ListView ID="ProjectListView" runat="server" DataSourceID="ProjectDataSource" DataKeyNames="project_id">
     <LayoutTemplate>
     <ul class="leftCol">
     <li id="itemPlaceHolder" runat="server"></li>
     </ul>     
     </LayoutTemplate>
        <ItemTemplate>
            <li>
              <a href="Actions.aspx?projectId=<%# Eval("project_id") %>"><asp:Label ID="NameLabel" runat="server" Text='<%# HttpUtility.UrlDecode(Eval("project_name") as string) %>' /></a>
            </li>
        </ItemTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="ProjectDataSource" runat="server" selectMethod="GetProjects" typename="GlueSDKSampleWebApp.Glue.ProjectDataSource"></asp:ObjectDataSource>
</asp:Content>
