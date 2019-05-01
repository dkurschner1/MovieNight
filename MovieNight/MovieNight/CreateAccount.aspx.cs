﻿using MovieNight.Models;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MovieNight
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        private MovieNightContext db = new MovieNightContext();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {
                txtActivePass.Attributes["value"] = txtActivePass.Text;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //string userName = txtUserName.Text;
            //string fName = txtFName.Text;
            //string lName = txtLName.Text;
            //string email = txtUserEmail.Text;
            //string password = txtUserPass.Text;

            string password = txtUserPass.Text;

            byte[] salt;

            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);

            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
      
            User user = new User();

            user.userName = txtUserName.Text;
            user.fName = txtFName.Text;
            user.lName = txtLName.Text;
            user.email = txtUserEmail.Text;
            user.passwordHash = savedPasswordHash;
           

            Session.Add("userAccount", user);

            MovieNightContext context= new MovieNightContext();
            context.users.Add(user);
            context.SaveChanges();

        

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           
            var email = txtActiveEmail.Text.ToString();

            var pass = txtActivePass.Text;

            
            

            Session["userAccount"] = email;


            DataView dvSql = (DataView)UserConnection.Select(DataSourceSelectArguments.Empty);



            if (dvSql.Count == 0)
            {
                emailCompare.Visible = true;
            }
            else
            {
                var query = db.users.SqlQuery("Select * from [User] Where email = '" + email + "'").First();

                string savedPasswordHash = query.passwordHash;
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                int match = 1;
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                        match = 0;
                if(match == 1)
                {
                    Session["userAccount"] = db.users.SqlQuery("SELECT [User].userID, [User].userName, [User].fName, [User].lName, [User].passwordHash, [User].email " +
                    "FROM [User] " +
                    "WHERE email = '" + email + "'").FirstOrDefault();
                    Response.Redirect("accountinfo.aspx");

                }
                else
                {
                    passCompare.Visible = true;
                }



                
                
            }
                


            



        }


        protected void Toggle_Password(object sender, EventArgs e)
        {
            var showBtnTextString = showPasswordBtn.Text;
            var passText = txtActivePass.Text;

            if (showBtnTextString == "Show")
            {
                showPasswordBtn.Text = "Hide";
                txtActivePass.TextMode = TextBoxMode.SingleLine;
                txtActivePass.Text = passText;

            }

            else
            {
                showPasswordBtn.Text = "Show";
                txtActivePass.TextMode = TextBoxMode.Password;
                txtActivePass.Text = passText;
            }
                

        }
    }
}