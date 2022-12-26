using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebApplication1.Models.DTOs;
using WebApplication1.Models.EFModels;
using WebApplication1.Models.Services.Interfaces;

namespace WebApplication1.Models.Infrastructures.Repositories
{
	public class MemberRepository : IMemberRepository
	{
		private AppDbContext db = new AppDbContext();
		public void Create(RegisterDto dto)
		{
			Member member = new Member
			{
				Account = dto.Account,
				EncryptedPassword = dto.EncryptedPassword,
				Email = dto.Email,
				Name = dto.Name,
				Mobile = dto.Mobile,
				IsConfirmed = false,
				ConfirmCode = dto.ConfirmCode
			};

			db.Members.Add(member);
			db.SaveChanges();
		}

		public bool IsExist(string account)
		{
			var entity = db.Members.SingleOrDefault(x => x.Account == account);
			return (entity != null);
		}
	}
}