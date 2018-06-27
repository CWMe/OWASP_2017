using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login_Click(object sender, EventArgs e)
    {
       
        string username = UserName.Text;
        string password = Password.Text;
        string passwordSQL = "";

        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand("Select username,password From UsersBad where username = '" + username + "'"))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();


                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        passwordSQL = rdr.GetString(1); //The 0 stands for "the 0'th column", so the first column of the result.

                    }
                }
                con.Close();
            }
        }


        if (password.Equals(passwordSQL))
        {
            Response.Redirect("Data_Bad.aspx");
        }
        

        
    }

    private void sendLoginError()
    {
        string script = "alert(\"Invalid Username or Password!!\");";
     
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", script, true);
    }

   


}