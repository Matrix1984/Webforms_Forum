<%@ Page Title="" Language="C#" MasterPageFile="~/CP.master" AutoEventWireup="true" CodeFile="NewMessage.aspx.cs" Inherits="Moderator_NewMessage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="direction:rtl; height: 642px; left:300px; margin-bottom: 175px;">
    <asp:Table ID="Table1" runat="server"  
        
        
        
        style="z-index: 1; left: 145px; top: 371px; position: absolute; height: 55px; width: 850px">


        <asp:TableRow>
           <asp:TableCell>
               </asp:Label>  <asp:Label ID="Label2" runat="server" Text="כותרת"></asp:Label> 
                  </asp:TableCell>
                  <asp:TableCell>
               <asp:TextBox ID="TextBox1" runat="server" Height="15px" Width="708px"></asp:TextBox>
          </asp:TableCell>
        </asp:TableRow>



       <asp:TableRow>
        <asp:TableCell>
              <asp:Label ID="Label1" runat="server" Text="שלח אל"></asp:Label> 

                     </asp:TableCell>
                  <asp:TableCell>
               <asp:DropDownList ID="DropDownList1" runat="server" 
                style="z-index: 1; left: 826px; top: 361px;  height: 20px; width: 97px; direction:rtl">
                </asp:DropDownList> 
            </asp:TableCell>
    </asp:TableRow>


</asp:Table>




                    <cc1:Editor 
                        ID="Editor1" 
                        Width="850px"  
                        Height="400px"
                        runat="server" 
                         style="z-index: 1; left: 137px; top: 485px; position: absolute; height: 400px; width: 850px;"/>

       



   
 <asp:Table ID="Table2" runat="server" 
        style="z-index: 1; left: 465px; top: 907px; position: absolute; height: 55px; width: 136px; margin-right: 6px" >
   
        <asp:TableRow>
           <asp:TableCell>

                    <asp:Button
                        id="btnSubmit"
                        Text="שלח"
                        Runat="server" onclick="btnSubmit_Click" />
               
            </asp:TableCell>
        </asp:TableRow> 
        



       <asp:TableRow>
           <asp:TableCell>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="Editor1" ErrorMessage="אין להשאיר שדות רקים" 
                    ForeColor="#CC0000" InitialValue=""></asp:RequiredFieldValidator>

           </asp:TableCell>
        </asp:TableRow>

    

       <asp:TableRow>
           <asp:TableCell>

                      <asp:Label ID="MessageSent" runat="server" Text="הודעה נשלחה בהצלחה"></asp:Label>

           </asp:TableCell>
        </asp:TableRow>

    </asp:Table>
    </div>
</asp:Content>

