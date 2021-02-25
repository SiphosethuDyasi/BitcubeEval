using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiphosethuDyasi_Section1
{
	public class Course
	{
		int courseID, duration;
		string courseName;

		public Course(int courseID, int duration, string courseName)
		{
			this.CourseID = courseID;
			this.Duration = duration;
			this.CourseName = courseName;
		}

		public int CourseID { get => courseID; set => courseID = value; }
		public int Duration { get => duration; set => duration = value; }
		public string CourseName { get => courseName; set => courseName = value; }
	}	
}