<%@ Page Title="" Language="C#" MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Statistics" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:MultiView ID="MultiView1" runat="server">

        <asp:View ID="View1" runat="server">
            <asp:Label ID="Label1" runat="server" Text="שאלות שנשאלו"></asp:Label>
             <br /><br /><br />
            <asp:DataList ID="DataList1" runat="server" 
                DataSourceID="AllQuestionAskedDataSource" BackColor="White" 
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Horizontal" DataKeyField="ThreadsID" 
                onitemcommand="DataList1_ItemCommand">
                <AlternatingItemStyle BackColor="#F7F7F7" />
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CommandName="Select" autopostback="True" Text='<%# Eval("ThreadTitle") %>'  runat="server"></asp:LinkButton>
                    <br />
                    <br />
                </ItemTemplate>

                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            </asp:DataList>
            <asp:SqlDataSource ID="AllQuestionAskedDataSource" runat="server" 
                ConnectionString="<%$ ConnectionStrings:CP_AllQuestionsAnswered %>" SelectCommand="SELECT TOP 50 ThreadTitle, ThreadsID
FROM Threads
WHERE UsersID=@UserID ORDER BY ThreadsID DESC " onselecting="AllQuestionAskedDataSource_Selecting">
                <SelectParameters>
                    <asp:Parameter  Name="UserID" />
                </SelectParameters>
            </asp:SqlDataSource>
             <br /><br /><br /><br />
            <asp:Button ID="Next1" runat="server" Text="הבא" onclick="Next1_Click" />
        </asp:View>




        <asp:View ID="View2" runat="server">
            <asp:Label ID="Label2" runat="server" Text="תשובות שניתנו"></asp:Label>
             <br /><br /><br />
            <asp:DataList ID="DataList2" runat="server" 
                DataSourceID="AllQuestionsResponded" BackColor="White" 
                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Horizontal" DataKeyField="ThreadsID" 
                onitemcommand="DataList2_ItemCommand">
                <AlternatingItemStyle BackColor="#F7F7F7" />
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <ItemTemplate>
                 
                     <asp:LinkButton ID="LinkButton2"  CommandName="Select" autopostback="True" Text='<%# Eval("ThreadTitle") %>' runat="server"></asp:LinkButton>
                    <br />
                    <br />
                </ItemTemplate>
                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            </asp:DataList>
            <asp:SqlDataSource ID="AllQuestionsResponded" runat="server" 
                ConnectionString="<%$ ConnectionStrings:CP_AllQuestionsAnswered %>" SelectCommand="SELECT TOP 50  ThreadTitle, ThreadsID
FROM Threads 
WHERE ThreadsID IN (SELECT ThreadsID
FROM Comments
WHERE UsersID=@UserID) ORDER BY ThreadsID DESC
" onselecting="AllQuestionAskedDataSource_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="UserID" />
                </SelectParameters>
            </asp:SqlDataSource>
             <br /><br /><br /><br />
        <asp:Button ID="Previous2" runat="server" Text="קודם" onclick="Previous2_Click" />
            <asp:Button ID="Next2" runat="server" Text="הבא" onclick="Next1_Click" />
        </asp:View>
     
        <asp:View ID="View3" runat="server">
            <asp:Label ID="Label4" runat="server" Text="שאלות שענית נכון"></asp:Label>
            <br />  <br />
           <div dir="rtl">
            <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="420px" 
                AllowPaging="True" AutoGenerateRows="False" BackColor="White" 
                BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                DataKeyNames="ThreadsID" DataSourceID="SqlDataSource1" 
                GridLines="Horizontal" onitemcommand="DetailsView1_ItemCommand">
                <EditRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                <FieldHeaderStyle HorizontalAlign="Left" />
                <Fields>
              <asp:TemplateField >
            
                 <ItemTemplate >
                    <asp:LinkButton ID="LinkButton3" runat="server" Text=<%# Eval("ThreadTitle") %> CommandName="Select" CommandArgument=<%# Eval("ThreadsID") %>>LinkButton</asp:LinkButton>
                 </ItemTemplate>
                
                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField>


                    <asp:BoundField HeaderText="שם אשכול"  DataField="ThreadTitle" 
                        SortExpression="ThreadTitle" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Comments" HeaderText="תשובתך" 
                        SortExpression="Comments" >
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WinningRemark" HeaderText="תגובה לתשובה המנצחת" 
                        SortExpression="WinningRemark" >
            
                    <HeaderStyle HorizontalAlign="Right" />
            
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
          
                </Fields>
                <FooterStyle BackColor="White" ForeColor="#333333" />
                <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Left" />
            </asp:DetailsView>


            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:CP_AllQuestionsAnswered %>" 
                SelectCommand="Statistics_GetAllWinningComments" 
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter Name="Name" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
          <br /><br /><br /><br />
          <asp:Button ID="Button3" runat="server" Text="הבא" onclick="Next1_Click" />
        <asp:Button ID="Button1" runat="server" Text="קודם" onclick="Previous2_Click" />
           </div>
        </asp:View>
        

        <asp:View ID="View4" runat="server">
            <asp:Label ID="Label3" runat="server" Text="אחוז תשובות לתשובות נכונות"></asp:Label><br />
             <br /><br />
      <asp:Label ID="Label122" Width="130" runat="server" Text=""></asp:Label> 
          <asp:Chart ID="Chart1" runat="server">
                <Series>
                    <asp:Series ChartType="Pie" Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
      
       
            <br />
        <asp:Button ID="Previous3" runat="server" Text="קודם" onclick="Previous2_Click" />
           <br /><br /><br /><br />
        </asp:View>
    </asp:MultiView>
</asp:Content>

