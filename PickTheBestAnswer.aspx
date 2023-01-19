<%@ Page Title="" Language="C#" MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="PickTheBestAnswer.aspx.cs" Inherits="PickTheBestAnswer" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ PreviousPageType VirtualPath="~/AnswerQuestion.aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
  
 
   <br />


    <asp:Table ID="Table2" runat="server" Width="568px" Height="83px">
        <asp:TableRow runat="server" HorizontalAlign="Right">
            <asp:TableCell runat="server">
                         <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            
</asp:TableCell>

        </asp:TableRow>
        <asp:TableRow runat="server" HorizontalAlign="Right">
            <asp:TableCell runat="server">
                                                      
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="54px" 
                                RepeatDirection="Horizontal" Width="164px">
                                <asp:ListItem Selected="True">1</asp:ListItem>
                                
                                        <asp:ListItem>2</asp:ListItem>
                                
                                        <asp:ListItem>3</asp:ListItem>
                                
                                        <asp:ListItem>4</asp:ListItem>
                                
                                        <asp:ListItem>5</asp:ListItem>
                
                                        </asp:RadioButtonList>
                
                     
            
</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" Width="800px" HorizontalAlign="Right">
                  <asp:TableCell runat="server">

              <asp:Label ID="Label1" runat="server" Text="&quot; &quot;"></asp:Label>
</asp:TableCell>
        </asp:TableRow>
    </asp:Table>

 <br />
    <div dir="rtl">
    
    <asp:TextBox ID="TextBox1" runat="server" Height="61px" 
          Width="502px" MaxLength="5000" TextMode="MultiLine"></asp:TextBox>
     
        <cc1:TextBoxWatermarkExtender ID="TextBox1_TextBoxWatermarkExtender" 
            runat="server" Enabled="True" TargetControlID="TextBox1" 
            ViewStateMode="Enabled" WatermarkText="סיבה לתשובה הטובה ביותר?">
        </cc1:TextBoxWatermarkExtender>
     
    </div>
     <asp:Button ID="Button1" runat="server" Text="שלח" onclick="Button1_Click" />
     <br /><br />
    

</asp:Content>


