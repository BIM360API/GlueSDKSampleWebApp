<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Actions.aspx.cs" Inherits="GlueSDKSampleWebApp.Actions" %>
<asp:Content runat="server" ID="Head" ContentPlaceHolderID="HeadContent">
<script language="javascript" src="Scripts/jquery-1.11.0.min.js"></script>
<script language="javascript" src="Scripts/actions.js"></script>    
<script language="javascript" src="Scripts/glue-embedded.js"></script>
</asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="MainContent">
    <script language="javascript">
        window.company = "<%= ViewState["company"] %>";
    </script>
    <asp:Panel ID="Panel2" runat="server" class="leftCol">
    <h2>
        Select an Activity
    </h2>
    <asp:ListView ID="ActionListView" runat="server" DataSourceID="ActionDataSource" DataKeyNames="action_id">
     <LayoutTemplate>
     <ul id="actionList">
     <li id="itemPlaceHolder" runat="server"></li>
     </ul>     
     </LayoutTemplate>
        <ItemTemplate>
            <li>
              <a href="#" data-actionid="<%# Eval("action_id") %>"><asp:Label ID="SubjectLabel" runat="server" Text='<%# HttpUtility.UrlDecode(Eval("subject") as string) %>' />, <asp:Label ID="ObjectLabel" runat="server" Text='<%# HttpUtility.UrlDecode(Eval("type_object_name") as string) %>' /></a>
            </li>
        </ItemTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="ActionDataSource" runat="server" selectMethod="GetActions" typename="GlueSDKSampleWebApp.Glue.ActionDataSource">
        <selectparameters>
            <asp:querystringparameter name="projectId" querystringfield="projectId" defaultvalue="" />
          </selectparameters>
    </asp:ObjectDataSource>
    </asp:Panel>
<asp:Panel ID="Panel1" runat="server" style="width:1700px;">
    <label>Show Properties Module: </label> <input type="checkbox" name="show_props" id="show_props"/><br/>
    <input id="selection" type="text" /><input id="set_selection" type="button" value="Set Selection" /><input id="get_properties" type="button" value="Get Properties" /><input id="zoom_selection" type="button" value="Zoom Selection" /><br/>
    <iframe id="iframe" height="700px" width="1200px" src=""></iframe><br />
    Viewer Messages:<br />
    <textarea id="messages" disabled="disabled" style="width:900px;height:100px;"></textarea>
</asp:Panel>
    
</asp:Content>
