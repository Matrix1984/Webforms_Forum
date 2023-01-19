<%@ Page Title="" Language="C#" MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="ComposeMessage.aspx.cs" Inherits="Moderator_ComposeMessage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div style="direction:rtl; position:absolute; height: 642px; left:100px; margin-bottom: 175px;">
        <cc1:Editor 
            ID="Editor1" 
            Width="850px"  
            Height="400px"
            runat="server"/>
        <br />
        <asp:Button
            id="btnSubmit"
            Text="הגב להודעה"
            Runat="server" />
    
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="Editor1" ErrorMessage="אין להשאיר שדות רקים" 
        ForeColor="#CC0000"></asp:RequiredFieldValidator>
</div>
</asp:Content>

