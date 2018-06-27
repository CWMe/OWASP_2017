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
        

        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            using (SqlCommand cmd = new SqlCommand(@"SET NOCOUNT ON;
                IF EXISTS(SELECT UserId FROM UsersBad WHERE Username = '"+username+@"')
                BEGIN
                    SELECT -1 -- Username exists.
                END
                ELSE
                BEGIN
                    INSERT INTO [UsersBad]
                                ([Username]
                                ,[Password]
                                )
                    VALUES
                    ('"+username+ @"'
                    ,'" + password + @"'
                )
           
                SELECT SCOPE_IDENTITY() -- UserId                 
                END"))
            {
                cmd.CommandType = CommandType.Text;
                
                cmd.Connection = con;
                con.Open();
                userId = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            
        }

        if (userId > 0)
        {
           Response.Redirect("Data_Bad.aspx");

        }
        else if (userId == -1)
        {
            sendError("Username already exsists, please pick another one!");
        }
        
    }

    private void sendError(string error)
    {
        string script = "alert(\""+error+"\");";

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", script, true);
    }

    


}