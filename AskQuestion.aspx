<%@ Page Title="" Language="C#" MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="AskQuestion.aspx.cs" Inherits="AskQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    <style type="text/css">
        .style1
        {
            width: 18px;
        }
        .style2
        {
        width: 705px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<br />


<table style="width: 916px">
<tr>
    <td class="style2"  align="right">
 
        <p dir="rtl" >
        <asp:TextBox ID="QuestionTextBox1" runat="server" Width="702px" AutoPostBack="True" 
                MaxLength="90"></asp:TextBox>
       </p>

    </td>
    <td class="style1" align="left">
              <asp:Label ID="QuestionLabel1" runat="server" Text="שאלה" ></asp:Label>
          
    </td>
</tr>

<tr>
     <td class="style2" align="right">
         <p dir="rtl" >
          <asp:TextBox  ID="QuestionDetailsTextBox2" runat="server" Height="161px" 
          Width="702px" MaxLength="5000" TextMode="MultiLine"></asp:TextBox>
          </p>
    </td>
    
      <td class="style1" align="left">

          <asp:Label ID="Details" runat="server"  Text="פרטים  " 
               Width="45px" Height="155px"></asp:Label>
              
      </td>

</tr>
<tr>
     <td class="style2" align="right" dir="rtl">
   
  
           <asp:DropDownList ID="topicDropDownMenu" runat="server" AutoPostBack="True" onselectedindexchanged="topicDropDownMenu_SelectedIndexChanged1" 
           >
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
            <asp:DropDownList ID="SubTopicDropDownList" runat="server" Width="120px">
            </asp:DropDownList>
               <br /><br />
    </td>

    <td class="style1"  align="right">
     <asp:Label ID="Label1" runat="server" Text="קטגוריה"> </asp:Label>
     <br /><br />
    </td>

</tr>
<tr>
        <td align="right" class="style2">

            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="QuestionDetailsTextBox2" EnableTheming="True" 
                ErrorMessage="אסור להשאיר שדות רקים" Font-Bold="True" ForeColor="#FF3300"></asp:RequiredFieldValidator><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="SubTopicDropDownList" ErrorMessage="בחר קטגטריה" 
                Font-Bold="True" ForeColor="#FF3300"></asp:RequiredFieldValidator><br />

            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="QuestionTextBox1" ErrorMessage=" שאל שאלה" 
                Font-Overline="False" ForeColor="Red" Font-Bold="True"></asp:RequiredFieldValidator><br />

            <asp:Button ID="sendButton1" runat="server" Text="שלח" Width="60px" 
                onclick="sendButton1_Click" />

        </td>
</tr>
</table>
</asp:Content>

