using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DTOs;
using WebApplication1.Models.Infrastructures;
using WebApplication1.Models.Services.Interfaces;

namespace WebApplication1.Models.Services
{
	public class MemberService
	{
		private readonly IMemberRepository repository;

		public MemberService(IMemberRepository repo)
		{
			this.repository = repo;
		}

		public (bool IsSuccess, string ErrorMessage) CreateNewMember(RegisterDto dto)
		{
			// todo 判斷各欄位是否正確
			// 判斷帳號是否已存在
			if (repository.IsExist(dto.Account))
			{
				return (false, "帳號已存在");
			}
			#region 建立一個會員記錄
			//     建立 ConfirmCode
			dto.ConfirmCode = Guid.NewGuid().ToString("N");

			repository.Create(dto);
			#endregion
			return (true, null);
		}
	
		public void ActiveRegister(int memberId, string confirmCode)
		{
			MemberDto dto = repository.Load(memberId);
			if (dto == null) return;

			if (string.Compare(dto.ConfirmCode, confirmCode) != 0) return;

			repository.ActiveRegister(memberId);
		}

		public (bool IsSuccess, string ErrorMessage) Login(string account, string password)
		{
			MemberDto member = repository.GetByAccount(account);
			if (member == null)
			{
				return (false, "帳密有誤");
			}
			if (member.IsConfirmed.HasValue == false || member.IsConfirmed.HasValue && member.IsConfirmed.Value == false)
			{
				return (false, "會員資格尚未確認");
			}
			string encryptedPwd = HashUtility.ToSHA256(password, RegisterDto.SALT);
			return (String.CompareOrdinal(member.EncryptedPassword, encryptedPwd) == 0)
				? (true, null)
				: (false, "帳密有誤");
		}
	}
}