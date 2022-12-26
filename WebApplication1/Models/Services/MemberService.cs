using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models.DTOs;
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
	}
}