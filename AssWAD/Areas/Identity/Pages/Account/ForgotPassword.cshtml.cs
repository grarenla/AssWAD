using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AssWAD.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AssWAD.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Member> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<Member> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                
                var user = await _userManager.FindByEmailAsync(Input.Email);
                
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);
                SendMail(Input.Email, "Test", $"Please reset your password by {HtmlEncoder.Default.Encode(callbackUrl)} clicking here");
//                await _emailSender.SendEmailAsync(
//                    Input.Email,
//                    "Reset Password",
//                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }

        private void SendMail(string receiveMail, string receiveName, string mailContent)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            MailAddress from = new MailAddress("s2rainlove2s@gmail.com", "Test");
            MailAddress to = new MailAddress(receiveMail, receiveName);
            MailMessage message = new MailMessage(from, to);
            message.Body = mailContent;
            message.Subject = "Reset Password";
            NetworkCredential myCreds = new NetworkCredential("s2rainlove2s@gmail.com", "grckfuicpiikuult");
            client.Credentials = myCreds;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception is:" + ex.ToString());
            }
            Console.WriteLine("Goodbye.");
        }
    }
}
