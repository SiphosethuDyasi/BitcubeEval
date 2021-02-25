using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiphosethuDyasi_Section1
{
	public class Degree
	{
		int degreeID, degreeDuration;
		string degreeName;

		public Degree(int degreeID, int degreeDuration, string degreeName)
		{
			this.DegreeID = degreeID;
			this.DegreeDuration = degreeDuration;
			this.DegreeName = degreeName;
		}

		public int DegreeID { get => degreeID; set => degreeID = value; }
		public int DegreeDuration { get => degreeDuration; set => degreeDuration = value; }
		public string DegreeName { get => degreeName; set => degreeName = value; }
	}
}