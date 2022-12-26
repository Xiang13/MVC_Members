using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Models.ViewModels
{
	public class LoginVM
	{
		[Display(Name = "帳號: ")]
		[Required]
		[StringLength(30)]
		public string Account { get; set; }

		[Display(Name = "密碼: ")]
		[Required]
		[StringLength(30)]
		[DataType(DataType.Password)]
		public string Password{ get; set; }
	}
}