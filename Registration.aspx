 <%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style type="text/css">
        #register{ position:absolute; left:0px;
            top: -5px;
            width: 304px;
        margin-left: 0px;
    }
        #entryTable
        {
             position:absolute; right:593px;
            width: 373px;
            top: 0px;
        }
        
  #container{ position:relative;
            top: 16px;
            left: 240px;
            height: 379px;
        width: 320px;
            margin-right: 0px;
        }
        .style4
        {
            width: 376px;
            height: 10px;
        }
        </style>

    <title></title>
</head>
<body>



    <form id="form1" runat="server">
    <div align="center" style="direction: ltr">
   <asp:Image ID="Image1" runat="server" ImageUrl="~/header.png" />
     <div align="right" dir="rtl" id="container">

   



    <div id="entryTable">
         <table>
        <tr>
        <td class="style4">
     
            <asp:Label ID="EntryLabel" runat="server" Font-Bold="True" Font-Size="XX-Large" 
                    Text="כניסה"></asp:Label>
                        <br />
                    </td>
                    </tr>
                    <tr>
                <td class="style4">
                  <asp:Login ID="Login1" runat="server" 
                        FailureText="חיבורך לא הייה מוצלח. אנא נסה שנית" LoginButtonText="התחבר" 
                        PasswordLabelText="סיסמה:" PasswordRequiredErrorMessage="יש צורך בסיסמה" 
                        RememberMeText="זכור אותי פעם הבאה" TitleText="" UserNameLabelText="שם משתמש:" 
                        UserNameRequiredErrorMessage="יש צורך בשם משתמש" Height="100px" 
                        DestinationPageUrl="~/AllQuestions.aspx" PasswordRecoveryText="שכחת סיסמה" 
                        PasswordRecoveryUrl="~/RetrievePassword.aspx" RememberMeSet="True" 
                        onauthenticate="Login1_Authenticate" >
                       
                      <CheckBoxStyle Height="50px" />
                       <ValidatorTextStyle BorderColor="#CC0000" />
                       </asp:Login>
                    </td>
            </tr>
            <tr>
                   <td class="style4">

                   <br />
                  <br /> 
                 
                       <br /> 
                                                                                                                 
   YourGuru זה אתר שעוזר לך להתחבר עם אנשים שיכולים  <br /> לענות על השאלות שלך .
חושב שיש לך שאלה, שאל ויענו לך.<br />
   חושב שאתה חכם, נראה על כמה שאלות תוכל לענות.  
                   </td>
            </tr>
      
      </table>   
      </div>                                                 
         <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" 
             AnswerLabelText="תשובת ביטחון:" 
             AnswerRequiredErrorMessage="יש להקליד שאלת ביטחון" 
             CompleteSuccessText="חשבונך נוצר בהצלחה" 
             ConfirmPasswordCompareErrorMessage="הססימאות לא זהות" 
             ConfirmPasswordLabelText="אמת את הסיסמה:" 
             ConfirmPasswordRequiredErrorMessage="יש צורך באימות סיסמה" 
             ContinueDestinationPageUrl="~/AllQuestions.aspx" 
             CreateUserButtonText="צור חשבון" 
             DuplicateEmailErrorMessage="האיימל כבר קיים במערכת" 
             DuplicateUserNameErrorMessage="שם משתמש זה כבר קיים במערכת" 
             EmailLabelText="איימל:" 
             EmailRegularExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
             EmailRegularExpressionErrorMessage="" 
             EmailRequiredErrorMessage="יש צורך באיימל" 
             FinishDestinationPageUrl="~/AllQuestions.aspx" 
             InvalidAnswerErrorMessage="בבקשה הכנס תשובת ביטחון אחרת:" 
             InvalidEmailErrorMessage="בבקשה הכנס איימל תקין" 
             InvalidPasswordErrorMessage="סיסמה שגויאה" 
             InvalidQuestionErrorMessage="בבקשה שאל שאת אימות אחרת" 
             PasswordLabelText="סיסמה:" PasswordRegularExpression="(?=.*\d)(?=.*[A-Za-z]).{2,}" 
             PasswordRegularExpressionErrorMessage="הסיסמה צריכה להיות מורכבת מאותיות באנגלית ומספרים (יותר מ 2 תווים)" 
             PasswordRequiredErrorMessage="יש צורך בסיסמה" 
             QuestionLabelText="שאלת אימות החשבון:" 
             QuestionRequiredErrorMessage="יש צורך בשאלת ביטחון" 
             UnknownErrorMessage="טעות! חשבונך לא נוצר" UserNameLabelText="שם משתמש:" 
             UserNameRequiredErrorMessage="יש צורך בשם משתמש" ViewStateMode="Enabled" 
             ContinueButtonText="כניסה" oncreateduser="CreateUserWizard1_CreatedUser" 
             Height="376px" Width="304px">
             <MailDefinition BodyFileName="~/Miscellenous/Message.html" 
                 From=" yourguru27@gmail.com" Subject="חשבונך ב YourGuru">
             </MailDefinition>
             <TitleTextStyle Font-Bold="True" Font-Size="XX-Large" HorizontalAlign="Right" />
             <ValidatorTextStyle BorderColor="#CC0000" />
             <WizardSteps>
                 <asp:CreateUserWizardStep runat="server" Title="הרשם" />
                 <asp:CompleteWizardStep runat="server" />
             </WizardSteps>
         </asp:CreateUserWizard>
</div>
</div>
    
   </form>
</body>
</html>
