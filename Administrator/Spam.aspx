<%@ Page Title="" Language="C#" MasterPageFile="~/CP.master" AutoEventWireup="true" CodeFile="Spam.aspx.cs" Inherits="Moderator_Spam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:Label ID="CommentHeader" runat="server" Text="הודעות ספאם של פוסטים"></asp:Label>
 <br /> <br /> 
    <asp:ListView ID="ListView1" runat="server" DataKeyNames="CommentsID" 
        DataSourceID="SqlDataSource1">



        <AlternatingItemTemplate>
            <tr style="background-color:#FFF8DC;">
                <td>
                    <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                </td>
                 <td>
                    <asp:Button ID="DeleteComment_Btn" runat="server" Text="מחק" CommandName="Delete"  />
                    <asp:Button ID="IgnoreComment_Btn" runat="server" Text="התעלם" CommandName="Update"  />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color:#008A8C;color: #FFFFFF;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="CommentsTextBox" runat="server" 
                        Text='<%# Bind("Comments") %>' />
                </td>
            </tr>
        </EditItemTemplate>

        <EmptyDataTemplate>
            <table runat="server" 
                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>
                        אין ספאם</td>
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
                    <asp:TextBox ID="CommentsTextBox" runat="server" 
                        Text='<%# Bind("Comments") %>' />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </InsertItemTemplate>


        <ItemTemplate>
            <tr style="background-color:#DCDCDC;color: #000000;">
                <td>
                    <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                </td>
                <td>
                    <asp:Button ID="DeleteComment_Btn" runat="server" Text="מחק" CommandName="Delete"  />
                    <asp:Button ID="IgnoreComment_Btn" runat="server" Text="התעלם" CommandName="Update"  />
                </td>
            </tr>
        </ItemTemplate>


        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="1" 
                            style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                <th runat="server">
                                    Comments</th>
                          
                            </tr>
                                <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" 
                        style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                    ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                <td>
                    <asp:Label ID="CommentsLabel" runat="server" Text='<%# Eval("Comments") %>' />
                  
                </td>
              
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CP_AllQuestionsAnswered %>" 
        DeleteCommand="ModeratorSpamDeleteComment" DeleteCommandType="StoredProcedure" 
        SelectCommand="ModeratorSpamComments" SelectCommandType="StoredProcedure" 
        UpdateCommand="ModeratorIgnoreSpamComment" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="CommentsID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="CommentsID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <br />  <br /> <br /> <br /> <br /> <asp:Label ID="Label1" runat="server" Text="הודעות ספאם של אשכולים"></asp:Label>
 <br /> <br /> 

    
    <asp:ListView ID="ListView2" runat="server" DataKeyNames="ThreadsID" 
        DataSourceID="SqlDataSource2" 
        style="z-index: 1; left: 452px; top: 753px; position: absolute; height: 381px; width: 221px">
        <AlternatingItemTemplate>
            <tr style="background-color:#FFF8DC;">
                <td>
                    <asp:Label ID="ThreadTitleLabel" runat="server" 
                        Text='<%# Eval("ThreadTitle") %>' />
                </td>
                   <td>
                    <asp:Button ID="DeleteThread_Btn" runat="server" Text="מחק" CommandName="Delete" />
                    <asp:Button ID="IgnoreThread_Btn" runat="server" Text="התעלם" CommandName="Update" />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color:#008A8C;color: #FFFFFF;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                        Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                        Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="ThreadTitleTextBox" runat="server" 
                        Text='<%# Eval("ThreadTitle") %>' />
                </td>
           
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" 
                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>
                        אין ספאם</td>
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
                    <asp:TextBox ID="ThreadTitleTextBox" runat="server" 
                        Text='<%# Bind("ThreadTitle") %>' />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color:#DCDCDC;color: #000000;">
                <td>
                    <asp:Label ID="ThreadTitleLabel" runat="server" 
                        Text='<%# Eval("ThreadTitle") %>' />
                </td>
                   <td>
                    <asp:Button ID="DeleteThread_Btn" runat="server" Text="מחק" CommandName="Delete" />
                    <asp:Button ID="IgnoreThread_Btn" runat="server" Text="התעלם" CommandName="Update" />
                </td>
               
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table ID="itemPlaceholderContainer" runat="server" border="1" 
                            style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color:#DCDCDC;color: #000000;">
                                <th runat="server">
                                    ThreadTitle</th>
                             </tr>
                            <tr ID="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" 
                        style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                    ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color:#008A8C;font-weight: bold;color: #FFFFFF;">
                <td>
                    <asp:Label ID="ThreadTitleLabel" runat="server" 
                        Text='<%# Eval("ThreadTitle") %>' />
                </td>
               
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CP_AllQuestionsAnswered %>" 
        DeleteCommand="ModeratorSpamDeleteThread" DeleteCommandType="StoredProcedure" 
        SelectCommand="ModeratorSpamThread" SelectCommandType="StoredProcedure" 
        UpdateCommand="ModeratorSpamIgnoreThread" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="ThreadsID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ThreadsID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

