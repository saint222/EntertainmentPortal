using System;
using System.Collections.Generic;
using System.Text;

namespace EP.WordsMaker.Data.Models
{
	public class GameDb
	{
		/// <summary>
		/// ID property
		/// </summary>
		/// <value> Represents unique id of game </value>
		public string Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime Duration { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string KeyWord { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public DateTime EndTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string PlayerId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public PlayerDb Player { get; set; }



	}
}
