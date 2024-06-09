﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dotel2.Models;
using System.Net.Mail;
using Dotel2.Service;


namespace Dotel2.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var input = Request.Form["EmailOrPhone"];
            if (IsValidEmail(input))
            {
                var emailExist = _context.Users.FirstOrDefault(s => s.Email.Equals(input));
                if (emailExist != null)
                {
                    TempData["ErrorMessage"] = "Email đã tồn tại.";
                    return Page();
                }
            }
            else if (IsValidPhone(input))
            {
                var phoneExist = _context.Users.FirstOrDefault(s => s.MainPhoneNumber.Equals(input));
                if (phoneExist != null)
                {
                    TempData["ErrorMessage"] = "Số điện thoại đã tồn tại.";
                    return Page();
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Định dạng email hoặc số điện thoại không hợp lệ.";
                return Page();
            }

            if (!Request.Form["Password"].Equals(Request.Form["RepeatPassword"]))
            {
                TempData["ErrorMessage"] = "Mật khẩu không khớp.";
                return Page();
            }

            var hashedPassword = GetHashedPassword(Request.Form["Password"]);

            // Add role if not exist
            var checkRole = _context.Roles.ToList();
            if (!checkRole.Any())
            {
                var addRole1 = new Role
                {
                    RoleName = "Admin",
                };
                _context.Roles.Add(addRole1);
                var addRole2 = new Role
                {
                    RoleName = "Guest",
                };
                _context.Roles.Add(addRole2);
                _context.SaveChanges();
            }

            var newUser = new User
            {
                Fullname = Request.Form["FullName"],
                Password = hashedPassword,
                RoleId = 2, // Admin = 1, Guest = 2
                Status = true,
            };

            string verificationCode = GenerateVerificationCode();
            if (IsValidEmail(input))
            {
                newUser.Email = input;
                newUser.CheckEmail = false; // Initially, email verification is false
                newUser.EmailVerificationCode = verificationCode;
                newUser.EmailVerificationCodeExpires = DateTime.Now.AddHours(1);
                SendEmailVerification(input, verificationCode);
            }
            else if (IsValidPhone(input))
            {
                newUser.MainPhoneNumber = input;
                newUser.CheckPhone = false; // Initially, phone verification is false
                newUser.PhoneVerificationCode = verificationCode;
                newUser.PhoneVerificationCodeExpires = DateTime.Now.AddHours(1);
                SendSMSVerification(input, verificationCode);
            }

            _context.Users.Add(newUser);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Đăng ký thành công. Vui lòng kiểm tra email hoặc điện thoại của bạn để xác nhận.";
            return Page();
        }

        private string GetHashedPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhone(string phone)
        {
            var phonePattern = @"^\d{10}$";
            return Regex.IsMatch(phone, phonePattern);
        }

        private string GenerateVerificationCode()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[4];
                rng.GetBytes(randomBytes);
                return BitConverter.ToString(randomBytes).Replace("-", "");
            }
        }

        private void SendEmailVerification(string email, string verificationCode)
        {
            try
            {
                var fromAddress = new MailAddress("nthanh174@outlook.com", "Thanh");
                var toAddress = new MailAddress(email);
                const string fromPassword = "Thanh1742001";
                const string subject = "Email Verification";
                string body = $"Your verification code is {verificationCode}";

                var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    Console.WriteLine("Email sent successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

        private void SendSMSVerification(string phoneNumber, string verificationCode)
        {
            SpeedSMSAPI api = new SpeedSMSAPI("J2UN35TT7vpudKKlUu_WT6DSawofkz1G");

            String[] phones = new String[] { "849xxxxxxx" };
            String str = "Nội dung sms";
            String response = api.sendSMS(phones, str, 2, "");
            //String response = api.sendMMS(phones, str, "https://", "device ID");
            Console.WriteLine(response);
            Console.ReadLine();
        }

    }
}
