﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.Infrastructures;

namespace WebApplication1.Models.DTOs
{
	public class MemberDto
	{
		public string Account { get; set; }

		public string Password { get; set; } // 密碼，明碼

		public string EncryptedPassword
		{
			get
			{
				string salt = "!@#$$DGTEGYT";
				string result = HashUtility.ToSHA256(this.Password, salt);
				return result;
			}
		} // 密碼，暗碼

		public string Email { get; set; }

		public string Name { get; set; }

		public string Mobile { get; set; }

		public string IsConfirmed { get; set; }

		public string ConfirmCode { get; set; }
	}
}