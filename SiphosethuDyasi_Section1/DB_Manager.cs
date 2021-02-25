using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SiphosethuDyasi_Section1
{
	public class DB_Manager
	{
		//Declaration for SQL connection
		SqlConnection SqlConn = null;

		//Make connection method that connects to the database 
		private void MakeConnection()
		{
			string connectionString = ConfigurationManager.ConnectionStrings["DBConnectString"].ToString();
			// Only create new connection object if it does not already exists
			if (SqlConn == null)
			{
				SqlConn = new SqlConnection(connectionString);
			}
		}

		public SqlConnection MakeConnection(SqlConnection sqlConn)
		{
			// Getting the connection string from the web.config file
			string connectionString = ConfigurationManager.ConnectionStrings["Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\sipho\\OneDrive\\Documents\\Internship\\SiphosethuDyasi_SectionOne\\SiphosethuDyasi_SectionOne\\App_Data\\FullDatabase.mdf;Integrated Security=True;Connect Timeout=30"].ToString();

			// Only create new connection object if it does not already exists
			if (sqlConn == null)
			{
				sqlConn = new SqlConnection(connectionString);
			}

			return sqlConn;
		}

		public List<Lecture> GetLectures(string lectureEmail, string lecturePassword)
		{

			List<Lecture> lecture = new List<Lecture>();
			try
			{
				MakeConnection();
				String sqlString = "SELECT * FROM LECTURE_TBL WHERE lectureEmail = '" + lectureEmail + "' AND lecturePassword = '" + lecturePassword + "';";
				SqlDataAdapter sqlCmd = new SqlDataAdapter(sqlString, SqlConn);
				DataSet ds = new DataSet();
				sqlCmd.Fill(ds);
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					int lectureID = Convert.ToInt32(row["lectureID"]);
					string lectureName = row["lectureName"].ToString();
					string lectureSurname = row["lectureSurname"].ToString();
					string email = row["lectureEmail"].ToString();
					string dob = row["lectureDOB"].ToString();
					string password = row["lecturePassword"].ToString();

					lecture.Add(new Lecture(lectureID, lectureName, lectureSurname, dob, email, password));

				}

			}
			catch (Exception ex)
			{

			}
			return lecture;
		}

		public List<Degree> GetDegrees(int lectureID)
		{

			List<Degree> degrees = new List<Degree>();
			try
			{
				MakeConnection();
				String sqlString = "SELECT DEGREE_TBL.degreeID, DEGREE_TBL.degreeName, DEGREE_TBL.degreeDuration FROM DEGREE_TBL " +
									"INNER JOIN LECTURE_DEGREE_TBL ON(LECTURE_DEGREE_TBL.degreeID = DEGREE_TBL.degreeID) " +
									"WHERE LECTURE_DEGREE_TBL.lectureID = " + lectureID + ";";
				SqlDataAdapter sqlCmd = new SqlDataAdapter(sqlString, SqlConn);
				DataSet ds = new DataSet();
				sqlCmd.Fill(ds);
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					int degreeID = Convert.ToInt32(row["degreeID"]);
					int duration = Convert.ToInt32(row["degreeDuration"]);
					string degreeName = row["degreeName"].ToString();

					degrees.Add(new Degree(degreeID, duration, degreeName));

				}

			}
			catch (Exception ex)
			{

			}
			return degrees;
		}

		public List<Course> GetCourses(int degreeID)
		{
			List<Course> courses = new List<Course>();
			try
			{
				MakeConnection();
				String sqlString = "select courseID, courseName, courseDuration from COURSE_TBL WHERE degreeID = " + degreeID + ";";
				SqlDataAdapter sqlCmd = new SqlDataAdapter(sqlString, SqlConn);
				DataSet ds = new DataSet();
				sqlCmd.Fill(ds);
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					int duration = Convert.ToInt32(row["courseDuration"]);
					int courseID = Convert.ToInt32(row["courseID"]);
					string courseName = row["courseName"].ToString();

					courses.Add(new Course(courseID, duration, courseName));

				}

			}
			catch (Exception ex)
			{

			}
			return courses;
		}

		public List<Student> GetStudents(int degreeID)
		{
			List<Student> students = new List<Student>();
			try
			{
				MakeConnection();
				string sqlString = "select studentID, studentName, studentSurname, studentEmail, studentDOB FROM STUDENT_TBL WHERE degreeID = " + degreeID + ";";
				SqlDataAdapter sqlCmd = new SqlDataAdapter(sqlString, SqlConn);
				DataSet ds = new DataSet();
				sqlCmd.Fill(ds);
				
				foreach (DataRow row in ds.Tables[0].Rows)
				{
					int studentID = Convert.ToInt32(row["studentID"]);
					string studentName = row["studentName"].ToString();
					string studentSurname = row["studentSurname"].ToString();
					string email = row["studentEmail"].ToString();
					DateTime date = Convert.ToDateTime(row["studentDOB"].ToString());
					 

					students.Add(new Student(studentID, studentName, studentSurname, email, date.ToString("yyyy/MM/dd")));
				}

			}
			catch (Exception ex)
			{

			}
			return students;
		}

		public Boolean updateStudents(string studentName, string studentSurname, string studentEmail, string studentDOB, int studentID)
		{
			bool update;
			try
			{
				MakeConnection();
				DateTime date = Convert.ToDateTime(studentDOB);

				//UPDATE STUDENT DETAILS
				string updateStudent = "UPDATE STUDENT_TBL SET studentName = '" + studentName + "', studentSurname = '" + studentSurname + "', studentEmail = '" + studentEmail + "', studentDOB = '" + date.ToString("yyyy/MM/dd") + "' WHERE studentID = " + studentID;
				SqlCommand sqlCmd4 = new SqlCommand(updateStudent, SqlConn);
				SqlConn.Open();
				// Execute the insert statement - it returns no result set and that is why ExecuteNonQuery is used
				sqlCmd4.ExecuteNonQuery();

				SqlConn.Close();
				update = true;
			}
			catch (System.Exception ex)
			{
				string error = ex.ToString();
				//MessageBox.Show(ex.Message);
				update = false;
			}
			return update;
		}

		public Boolean updateCourse(string courseName, int courseDuration, int courseID)
		{
			bool update;
			try
			{
				MakeConnection();
	

				//UPDATE COURSE DETAILS
				string updateCourse = "UPDATE COURSE_TBL SET courseName = '" + courseName + "', duration = " + courseDuration + " WHERE courseID = " + courseID;
				SqlCommand sqlCmd4 = new SqlCommand(updateCourse, SqlConn);
				SqlConn.Open();
				// Execute the insert statement - it returns no result set and that is why ExecuteNonQuery is used
				sqlCmd4.ExecuteNonQuery();

				SqlConn.Close();
				update = true;
			}
			catch (System.Exception ex)
			{
				string error = ex.ToString();
				//MessageBox.Show(ex.Message);
				update = false;
			}
			return update;
		}
		public Boolean deleteStudent(int studentID)
		{
			bool update;
			try
			{
				MakeConnection();

				//DELETE STUDENT DETAILS
				string deleteStudent = "DELETE FROM STUDENT_TBL WHERE studentID = " + studentID;
				SqlCommand sqlCmd4 = new SqlCommand(deleteStudent, SqlConn);
				SqlConn.Open();
				// Execute the insert statement - it returns no result set and that is why ExecuteNonQuery is used
				sqlCmd4.ExecuteNonQuery();

				SqlConn.Close();
				update = true;
			}
			catch (System.Exception ex)
			{
				string error = ex.ToString();
				//MessageBox.Show(ex.Message);
				update = false;
			}
			return update;
		}

		public Boolean deleteCourse(int courseID)
		{
			bool update;
			try
			{
				MakeConnection();

				//DELETE STUDENT DETAILS
				string deleteCourse = "DELETE FROM COURSE_TBL WHERE courseID = " + courseID;
				SqlCommand sqlCmd4 = new SqlCommand(deleteCourse, SqlConn);
				SqlConn.Open();
				// Execute the insert statement - it returns no result set and that is why ExecuteNonQuery is used
				sqlCmd4.ExecuteNonQuery();

				SqlConn.Close();
				update = true;
			}
			catch (System.Exception ex)
			{
				string error = ex.ToString();
				//MessageBox.Show(ex.Message);
				update = false;
			}
			return update;
		}
		public Boolean addStudent(string studentName, string studentSurname, string studentEmail, string studentDOB, int degreeID)
		{
			bool update;
			try
			{
				MakeConnection();
				DateTime date = Convert.ToDateTime(studentDOB);

				//ADD STUDENT DETAILS
				string addStudent = "INSERT INTO STUDENT_TBL VALUES('" + studentName +"', '" + studentSurname +"', '" + studentEmail +"', '"+ date.ToString("yyyy/MM/dd") +"', " + degreeID + ");";
				SqlCommand sqlCmd4 = new SqlCommand(addStudent, SqlConn);
				SqlConn.Open();
				// Execute the insert statement - it returns no result set and that is why ExecuteNonQuery is used
				sqlCmd4.ExecuteNonQuery();

				SqlConn.Close();
				update = true;
			}
			catch (System.Exception ex)
			{
				string error = ex.ToString();
				//MessageBox.Show(ex.Message);
				update = false;
			}
			return update;
		}

		public Boolean addCourse(string courseName, int courseDuration, int degreeID)
		{
			bool update;
			try
			{
				MakeConnection();

				//ADD STUDENT DETAILS
				string addCourse = "INSERT INTO COURSE_TBL VALUES('" + courseName + "', " + courseDuration + ", " + degreeID + ");";
				SqlCommand sqlCmd4 = new SqlCommand(addCourse, SqlConn);
				SqlConn.Open();
				// Execute the insert statement - it returns no result set and that is why ExecuteNonQuery is used
				sqlCmd4.ExecuteNonQuery();

				SqlConn.Close();
				update = true;
			}
			catch (System.Exception ex)
			{
				string error = ex.ToString();
				//MessageBox.Show(ex.Message);
				update = false;
			}
			return update;
		}

	}


}