using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CustomUserOperations.MVC.DbContext;
using CustomUserOperations.MVC.Entities;
using CustomUserOperations.MVC.Helpers;
using CustomUserOperations.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomUserOperations.MVC.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserOperationsDbContext _context;
        private readonly IEmailHelper _emailHelper;

        public UserController(UserOperationsDbContext context, IEmailHelper emailHelper)
        {
            _context = context;
            _emailHelper = emailHelper;
        }




        [HttpPost]
        public IActionResult Register(RegisterRequest request)
        {
            ApplicationUser user = new()
            {
                Name = request.Name,
                LastName = request.LastName,
                Age = request.Age,
                Email = request.Email,
                Password = request.Password
            };

            ConfirmEmailOperation confirmEmailOperation = new()
            {
                ConfirmEmailToken = System.Guid.NewGuid().ToString(),
                ValidityTime = DateTime.Now.AddHours(1)
            };

            user.ConfirmEmailOperation = confirmEmailOperation;
            _context.ApplicationUsers.Add(user);
            _context.SaveChanges();

            _emailHelper.SendConfirmAccountMail(user);
            return Ok("Üye Olundu. Mailinizi onaylayın.");
        }



        [HttpGet]
        public IActionResult ConfirmAccount([FromQuery] int userId, [FromQuery] string token)
        {
            var confirmEmailOperation = _context.ConfirmEmailOperations.AsNoTracking().FirstOrDefault(x=>x.ApplicationUserId == userId);

            if (confirmEmailOperation == null)
            {
                return BadRequest("İşlem Bulunamadı.");
            }

            if (confirmEmailOperation.ValidityTime < DateTime.Now)
            {
                return BadRequest("Hesap Onaylama Kodunun Süresi Dolmuş.");
            }

            if (!confirmEmailOperation.isValid)
            {
                return BadRequest("Kod Kullanılmış.");
            }

            if (confirmEmailOperation.ConfirmEmailToken != token)
            {
                return BadRequest("Geçersiz Kod.");
            }

            //TÜM ŞARTLARI SAĞLADI
            confirmEmailOperation.isValid = false;
            _context.ConfirmEmailOperations.Update(confirmEmailOperation);
            _context.SaveChanges();

            return Ok("Hesabınız Onaylandı!");
        }



        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            var user = _context.ApplicationUsers.Include(x=>x.ResetPasswordOperations).FirstOrDefault(x => x.Email == request.Email);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı.");
            }

            ResetPasswordOperation resetPasswordOperation = new()
            {
                ResetPasswordToken = System.Guid.NewGuid().ToString(),
                ValidityTime = DateTime.Now.AddHours(1)
            };

            user.ResetPasswordOperations.Add(resetPasswordOperation);
            _context.SaveChanges();

            _emailHelper.SendResetPasswordMail(user);
            return Ok("Şifre Sıfırlama Kodu Mail Adresinize Gönderildi.");
        }



        [HttpGet]
        public IActionResult ResetPassword([FromQuery] int userId, [FromQuery] string token, string password)
        {
            var resetPasswordOperation = _context.ResetPasswordOperations.FirstOrDefault(x=>x.ApplicationUserId == userId);
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.Id == userId);

            if (resetPasswordOperation == null || user == null)
            {
                return BadRequest("İşlem-Kullanıcı Bulunamadı.");
            }

            if (resetPasswordOperation.ValidityTime < DateTime.Now)
            {
                return BadRequest("Şifre Sıfırlama Kodunun Süresi Dolmuş.");
            }

            if (!resetPasswordOperation.isValid)
            {
                return BadRequest("Kod Kullanılmış.");
            }

            if (resetPasswordOperation.ResetPasswordToken != token)
            {
                return BadRequest("Geçersiz Kod.");
            }


            resetPasswordOperation.isValid = false;
            _context.ResetPasswordOperations.Update(resetPasswordOperation);

            user.Password = password;
            _context.ApplicationUsers.Update(user);

            _context.SaveChanges();

            return Ok("Şifreniz Başarıyla Değiştirildi.");

        }




        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.Email == request.Email);

            if (user == null)
            {
                return BadRequest("Kullanıcı Bulunamadı.");
            }

            if (user.Password != request.Password)
            {
                return BadRequest("Şifre Hatalı.");
            }

            return Ok("Login Olundu.");
        }
    }
}
