using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models;
using MyPhamTrueLife.DAL.Models.Utils;
using MyPhamTrueLife.Web.Base;
using MyPhamTrueLife.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyPhamTrueLife.Web.Controllers.Client
{
    public class EmailInfo
    {
        public List<string> ToEmail { get; set; }

        public string Subj { get; set; }
        public string Message { get; set; }
        public MemoryStream dataAttach { get; set; }
        public string nameAttach { get; set; }
        public bool isAttach { get; set; }

        public string UserName { get; set; }
        public string Passwork { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _user;
        private readonly IConfiguration _config;
        private readonly IInfoUserService _infoUserService;
        public UserController(IUserService user, IConfiguration config, IInfoUserService infoUserService)
        {
            _infoUserService = infoUserService;
            _user = user;
            _config = config;
        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ResponseResult<LoginResponse>> Login([FromBody] Login user)
        {
            try
            {
                if (user == null)
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Tài khoản và mật khẩu đăng nhập không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Tài khoản đăng nhập không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.PassWord))
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Mật khẩu đăng nhập không được trống.", null);
                }
                var result = await _user.LoginAsync(user);
                if (result == null)
                {
                    return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, "Tài khoản và mật khẩu không chính xác.", null);
                }
                result.Token = CreateToken(result);
                return new ResponseResult<LoginResponse>(RetCodeEnum.Ok, "Đăng nhập thành công.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<LoginResponse>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<ResponseResult<InfoUser>> RegisterUser([FromBody] RegisterUser user)
        {
            try
            {
                if (user == null)
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Thông tin đăng ký không được để trống.", null);
                }
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Thông tin tài khoản không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.Email))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Thông tin email không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.Phone))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Thông tin sô điện thoại không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.Password))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Mật khẩu không được trống.", null);
                }
                if (string.IsNullOrEmpty(user.UserName))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Xác nhận mật khẩu không được trống.", null);
                }
                if (!IsValidEmail(user.Email))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Email sai định dạng.", null);
                }
                if (!IsPhoneNumber(user.Phone))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Số điện thoại sai định dạng.", null);
                }
                var check = await _user.CheckUserNameAsync(user.UserName);
                if (check == false)
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Tên tài khoản đã tồn tại trong hệ thống vui lòng nhập lại tài khoản mới.", null);
                }
                if (!user.Password.Equals(user.ConfirmPassword))
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Mật khẩu và xác nhận mật khẩu không trùng khớp.", null);
                }
                var result = await _user.ResgisterUserAsync(user);
                if (result == null)
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Đăng ký không thành công.", null);
                }
                var mail = new EmailInfo();
                mail.UserName = result.UserName;
                mail.Passwork = user.Password;
                mail.ToEmail = new List<string>();
                mail.ToEmail.Add(result.Email);
                var sendMail = SendMailThankYou(mail);
                if (sendMail == false)
                {
                    return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, "Đăng ký thành công, gửi mail thất bại", null);
                }
                return new ResponseResult<InfoUser>(RetCodeEnum.Ok, "Đăng ký thành công.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        [HttpPost]
        [Route("ForgotPassWordAsync")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> ForgotPassWordAsync(string userName)
        {
            try
            {

                if (string.IsNullOrEmpty(userName))
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Tài khoản đăng nhập không được trống.", null);
                }
                var result = await _user.CheckUserNameAsync(userName);
                if (result == true)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Tài khoản của bạn không tồn tại trong hệ thống.", null);
                }
                var res = await _user.ForgotPassWordAsync(userName);
                if (res == 1)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Tài khoản của bạn không tồn tại trong hệ thống.", null);
                }
                if (res == 2)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Tài khoản của bạn đã bị xóa khỏi hệ thống.", null);
                }
                if (res == 0)
                {
                    var user = await _user.GetByUserNameAsync(userName);
                    var mail = new EmailInfo();
                    mail.UserName = userName;
                    mail.Passwork = "123";
                    mail.ToEmail = new List<string>();
                    mail.ToEmail.Add(user.Email);
                    var sendMail = SendMailForgot(mail);
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Thay đổi tài khoản thành công, quý khách vui lòng kiểm tra mail để lấy thông tin đănng nhập.", "");
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, "");
            }
        }

        [HttpPost]
        [Route("UpdateInfoUserAsync")]
        [AllowAnonymous]
        public async Task<ResponseResult<string>> UpdateInfoUserAsync([FromBody] InfoUser value)
        {
            try
            {
                var result = await _infoUserService.UpdateInfoUserAsync(value);
                if (result == false)
                {
                    return new ResponseResult<string>(RetCodeEnum.ApiError, "Cập nhật thất bại.", result.ToString());
                }
                return new ResponseResult<string>(RetCodeEnum.Ok, "Cập nhật thành công.", result.ToString());
            }
            catch (Exception ex)
            {
                return new ResponseResult<string>(RetCodeEnum.ApiError, ex.Message, "");
            }
        }

        [HttpPost]
        [Route("DetailInfoUserAsync")]
        [AllowAnonymous]
        public async Task<ResponseResult<InfoUser>> DetailInfoUserAsync(int userId)
        {
            try
            {
                var result = await _infoUserService.DetailInfoUserAsync(userId);
                return new ResponseResult<InfoUser>(RetCodeEnum.Ok, "Chi tiết người dùng.", result);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InfoUser>(RetCodeEnum.ApiError, ex.Message, null);
            }
        }

        private bool SendMailThankYou(EmailInfo EmailInfo)
        {
            try
            {
                //SENT MAIL
                bool flag = true;
                if (flag)
                {

                    string smtpAddress = "smtp.gmail.com";

                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "soiconguoiyeu123@gmail.com";
                    string password = "rlzr pyxk gllz dhlw";
                    string displayName = "TrueLife";


                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom, displayName);
                        if (EmailInfo.ToEmail != null)
                        {
                            foreach (var item in EmailInfo.ToEmail)
                            {
                                if (item != null)
                                    mail.To.Add(item);
                            }
                        }
                        mail.Subject = "Thông báo cám ơn quý khách đã đăng ký tài khoản tại TrueLife ";

                        var body = string.Empty;
                        body = "(*) Quý khách vui lòng không phản hồi email này. Đây là email được gửi bằng hệ thống tự động.\n";
                        body += "Kính gửi Quý khách hàng,";
                        body += "Đầu tiên xin gửi lời cảm ơn Anh/Chị đã tin tưởng và sử dụng shop TrueLife của chúng tôi.\n";
                        body += "\t - Username: " + EmailInfo.UserName + "\n\t - Password: " + EmailInfo.Passwork + "";
                        mail.Body = body;
                        //media type that is respective of the data attach file
                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            if (EmailInfo.ToEmail.FirstOrDefault() != null)
                            {
                                smtp.Send(mail);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool SendMailForgot(EmailInfo EmailInfo)
        {
            try
            {
                //SENT MAIL
                bool flag = true;
                if (flag)
                {

                    string smtpAddress = "smtp.gmail.com";

                    int portNumber = 587;
                    bool enableSSL = true;

                    string emailFrom = "soiconguoiyeu123@gmail.com";
                    string password = "rlzr pyxk gllz dhlw";
                    string displayName = "TrueLife";


                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(emailFrom, displayName);
                        if (EmailInfo.ToEmail != null)
                        {
                            foreach (var item in EmailInfo.ToEmail)
                            {
                                if (item != null)
                                    mail.To.Add(item);
                            }
                        }
                        mail.Subject = "Cửa hàng TrueLife gửi thông tin tài khoản cho quý khách";

                        var body = string.Empty;
                        body = "(*) Quý khách vui lòng không phản hồi email này. Đây là email được gửi bằng hệ thống tự động.\n";
                        body += "Kính gửi Quý khách hàng,";
                        body += "Đầu tiên xin gửi lời cảm ơn Anh/Chị đã tin tưởng và sử dụng shop TrueLife của chúng tôi.\n";
                        body += "\t - Username: " + EmailInfo.UserName + "\n\t - Password: " + EmailInfo.Passwork + "";
                        mail.Body = body;
                        //media type that is respective of the data attach file
                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                            smtp.EnableSsl = enableSSL;
                            if (EmailInfo.ToEmail.FirstOrDefault() != null)
                            {
                                smtp.Send(mail);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #region "Private Methods"
        private string CreateToken(LoginResponse user)
        {
            if (user == null) return string.Empty;

            var tokenString = CreateTokenString(
                string.IsNullOrEmpty(user.Email) ? user.Email : user.Email,
                user.FullName ?? user.Email,
                user.UserId.ToString(),
                user.Email,
                user.Phone ?? user.UserId.ToString(),
                _config["Tokens:Key"],
                _config["Tokens:Issuer"]);
            return tokenString;
        }
        #endregion "Private Methods"
    }
}
