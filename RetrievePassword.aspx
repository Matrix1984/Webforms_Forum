<%@ Page Title="" Language="C#" MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="RetrievePassword.aspx.cs" Inherits="RetrievePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <br />
<br />&nbsp;<div style=" direction:rtl">
    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" 
        GeneralFailureText="נסיון להשיב את הסיסמה נכשל" 
        QuestionFailureText="תשובתך לא אומתה" 
        QuestionInstructionText="ענה על השאלה הבאה בכדי לקבל את סיסמתך בחזרה" 
        QuestionLabelText="שאלה:" QuestionTitleText="אימות זהות" SubmitButtonText="שלח" 
        SuccessText="" UserNameFailureText="לא יכלנו למצוא את שמך" 
        UserNameInstructionText="הכנס את שם המשתמש שלך" UserNameLabelText="שם משתמש:" 
        UserNameRequiredErrorMessage="יש צורך בשם משתמש" UserNameTitleText="" 
        AnswerLabelText="תשובה" AnswerRequiredErrorMessage="יש צורך בתשובה" ViewStateMode="Enabled" 
            onsendingmail="PasswordRecovery1_SendingMail">
    </asp:PasswordRecovery>
    </div>
<br /> 
<br />
    </asp:Content>

