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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SignUp_Click(object sender, EventArgs e)
    {
        int userId = 0;
        string username = UserName.Text;
        string password = Password.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            sendError();
            return;
        }

        string pass_hash = HashPassword(password);

        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand("Insert_User"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", pass_hash);
                cmd.Connection = con;
                con.Open();
                userId = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            
        }

        if (userId > 0)
        {
            FormsAuthentication.SetAuthCookie(username, false);
            Response.Redirect("Data.aspx");
        }
        else
        {
            sendError();
        }
        
    }

    private void sendError()
    {
        string script = "alert(\"Invalid Username or Password!!\");";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", script, true);
    }

    private string HashPassword(string pass)
    {
        try
        {
            
            // Generate the hash, with an automatic 16 generic byte salt
            
            var pbkdf2 = new Rfc2898DeriveBytes(pass, 16, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] salt = pbkdf2.Salt;
            //Return the salt and the hash
            var temp = Convert.ToBase64String(salt);
            return Convert.ToBase64String(salt) + Convert.ToBase64String(hash);
        }
        catch (Exception)
        {

        }

        return "";
    }


}