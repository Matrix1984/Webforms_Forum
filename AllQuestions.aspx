<%@ Page Title="" Language="C#" EnableEventValidation="false"  MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="AllQuestions.aspx.cs" Inherits="AllQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
   table#QuestionTable{position:absolute; border-bottom-width:thin; border-color:Orange; text-align:right}
        .style1
        {
            height: 58px;
        }
        .style2
        {
            height: 58px;
            width: 289px;
        }
 </style>
 <script type="text/javascript">
     function radioClick(e) {

         var flag = e.getAttribute('how_many_clicked');
         var times = Number(flag);
         times += 1;
         e.setAttribute('how_many_clicked', times.toString())
         if (times > 1) {
             e.checked = false;
             e.setAttribute('how_many_clicked', "0");
         }
         else {
             e.checked = true;
         }
     }

 </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div align="center" runat="server" style="height: 130px; direction: ltr;">
        <br />
        <table style="width: 945px; height: 25px;" id="Table1">
             <tr>
                      <td class="style2">
 <asp:LoginView ID="LoginView2" runat="server">                        
   <RoleGroups>
    <asp:RoleGroup Roles="Moderator, Administrator">
      <ContentTemplate>
                   
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>        
          <asp:DropDownList ID="ModeratorDropDownListSubtopic" runat="server"  Width="92px">
                                                       <asp:ListItem Enabled="False">-------------------</asp:ListItem>
                                </asp:DropDownList>       
            <asp:DropDownList ID="ModeratorDropDownListTopic" AutoPostBack="true" runat="server">
                 <asp:ListItem>אומנות</asp:ListItem>
               <asp:ListItem>עסקים וכלכלה</asp:ListItem>
               <asp:ListItem>תחבורה</asp:ListItem>
               <asp:ListItem>מחשבים</asp:ListItem>
               <asp:ListItem>מוצרי חשמל</asp:ListItem>
               <asp:ListItem>חינוך</asp:ListItem>
               <asp:ListItem>בידור</asp:ListItem>
               <asp:ListItem>משפחה ויחסים</asp:ListItem>
               <asp:ListItem>אוכל ושתייה</asp:ListItem>
               <asp:ListItem>משחקים וזמן פנוי</asp:ListItem>
               <asp:ListItem>בריאות</asp:ListItem>
               <asp:ListItem>בית וגינון</asp:ListItem>
               <asp:ListItem>חדשות</asp:ListItem>
               <asp:ListItem>חיות בית</asp:ListItem>
               <asp:ListItem>פוליטיקה</asp:ListItem>
               <asp:ListItem>הריון והורות</asp:ListItem>
               <asp:ListItem>מתמתיקה ומדע</asp:ListItem>
               <asp:ListItem>מדעי החברה</asp:ListItem>
               <asp:ListItem>חברה ותרבות</asp:ListItem>
               <asp:ListItem>ספורט</asp:ListItem>
               <asp:ListItem>טיולים</asp:ListItem>
               <asp:ListItem>צבא</asp:ListItem>
            </asp:DropDownList>
                               
             
          </ContentTemplate>
    </asp:UpdatePanel>
                                <td>
                            <asp:Button ID="PassThreadTo" runat="server" Text="העבר אל" Height="27px" 
                                Width="79px" UseSubmitBehavior="False" />
           </td> 
       </ContentTemplate>
   </asp:RoleGroup>
  </RoleGroups>
</asp:LoginView>
                            </td>


              <td>
              <asp:LoginView ID="LoginView1" runat="server">
              <RoleGroups>
              <asp:RoleGroup Roles="Moderator, Administrator">
              <ContentTemplate>
                        <asp:Button ID="DeleteThread" runat="server" Text="מחק" Height="27px" 
                            Width="79px" />
                     </ContentTemplate>
                   </asp:RoleGroup>
              </RoleGroups>
              </asp:LoginView>
                        </td>
                    <td class="style1">
                        <asp:Button ID="Ask" runat="server" Text="שאל" Width="79px" Height="27px" 
                            PostBackUrl="~/AskQuestion.aspx" />     &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Answer" runat="server" Text="חפש" Width="79px" 
                            Height="27px" onclick="Answer_Click" />
                      
                        </td>
                       
                    <td class="style1">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

                        &nbsp   &nbsp   &nbsp
                        <asp:DropDownList ID="SubTopicDropDownList" runat="server" Width="92px">
                            <asp:ListItem Enabled="False">-------------------</asp:ListItem>
                        </asp:DropDownList>
                         &nbsp   &nbsp   &nbsp

<asp:DropDownList ID="topicDropDownMenu" runat="server"  AutoPostBack="True" onselectedindexchanged="topicDropDownMenu_SelectedIndexChanged">                        
<asp:ListItem>אומנות</asp:ListItem>
               <asp:ListItem>עסקים וכלכלה</asp:ListItem>
               <asp:ListItem>תחבורה</asp:ListItem>
               <asp:ListItem>מחשבים</asp:ListItem>
               <asp:ListItem>מוצרי חשמל</asp:ListItem>
               <asp:ListItem>חינוך</asp:ListItem>
               <asp:ListItem>בידור</asp:ListItem>
               <asp:ListItem>משפחה ויחסים</asp:ListItem>
               <asp:ListItem>אוכל ושתייה</asp:ListItem>
               <asp:ListItem>משחקים וזמן פנוי</asp:ListItem>
               <asp:ListItem>בריאות</asp:ListItem>
               <asp:ListItem>בית וגינון</asp:ListItem>
               <asp:ListItem>חדשות</asp:ListItem>
               <asp:ListItem>חיות בית</asp:ListItem>
               <asp:ListItem>פוליטיקה</asp:ListItem>
               <asp:ListItem>הריון והורות</asp:ListItem>
               <asp:ListItem>מתמתיקה ומדע</asp:ListItem>
               <asp:ListItem>מדעי החברה</asp:ListItem>
               <asp:ListItem>חברה ותרבות</asp:ListItem>
               <asp:ListItem>ספורט</asp:ListItem>
               <asp:ListItem>טיולים</asp:ListItem>
               <asp:ListItem>צבא</asp:ListItem>
            </asp:DropDownList>
     </ContentTemplate>

</asp:UpdatePanel>
                       </td>

               
                      
                 </tr>
            </table>
            <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
            
            <br />

        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                   
        <br />
        <asp:PlaceHolder ID="PlaceHolder3" runat="server"></asp:PlaceHolder>
                   
</div>



</asp:Content>

