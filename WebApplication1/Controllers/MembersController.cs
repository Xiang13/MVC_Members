using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Models.Infrastructures.Repositories;
using WebApplication1.Models.Services;
using WebApplication1.Models.Services.Interfaces;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
	public class MembersController : Controller
    {
		private IMemberRepository repository = new MemberRepository();
		// GET: Members
		public ActionResult Register()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Register(RegisterVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var service = new MemberService(repository);
			(bool IsSucess, string ErrorMessage) response = service.CreateNewMember(model.ToRequestDto());
			if (response.IsSucess)
			{
				//建檔成功，轉到confirm page
				return View("RegisterConfirm");
			}
			else
			{
				ModelState.AddModelError(string.Empty, response.ErrorMessage);
				return View(model);
			}
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginVM model)
		{
			var service = new MemberService(repository);
			(bool IsSuccess, string ErrorMessage) response =
				service.Login(model.Account, model.Password);
			if (response.IsSuccess)
			{
				// 記住登入成功的會員
				var rememberMe = false;
				string returnUrl = ProcessLogin(model.Account, rememberMe, out HttpCookie cookie);
				Response.Cookies.Add(cookie);
				return Redirect(returnUrl);
			}
			ModelState.AddModelError(string.Empty, response.ErrorMessage);
			return this.View(model);
		}

		//public ActionResult Logout() { }

		private string ProcessLogin(string account, bool rememberMe, out HttpCookie cookie)
		{
			var member = repository.GetByAccount(account);
			string roles = String.Empty; // 在本範例, 沒有用到角色權限,所以存入空白
										 // 建立一張認證票
			FormsAuthenticationTicket ticket =
				new FormsAuthenticationTicket(
					1,          // 版本別, 沒特別用處
					account,
					DateTime.Now,   // 發行日
					DateTime.Now.AddDays(2), // 到期日
					rememberMe,     // 是否續存
					roles,          // userdata
					"/" // cookie位置
				);
			// 將它加密
			string value = FormsAuthentication.Encrypt(ticket);
			// 存入cookie
			cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);
			// 取得return url
			string url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處
			return url;
		}

		public ActionResult ActiveRegister(int memberId, string confirmCode)
		{
			IMemberRepository repo = new MemberRepository();
			var service = new MemberService(repo);
			service.ActiveRegister(memberId, confirmCode);

			return View();
		}
	}
}