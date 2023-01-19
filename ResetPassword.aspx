<%@ Page Title="" Language="C#" MasterPageFile="~/YourGuruMaster.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="CheckYourEmail" %>
<%@ PreviousPageType VirtualPath="~/Registration.aspx" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div style="direction:rtl">
    <asp:ChangePassword ID="ChangePassword1" runat="server" 
        ChangePasswordButtonText="שנה סיסמה" ChangePasswordFailureText="סיסמה לא תקינה" 
        ChangePasswordTitleText="איפוס סיסמה" 
        ConfirmNewPasswordLabelText="הקלד את הסיסמה החדשה שוב:" 
        ConfirmPasswordCompareErrorMessage="הסיסמה החדשה והאימות שלה לא תואמים" 
        ConfirmPasswordRequiredErrorMessage="דרוש אימות של הסיסמה החדשה" 
        NewPasswordLabelText="סיסמה חדשה:" 
        NewPasswordRegularExpressionErrorMessage="בבקשה הכנס סיסמה שונה" 
        NewPasswordRequiredErrorMessage="דרושה סיסמה חדשה" PasswordLabelText="סיסמה:" 
        PasswordRequiredErrorMessage="יש צורך בסיסמה" SuccessText="סיסמתך שונתה" 
        SuccessTitleText="שינוי הסיסמה בוצע" UserNameLabelText="שם משתמש:" 
        UserNameRequiredErrorMessage="יש צורך בשם משתמש" CancelButtonText="בטל" 
        CancelDestinationPageUrl="~/Registration.aspx" 
            NewPasswordRegularExpression="(?=.*\d)(?=.*[A-Za-z]).{2,}" 
            ContinueDestinationPageUrl="~/UserControl.aspx">
        <ValidatorTextStyle BorderColor="Red" />
    </asp:ChangePassword>
    </div>
    <br />
</asp:Content>

