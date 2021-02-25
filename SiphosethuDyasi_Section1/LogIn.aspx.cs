using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiphosethuDyasi_Section1
{
	public partial class LogIn : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void logIn(object sender, EventArgs e)
		{
			//pattern variable for the pattern that validates if a proper email was inputed
			string pattern = null;
			pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

			//IF TO CHECK IF TEXTBOXES HAVE TEXT IN THEM
			if(txtUsername.Text != "" && txtPassword.Text != "")
			{
				//if to check if the email is in correct format
				if (Regex.IsMatch(txtUsername.Text, pattern))
				{
					//POPULATING THE LECTURE LIST WITH DATA FROM THE DM_MANAGER
					IList<Lecture> logIn = new DB_Manager().GetLectures(txtUsername.Text, txtPassword.Text);

					if (logIn.Count > 0)
					{
						foreach (Lecture lectureDetails in logIn)
						{
							//SESSION VARIABLES TO STORE THE LECTURE NAME, SURNAME AND THEIR ID
							Session["firstName"] = lectureDetails.FirstName.ToString();
							Session["surname"] = lectureDetails.Surname.ToString();
							Session["lectureID"] = Convert.ToInt32(lectureDetails.LectureID);
							Response.Redirect("Home_Page.aspx");
				
						}
					}
					else
					{
						//CHANGING THE BACK COLOUR OF TEXTBOXES IF THE USER INPUTTED INCORRECT USERNAME OR PASSWORD
						txtUsername.BackColor = System.Drawing.Color.LavenderBlush;
						txtPassword.BackColor = System.Drawing.Color.LavenderBlush;
						lblErrorMessage.Visible = true;
						lblErrorMessage.Text = "Incorrect username or Password";
					}
				}
				else
				{
					txtUsername.BackColor = System.Drawing.Color.LavenderBlush;
					lblErrorMessage.Visible = true;
					lblErrorMessage.Text = "Incorrect/Invalid Email";
				}
			}
			else
			{
				//CHANGING THE BACK COLOUR OF TEXTBOXES IF THE USER TRIES LOGGING IN WITH AN EMAIL OR PASSWORD INPUT
				txtUsername.BackColor = System.Drawing.Color.LavenderBlush;
				txtPassword.BackColor = System.Drawing.Color.LavenderBlush;
				lblErrorMessage.Visible = true;
				lblErrorMessage.Text = "Please input username or password";
			}

		}
	}
}