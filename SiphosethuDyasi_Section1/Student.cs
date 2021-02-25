using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiphosethuDyasi_Section1
{
	public class Student
	{
		int studentID;
		string studentName, studentSurname, studentEmail, studentDOB;

		public Student(int studentID, string studentName, string studentSurname, string studentEmail, string studentDOB)
		{
			this.StudentID = studentID;
			this.StudentName = studentName;
			this.StudentSurname = studentSurname;
			this.StudentEmail = studentEmail;
			this.StudentDOB = studentDOB;
		}

		public int StudentID { get => studentID; set => studentID = value; }
		public string StudentName { get => studentName; set => studentName = value; }
		public string StudentSurname { get => studentSurname; set => studentSurname = value; }
		public string StudentEmail { get => studentEmail; set => studentEmail = value; }
		public string StudentDOB { get => studentDOB; set => studentDOB = value; }
	}
}