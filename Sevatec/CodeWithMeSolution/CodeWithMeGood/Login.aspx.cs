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
        try
        {
            string username = UserName.Text;
            Utiliity.Logger(username + " attempted log in");
            string password = Password.Text;

            // we can also check password complexity here by doing REGEX  --- a stronger password by a user also helps with security!
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                sendLoginError();
                return;
            }

            var pass_hash = CallSQL(username);


            if (pass_hash == "")
            {
                sendLoginError();
                Utiliity.Logger(username + " log in failed");
            }
            else
            {
                if (comparepasswords(password, pass_hash) == true)
                {
                    FormsAuthentication.SetAuthCookie(username, false);
                    Response.Redirect("Data.aspx");
                }
                else
                {
                    sendLoginError();
                    Utiliity.Logger(username + " log in failed");
                }
            }
        }
        catch (Exception ex)
        {
            Utiliity.Logger("ERROR -- "+ex.Message);

        }

        
    }

    private void sendLoginError()
    {
        string script = "alert(\"Invalid Username or Password!!\");";
     
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", script, true);
    }

    private string CallSQL(string username)
    {
        string pass_hash = "";
        try
        {

            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlDataReader reader;
            using (SqlConnection con = new SqlConnection(cs))
            {
                using (SqlCommand cmd = new SqlCommand("Get_UserInfo"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Connection = con;
                    con.Open();
                    

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            pass_hash = rdr.GetString(1); //The 0 stands for "the 0'th column", so the first column of the result.

                        }
                    }
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Utiliity.Logger("ERROR -- " + ex.Message);

        }
        return pass_hash;

    }

    private bool comparepasswords(string password, string hashed_password)
    {

        var stored_salt = hashed_password.Substring(0, 24);
        var stored_PasswordHashed = hashed_password.Substring(24);

        var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(stored_salt), 10000);
        byte[] hash = pbkdf2.GetBytes(20);
        var current_PasswordHashed = Convert.ToBase64String(hash);

        if (current_PasswordHashed.Equals(stored_PasswordHashed))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}