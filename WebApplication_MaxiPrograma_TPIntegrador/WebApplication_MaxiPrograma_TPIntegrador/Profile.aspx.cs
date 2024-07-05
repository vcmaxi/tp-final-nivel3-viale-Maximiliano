using System;
using System.Web.UI.WebControls;
using Dominio;
using Helper;
using Manager;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class Profile: System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

            if(!IsPostBack) {
                if(Help.IsSessionActive(this.Context, GlobalVariables.userString)) {
                    // se cargan los valores de trainer (pueden no estar completos por ej: al registrarse por primera vez)
                    User user = (User)Help.ActiveUser(this.Context, GlobalVariables.userString);
                    txtEmail.Text=user.Email;
                    txtNombre.Text=user.Nombre;
                    txtApellido.Text=user.Apellido;
                    imgProfile.ImageUrl="~/Images/"+user.ImagenPerfil;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {
            lblExtensionError.Visible=false;
            try {

                User user = (User)Help.ActiveUser(this.Context, GlobalVariables.userString);
                user.Nombre=txtNombre.Text;
                user.Apellido=txtApellido.Text;

                if(profileImgUpload.PostedFile!=null) {
                    string fileExtension = System.IO.Path.GetExtension(profileImgUpload.PostedFile.FileName).ToLower();
                    if(!(fileExtension.Length==0)) {// el usuario modifica los otros parametros pero no la foto
                        if(fileExtension==".jpg"||fileExtension==".jpeg") {
                            string imagePath = Server.MapPath("./Images/");
                            imagePath+="perfil-"+user.Id+fileExtension;
                            profileImgUpload.PostedFile.SaveAs(imagePath);

                            user.ImagenPerfil="perfil-"+user.Id+fileExtension;
                            imgProfile.ImageUrl="./images/"+user.ImagenPerfil;

                            // Actualizar la imagen en el MasterPage
                            Image img = (Image)Master.FindControl("imgProfile");
                            img.ImageUrl="~/Images/"+user.ImagenPerfil;
                        } else {
                            lblExtensionError.Visible=true;
                            lblExtensionError.Text="Only .jpg and .jpeg file formats are allowed for upload.<br> Please select a valid JPEG image file.";
                            return;
                        }
                    }
                }
                new UserManager().Updateuser(user);
            } catch(Exception ex) {
                Help.RedirectToErrorPage(this.Context, ex.ToString());
            }
        }
    }
}