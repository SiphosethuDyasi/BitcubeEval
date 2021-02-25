using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiphosethuDyasi_Section1
{
	public partial class Home_Page : System.Web.UI.Page
	{
		DB_Manager manager = new DB_Manager();

		protected void Page_Load(object sender, EventArgs e)
		{
			lblFullName.Text = Session["firstName"].ToString() + " "  + Session["surname"].ToString();

			getDegree();

		}

		private void getDegree()
		{
			IList<Degree> degrees = new DB_Manager().GetDegrees(Convert.ToInt32(Session["lectureID"]));

			if (degrees.Count > 0)
			{
					DegreeGrid.DataSource = degrees;
					DegreeGrid.DataBind(); ;
			}

		}
		protected void DegreeGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DegreeGrid, "Select$" + e.Row.RowIndex);
				e.Row.Attributes["style"] = "cursor:pointer";
			}
			
		}

		protected void DegreeGrid_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selectedIndex = Convert.ToInt32(DegreeGrid.SelectedIndex);
			Session["degreeID"] = DegreeGrid.DataKeys[selectedIndex]["degreeID"].ToString();

			getStudents(Convert.ToInt32(Session["degreeID"]));
			getCourse(Convert.ToInt32(Session["degreeID"]));
		}

		protected void StudentGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
		{
			//NewEditIndex property used to determine the index of the row being edited.  
			StudentGrid.EditIndex = e.NewEditIndex;
			
			getStudents(Convert.ToInt32(Session["degreeID"]));
		}

		protected void CourseGrid_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
		{
			//NewEditIndex property used to determine the index of the row being edited.  
			CourseGrid.EditIndex = e.NewEditIndex;

			getCourse(Convert.ToInt32(Session["degreeID"]));
		}

		protected void StudentGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
		{
			//Finding the controls from Gridview for the row which is going to update  
			TextBox studentName = StudentGrid.Rows[e.RowIndex].FindControl("txtStudentName") as TextBox;
			TextBox studentSurname = StudentGrid.Rows[e.RowIndex].FindControl("txtStudentSurname") as TextBox;
			TextBox studentEmail = StudentGrid.Rows[e.RowIndex].FindControl("txtStudentEmail") as TextBox;
			TextBox studentDOB = StudentGrid.Rows[e.RowIndex].FindControl("txtStudentDOB") as TextBox;
			Label studentID = StudentGrid.Rows[e.RowIndex].FindControl("lblStudentID") as Label;

			if (txtName.Text != "" && txtSurname.Text != "" && txtDOB.Text != "" && txtEmail.Text != "")
			{
				if (manager.updateStudents(studentName.Text, studentSurname.Text, studentEmail.Text, studentDOB.Text, Convert.ToInt32(studentID.Text)).Equals(true))
				{
					//Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
					StudentGrid.EditIndex = -1;
					//Call ShowData method for displaying updated data  
					getStudents(Convert.ToInt32(Session["degreeID"]));
				}
				else
				{
					lblStudentNum.Text = "Update Failed";
				}
			}
			else
			{
				studentName.BackColor = System.Drawing.Color.LavenderBlush;
				studentSurname.BackColor = System.Drawing.Color.LavenderBlush;
				studentDOB.BackColor = System.Drawing.Color.LavenderBlush;
				studentEmail.BackColor = System.Drawing.Color.LavenderBlush;
			}
				
		}

		protected void CourseGrid_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
		{
			//Finding the controls from Gridview for the row which is going to update  
			TextBox courseName = CourseGrid.Rows[e.RowIndex].FindControl("txtCourseName") as TextBox;
			TextBox courseDuration = CourseGrid.Rows[e.RowIndex].FindControl("txtCourseDuration") as TextBox;
			Label courseID = CourseGrid.Rows[e.RowIndex].FindControl("lblCourseID") as Label;

			if (txtCName.Text != "" && txtDuration.Text != "")
			{
				if (manager.updateCourse(courseName.Text, Convert.ToInt32(courseDuration.Text), Convert.ToInt32(courseID.Text)).Equals(true))
				{
					//Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
					CourseGrid.EditIndex = -1;
					//Call ShowData method for displaying updated data  
					getCourse(Convert.ToInt32(Session["degreeID"]));
				}
				else
				{
					lblCourseNum.Text = "Update Failed";
				}
			}
			else
			{
				courseName.BackColor = System.Drawing.Color.LavenderBlush;
				courseDuration.BackColor = System.Drawing.Color.LavenderBlush;
			}

		}

		protected void StudentGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
		{
			//Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
			StudentGrid.EditIndex = -1;
			getStudents(Convert.ToInt32(Session["degreeID"]));
		}

		protected void CourseGrid_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
		{
			//Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
			CourseGrid.EditIndex = -1;
			getCourse(Convert.ToInt32(Session["degreeID"]));
		}

		protected void StudentGrid_RowDeleting(object sender,  System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			Label studentID = StudentGrid.Rows[e.RowIndex].FindControl("lblStudentID") as Label;

			if (manager.deleteStudent(Convert.ToInt32(studentID.Text)).Equals(true))
			{
				//Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
				StudentGrid.EditIndex = -1;
				//Call ShowData method for displaying updated data  
				getStudents(Convert.ToInt32(Session["degreeID"]));
				lblStudentNum.Text = "Delete Successful";
			}
			else
			{
				lblStudentNum.Text = "Delete failed";
			}
			
		}

		protected void CourseGrid_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
		{
			Label courseID = CourseGrid.Rows[e.RowIndex].FindControl("lblCourseID") as Label;

			if (manager.deleteCourse(Convert.ToInt32(courseID.Text)).Equals(true))
			{
				//Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
				CourseGrid.EditIndex = -1;
				//Call ShowData method for displaying updated data  
				getCourse(Convert.ToInt32(Session["degreeID"]));
				lblCourseNum.Text = "Delete Successful";
			}
			else
			{
				lblCourseNum.Text = "Delete failed";
			}

		}

		private void getStudents(int degreeID)
		{
			IList<Student> students = new DB_Manager().GetStudents(degreeID);

			if (students.Count > 0)
			{
				StudentGrid.DataSource = students;
				StudentGrid.DataBind();
			}
			else
			{
				DataTable dt = new DataTable();

				dt.Columns.Add("StudentID");
				dt.Columns.Add("StudentName");
				dt.Columns.Add("StudentSurname");
				dt.Columns.Add("StudentEmail");
				dt.Columns.Add("StudentDOB");
				dt.Columns.Add("Edit");
				dt.Rows.Add(0, "", "", "", "", "");
				StudentGrid.DataSource = dt;
				StudentGrid.DataBind();
				StudentGrid.Rows[0].Visible = false; 

			}
		}

		private void getCourse(int degreeID)
		{
			IList<Course> courses = new DB_Manager().GetCourses(degreeID);

			if (courses.Count > 0)
			{
				CourseGrid.DataSource = courses;
				CourseGrid.DataBind(); ;
			}
			else
			{
				DataTable dt = new DataTable();

				dt.Columns.Add("CourseID");
				dt.Columns.Add("CourseName");
				dt.Columns.Add("Duration");
				dt.Columns.Add("Edit");
				dt.Rows.Add(0, "", 0, "");
				CourseGrid.DataSource = dt;
				CourseGrid.DataBind();
				CourseGrid.Rows[0].Visible = false;

			}
		}

		protected void Open(object sender, EventArgs e)
		{
			addDiv.Visible = true;
		}

		protected void OpenCourse(object sender, EventArgs e)
		{
			DivCourse.Visible = true;
		}

		protected void Save(object sender, EventArgs e)
		{
			if(txtName.Text != "" && txtSurname.Text != "" && txtDOB.Text != "" && txtEmail.Text != "")
			{
				if (manager.addStudent(txtName.Text, txtSurname.Text, txtEmail.Text, txtDOB.Text, Convert.ToInt32(Session["degreeID"])).Equals(true))
				{
					getStudents(Convert.ToInt32(Session["degreeID"]));
					addDiv.Visible = false;
				}
				
			}
			else
			{
				txtName.BackColor = System.Drawing.Color.LavenderBlush;
				txtSurname.BackColor = System.Drawing.Color.LavenderBlush;
				txtDOB.BackColor = System.Drawing.Color.LavenderBlush;
				txtEmail.BackColor = System.Drawing.Color.LavenderBlush;
			}
			
			
		}

		protected void SaveCourse(object sender, EventArgs e)
		{
			if(txtCName.Text != "" && txtDuration.Text != "")
			{
				if (manager.addCourse(txtCName.Text, Convert.ToInt32(txtDuration.Text), Convert.ToInt32(Session["degreeID"])).Equals(true))
				{
					getCourse(Convert.ToInt32(Session["degreeID"]));
					DivCourse.Visible = false;
				}
			} else
			{
				txtCName.BackColor = System.Drawing.Color.LavenderBlush;
				txtDuration.BackColor = System.Drawing.Color.LavenderBlush;
			}
			
			
		}
		protected void LogOut(object sender, EventArgs e)
		{
			Response.Redirect("LogIn.aspx");
		}
	}
}