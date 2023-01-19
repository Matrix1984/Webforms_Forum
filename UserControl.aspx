<%@ Page Title="" Language="C#" MasterPageFile="~/CP.master" AutoEventWireup="true" CodeFile="UserControl.aspx.cs" Inherits="UserControl" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="ContentCP" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
      
        TABLE.tabulardata TR {
	      height:30px;
        }
     
        TABLE.tabulardata TD {
	        height:30px;
        }
    </style>

</asp:Content>
<asp:Content ID="ContentCP2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="position:absolute; right:310px; top: 0px;">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        style="z-index: 1; left: 319px; top: 330px; position: absolute;  width: 666px" 
       BackColor="#CCCCCC" BorderColor="#999999" 
        BorderWidth="0px" CellPadding="0" CellSpacing="3"  
        ForeColor="Black" AllowPaging="True" DataSourceID="ObjectDataSource1" 
        PageSize="5" onpageindexchanging="GridView1_PageIndexChanging" 
    onrowcreated="GridView1_RowCreated" Height="30px" CssClass="tabulardata" 
        DataKeyNames="CommentsID" onrowdeleted="GridView1_RowDeleted">

 <Columns>
      <asp:CommandField ShowDeleteButton="True" />
      <asp:TemplateField>
                        <FooterStyle Height="10px" />
                        <HeaderStyle Width="500px" Height="10px" />
                         <ItemStyle Width="500px" Height="10px" HorizontalAlign="Right" 
                            VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblMessage" runat="server"  Text='<%# Bind("Comment") %>'></asp:Label>
                        </ItemTemplate>
       </asp:TemplateField>

           <asp:TemplateField>
                        <FooterStyle Height="10px" />
                        <HeaderStyle Width="100px" Height="10px" />
                          <ItemStyle Width="100px" Height="30px" HorizontalAlign="Center" 
                            VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Image ID="imgName" runat="server"  imageUrl='<%# Bind("Img") %>'></asp:Image><br />
                            <asp:Hyperlink ID="hyperLink" runat="server"  Text='<%# Bind("Name") %>' ></asp:Hyperlink>
                        </ItemTemplate>
       </asp:TemplateField>
   </Columns>





        <FooterStyle BackColor="#CCCCCC" HorizontalAlign="Right" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Height="10px" 
            CssClass=".tabulardata" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Right" Height="10px"  />
        <RowStyle BackColor="White" Height="30px" CssClass=".tabulardata" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>







   <div align="center">






    <div align="left" style="height: 1425px; width: 1305px; margin-top: 0px;">
    
             
    <br />
    <br />
    <br />
    <br />
    <br />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
               ControlToValidate="TextBox1" ErrorMessage="אין להשאיר שדות רקים" 
               style="z-index: 1; left: 516px; top: 1187px; position: absolute; width: 158px;" 
               ValidationGroup="Txt"></asp:RequiredFieldValidator>
           <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
               SelectMethod="GetObjects" TypeName="ControlPanelMessages" 
            DeleteMethod="RemoveComment">
               <DeleteParameters>
                   <asp:Parameter Name="CommentsID" Type="Int32" />
               </DeleteParameters>
           </asp:ObjectDataSource>

     <div style="position:absolute; top:0; right:1265px">
           <asp:TextBox ID="TextBox1" runat="server" 
               style="z-index: 1; left: 362px; top: 1001px; position: absolute; height: 99px; width: 465px" 
               TextMode="MultiLine" ValidationGroup="Txt"></asp:TextBox>
               
             
               <asp:Button ID="Button1" runat="server" Text="שלח הודעה" 
               onclick="Button1_Click" 
               style="z-index: 1; left: 543px; top: 1132px; position: absolute; width: 90px; height: 39px;" 
               ValidationGroup="Txt" />
             </div>  
             
           </div>
    </div>
</div>

        </asp:Content>

