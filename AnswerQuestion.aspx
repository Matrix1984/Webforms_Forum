<%@ Page Title="" Language="C#" MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="AnswerQuestion.aspx.cs" Inherits="AnswerQuestion" Theme="AnswerDesign"%>
<%@ PreviousPageType VirtualPath="~/AskQuestion.aspx" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
  
    .newStyle4
    {
        position: absolute;
    }
    .newStyle5
    {
        position: relative;
        z-index: auto;
    }
  
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <br />
<br />

<br />
<div>
    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
    <br />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    <br />
    <br />
    <br />
    </div>

 
   


</asp:Content>

