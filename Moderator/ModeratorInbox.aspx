<%@ Page Title="" Language="C#"  MasterPageFile="~/CP.master" AutoEventWireup="true" CodeFile="ModeratorInbox.aspx.cs" Inherits="Moderator_ModeratorInbox" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/css">
  
 div.ListView1
{
    position: absolute;
    left: 100;
    top: 100;
}


</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  

        <asp:ListView ID="ListView1" runat="server" DataKeyNames="MessageID" 
            DataSourceID="SqlDataSource1" onitemcommand="ListView1_ItemCommand" 
            
            style="z-index: 1; left: 482px; top: 358px; position: absolute; height: 271px; width: 221px; margin-top: 0px">
        

            <LayoutTemplate>
           <div style="position:absolute; left: 452px; top: 329px;"> 

                    <table runat="server">
                      <tr runat="server">
                          <td runat="server">
                              <table ID="itemPlaceholderContainer" runat="server" border="1" 
                                  style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                  <tr runat="server" style="background-color: #FFFBD6;color: #333333;">
                                      <th runat="server">
                                          MessageTitle</th>
                                     
                                  </tr>
                                  <tr ID="itemPlaceholder" runat="server">
                                  </tr>
                              </table>
                          </td>
                      </tr>
                        <tr runat="server">
                            <td runat="server" 
                                style="text-align: center;background-color: #FFCC66;font-family: Verdana, Arial, Helvetica, sans-serif;color: #333333;">
                                <asp:DataPager ID="DataPager1" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                            ShowLastPageButton="True" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
               </div>
            </LayoutTemplate>
           
            <AlternatingItemTemplate>
                <tr style="background-color: #FAFAD2;color: #284775;">
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="Select">'<%# Eval("MessageTitle") %>'</asp:LinkButton>


                    </td>
                    
                </tr>
            </AlternatingItemTemplate>
            <EditItemTemplate>
                <tr style="background-color: #FFCC66;color: #000080;">
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                            Text="Update" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                            Text="Cancel" />
                    </td>
                    <td>
                        <asp:TextBox ID="MessageTitleTextBox" runat="server" 
                            Text='<%# Bind("MessageTitle") %>' />
                    </td>
                  
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" 
                    style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                    <tr>
                    
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                            Text="Insert" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                            Text="Clear" />
                    </td>
                    <td>
                        <asp:TextBox ID="MessageTitleTextBox" runat="server" 
                            Text='<%# Bind("MessageTitle") %>' />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </InsertItemTemplate>

            <ItemTemplate>
                <tr style="background-color: #FFFBD6;color: #333333;">
                    <td>
                   <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="Select" >'<%# Eval("MessageTitle") %>'</asp:LinkButton>
                    </td>    
                </tr>
            </ItemTemplate>
            <SelectedItemTemplate>
                <tr style="background-color: #FFCC66;font-weight: bold;color: #000080;">
                    <td>
                      <asp:LinkButton ID="LinkButton1" runat="server"  CommandName="Select">'<%# Eval("MessageTitle") %>'</asp:LinkButton>
                    </td>
                 
                </tr>
            </SelectedItemTemplate>
        </asp:ListView>
       
      


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CP_AllQuestionsAnswered %>" 
            SelectCommand="GetMessagesTitles" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:Parameter Name="Name" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>


  
</asp:Content>

