using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using WebApplication1.Models.DTOs;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models.ViewModels
{
	public class RegisterVM
	{
		public int Id { get; set; }

		[Display(Name = "帳號")]
		[Required]
		[StringLength(30)]
		public string Account { get; set; }

		[Display(Name = "密碼")]
		[Required]
		[StringLength(50)]
		[DataType(DataType.Password)] // 明碼
		public string Password { get; set; }

		[Display(Name = "確認密碼")]
		[Required]
		[StringLength(50)]
		[Compare(nameof(Password))]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		[Display(Name = "電子信箱")]
		[Required]
		[StringLength(256)]
		[EmailAddress]
		public string Email { get; set; }

		[Display(Name = "姓名")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "手機號碼")]
		[StringLength(10)]
		public string Mobile { get; set; }
	}
}

public static class RegisterVMExts
{
	public static RegisterDto ToRequestDto(this RegisterVM source)
	{
		return new RegisterDto
		{
			Account = source.Account,
			Password = source.Password,
			Email = source.Email,
			Name = source.Name,
			Mobile = source.Mobile
		};
	}
}