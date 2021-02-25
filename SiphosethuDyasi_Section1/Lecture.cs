using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiphosethuDyasi_Section1
{
	public class Lecture
	{
		int lectureID;
		string firstName, surname, dob, email, password;

		public Lecture(int lectureID, string firstName, string surname, string dob, string email, string password)
		{
			this.LectureID = lectureID;
			this.FirstName = firstName;
			this.Surname = surname;
			this.Dob = dob;
			this.Email = email;
			this.Password = password;
		}

		public int LectureID { get => lectureID; set => lectureID = value; }
		public string FirstName { get => firstName; set => firstName = value; }
		public string Surname { get => surname; set => surname = value; }
		public string Dob { get => dob; set => dob = value; }
		public string Email { get => email; set => email = value; }
		public string Password { get => password; set => password = value; }
	}
}