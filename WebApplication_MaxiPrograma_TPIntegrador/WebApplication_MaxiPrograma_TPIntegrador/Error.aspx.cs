using System;

namespace WebApplication_MaxiPrograma_TPIntegrador {
    public partial class Error: System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            txtError.Text=Session["error"].ToString();
        }
    }
}