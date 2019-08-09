using KM.Common;
using SC.Business.Entity.Models;
using SC.Business.Interface;
using SC.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.Mvc;

namespace SC.WebController
{
    public class HomeController : Controller
    {

        //[AntiForgeryAuthorizationd]
        public ActionResult Index()
        {
            ViewBag.Notifications = new List<Notification>() {
                new Notification {
                    Title="111",
                    InfoLevel="warning",
                    Content="12werwerwerweqerqw31",

                },
                          new Notification {
                    Title="2222",
                    InfoLevel="warning",
                      Content="23qereqewr12",
                },
                                    new Notification {
                    Title="333",
                    InfoLevel="warning",
                      Content="erqw",
                },

            };


            var userInfo = new User();
            userInfo.UserName = "用户1";
            userInfo.ImgUrl = "user1.png";
            //userInfo.EmpNo = SecurityHelper.CurrentPrincipal.EmpNo;
            //userInfo.UserId = SecurityHelper.CurrentPrincipal.UserId;
            //SecurityHelper.CurrentPrincipal.CurrentLoginSys = "LC";
            //SetCustomPrincipal(SecurityHelper.CurrentPrincipal);
            ViewBag.User = userInfo;



            return View("index");
        }
        public ActionResult login(string name, string pwd, string code, string orgId)
        {
            ViewBag.Msg = "";
            IUserService userService = IOCContainer.Instance.Resolve<IUserService>();

            //// 临时去掉验证码检验
            //code = "1234";
            //Session["CheckCode"] = code.ToUpper();

            //if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(pwd) && !string.IsNullOrEmpty(orgId) && !string.IsNullOrEmpty(code) && userCheck(name, pwd, orgId, ref user)
            //    && Session["CheckCode"] != null && code.ToUpper() == Session["CheckCode"].ToString().ToUpper())
            //{
            //    IAuthenticationService authenticationService = IOCContainer.Instance.Resolve<IAuthenticationService>();
            //    ClientUserData clientUserData = new ClientUserData()
            //    {
            //        UserId = user.UserId,
            //        LoginName = user.LogonName,
            //        EmpNo = user.EmpNo,
            //        EmpName = user.EmpName,
            //        EmpGroup = user.EmpGroup,
            //        JobTitle = user.JobTitle,
            //        JobType = user.JobType,
            //        OrgId = user.OrgId,
            //        RoleId = user.RoleId,
            //        RoleType = user.RoleType,
            //        SysType = user.SysType,
            //        LTCRoleType = user.LTCRoleType,
            //        DCRoleType = user.DCRoleType
            //    };

            //    authenticationService.SignIn(clientUserData, true);

            //    if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            //    {
            //        return Redirect(HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"]));
            //    }

            //        return Redirect("/Home/Index");


            //}
            //else
            //{
            //    if (!string.IsNullOrEmpty(name))
            //    {
            //        ViewBag.Msg = "请输入正确信息";
            //    }
            //}
            //return View();
            var user = userService.GetUser(name, pwd).Data;
           if (name==null)
            {
                ViewBag.Msg = "";
            }
            else if(user==null)
            {
                ViewBag.Msg = "用户名或密码不正确";
            }
            else
            {
                ClientUserData clientUserData = new ClientUserData()
                {
                    UserID = user.UserID,
                    UserName = user.UserName,
                    Pwd = user.Pwd,
                    PwdExpDate = user.PwdExpDate,
                    Sex = user.Sex,
                    Age = user.Age,
                    ImgUrl = user.ImgUrl,
                    Email = user.Email,
                    EmpNo = user.EmpNo,
                    RoleId = user.RoleId,
                    RoleType = user.RoleType,
                };
                return Redirect("/Home/Index");
            }
            return View();
        }

        public bool userCheck(string name, string pwd, string orgId, ref User user)
        {

            return true;
        }

        private void SetCustomPrincipal(ICustomPrincipal principal)
        {
            IAuthenticationService authenticationService = IOCContainer.Instance.Resolve<IAuthenticationService>();
            ClientUserData clientUserData = new ClientUserData()
            {
                UserID = principal.UserID,
                UserName = principal.UserName,
                Pwd = principal.Pwd,
                PwdExpDate = principal.PwdExpDate,
                Sex = principal.Sex,
                Age = principal.Age,
                ImgUrl = principal.ImgUrl,
                Email = principal.Email,
                EmpNo = principal.EmpNo,
                RoleId = principal.RoleId,
                RoleType = principal.RoleType,
            };
            authenticationService.SignIn(clientUserData, true);
        }
    }
}
