<%@ Page Title="" Language="C#" MasterPageFile="~/CP.master" AutoEventWireup="true" CodeFile="ObserveMessage.aspx.cs" Inherits="Moderator_ObserveMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Table ID="Table1" runat="server" Border="1" Height="124px" 
        
        
        
        style="z-index: 1; left: 189px; top: 347px; position: absolute; height: 124px; width: 700px">
     <asp:TableRow>
      <asp:TableCell ID="MessageTitleCell" HorizontalAlign="Right" >

        </asp:TableCell>
      </asp:TableRow>

    <asp:TableRow> 
        <asp:TableCell ID="SenderCell" HorizontalAlign="Right">
  
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
          <asp:TableCell ID="MessageCell" HorizontalAlign="Right">
                  
          </asp:TableCell>
    </asp:TableRow>


      <asp:TableRow>
        <asp:TableCell ID="ButtonCell"  HorizontalAlign="Right" >

            <asp:Button ID="Button2" runat="server" Text="הגב"  onclick="Button1_Click" />

         </asp:TableCell>
       </asp:TableRow>

     </asp:Table>
 
</asp:Content>

