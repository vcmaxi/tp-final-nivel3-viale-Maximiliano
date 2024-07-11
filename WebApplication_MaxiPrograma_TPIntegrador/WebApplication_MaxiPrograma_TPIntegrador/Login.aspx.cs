using System;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;
namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class Login: System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            if(!IsPostBack) {
                if(Session["isFromLogin"]==null) {
                    Session["isFromLogin"]=true;
                }
            } else {
                foreach(BaseValidator validator in Page.Validators) {
                    validator.Validate();
                }
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e) {
            lblErrorLogin.Visible=false;
            if(Page.IsValid) {
                try {

                    if(Help.IsSessionContaining(this.Context, GlobalVariables.isFromLogin)) {
                        User user = new User();
                        user.Email=txtEmail.Text;
                        user.Pass=txtPass.Text;
                        if(new UserManager().HasLogin(user)) {
                            Session[GlobalVariables.userString]=user;
                            if(user.Admin) {
                                Response.Redirect("ArticleList.aspx", false); return;
                            }
                            Response.Redirect("Default.aspx", false); return;
                        }
                        lblErrorLogin.Text="Please enter a correct username and password.<br> Note that password fields is case-sensitive.";
                        lblErrorLogin.Visible=true;
                    } else { //si entro al if y no proviene de login, es un regitro nuevo
                        if((bool)Session[GlobalVariables.isEmailInUse]) {
                            panelRegister.Visible=true;
                        }
                    }
                } catch(Exception ex) {
                    Help.RedirectToErrorPage(this.Context, ex.ToString());
                }
            }
        }
        protected void btnConfirmYes_Click(object sender, EventArgs e) {
            try {
                User user = new User();
                user.Email=txtEmail.Text;
                user.Pass=txtPass.Text;
                if(!(new UserManager().UserExistInDb(user.Email))) {
                    user.Id=new UserManager().InsertUser(user);
                    Session[GlobalVariables.userString]=user;
                    Response.Redirect("Profile.aspx", false); return;
                }
                lblErrorLogin.Text="User already exist. Use another email address.";
                lblErrorLogin.Visible=true;
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }

        }
        protected void btnConfirmNo_Click(object sender, EventArgs e) {
            panelRegister.Visible=false;
            txtPass.Text="";
        }
        protected void txtEmail_TextChanged(object sender, EventArgs e) {
            try {
                bool B = true;
                if(!(Help.IsSessionContaining(this.Context, GlobalVariables.isFromLogin))) {
                    lblEmailExist.Visible=true;
                    User user = new User();
                    user.Email=txtEmail.Text;
                    if((new UserManager().UserExistInDb(user.Email))) {
                        lblEmailExist.Text="User already exist. Use another email address.";
                        lblEmailExist.CssClass="validatingForms";
                        lblEmailExist.Visible=true;
                        B=false;
                    } else {
                        lblEmailExist.Text="Email is not being used. 😃";
                        lblEmailExist.Visible=true;
                        lblEmailExist.CssClass="successfullyValidated";
                    }
                }
                Session[GlobalVariables.isEmailInUse]=B;
                hfFocus.Value=txtPass.ClientID;
            } catch(Exception ex) {
                Help.RedirectToErrorPage((this.Context), ex.ToString());
            }
        }
    }
}
