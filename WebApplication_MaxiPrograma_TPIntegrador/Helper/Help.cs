using System.Web;
using System.Web.UI.WebControls;

namespace Helper {
    public static class Help {
        public static bool ValidateString(string cadena) {
            if(string.IsNullOrEmpty(cadena)) {
                return false;
            }
            return true;
        }
        public static void RedirectToErrorPage(HttpContext context, string error) {
            if(context!=null) {
                context.Session["error"]=error;
                context.Response.Redirect("Error.aspx", false);

            }
        }
        public static bool IsSessionContaining(HttpContext context, string isFrom) {
            if(context!=null) {
                if(context.Session[isFrom]!=null) {
                    if((bool)context.Session[isFrom]) {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool DoesSessionContainValue(HttpContext context, string keyToLook) {
            if(context!=null) {
                if(context.Session[keyToLook]!=null) {
                    return true;
                }
            }
            return false;
        }
        public static bool IsSessionActive(HttpContext context, string user) {
            if(context!=null) {
                if(context.Session[user]!=null) {
                    return true;
                }
            }
            return false;
        }
        public static bool IsUserAdmin(HttpContext context, string user) {
            if(context!=null) {
                if(IsSessionActive(context, user)) {
                    var obj = (object)context.Session[user];
                    if(obj is Dominio.User User) {
                        if(User.Admin) { return true; }
                    }
                }
            }
            return false;
        }
        public static object ActiveUser(HttpContext context, string user) {
            if(context!=null) {
                return context.Session[user];
            }
            return null;
        }
        public static void ValidateImg(Image imageID, string imagenUrlToAdd, string errorImgPath) {
            if(string.IsNullOrEmpty(imagenUrlToAdd)) {
                imageID.ImageUrl=errorImgPath;
                imageID.Attributes["onerror"]=$"this.onerror=null;this.src='{errorImgPath}';";
            } else {
                if(imagenUrlToAdd.Contains("perfil-")) {
                    imageID.ImageUrl="./Images/"+imagenUrlToAdd;
                } else {
                    imageID.ImageUrl=imagenUrlToAdd;
                }
                imageID.Attributes["onerror"]=$"this.onerror=null;this.src='{errorImgPath}';";
            }

        }
        public static void ValidateImgButton(ImageButton imageID, string imagenUrlToAdd, string errorImgPath) {
            if(string.IsNullOrEmpty(imagenUrlToAdd)) {
                imageID.ImageUrl=errorImgPath;
                imageID.Attributes["onerror"]=$"this.onerror=null;this.src='{errorImgPath}';";
            } else {
                if(imagenUrlToAdd.Contains("perfil-")) {
                    imageID.ImageUrl="./Images/"+imagenUrlToAdd;
                } else {
                    imageID.ImageUrl=imagenUrlToAdd;
                }
                imageID.Attributes["onerror"]=$"this.onerror=null;this.src='{errorImgPath}';";
            }

        }
    }
}
