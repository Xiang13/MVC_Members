using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Models.Services.Interfaces
{
	public interface IMemberRepository
	{
		bool IsExist(string account);
		void Create(RegisterDto dto);
	}
}
