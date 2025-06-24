using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;

namespace ProBono.Pages
{
    public class CaptureModel : PageModel
    {
        private string customerAttentionEmail = string.Empty;
        private string signedAttentionEmail = string.Empty;
        private string notsignedAttentionEmail = string.Empty;

        public CaptureModel(IConfiguration configuration)
        {
            customerAttentionEmail = configuration["ContactSettings:CustomerAttentionEmailAddress"];
            signedAttentionEmail = configuration["ContactSettings:SignedAttentionEmailAddress"];
            notsignedAttentionEmail = configuration["ContactSettings:NotsignedAttentionEmailAddress"];
        }

        [BindProperty]
        public FormData Form { get; set; }

        public void OnGet()
        {
            // Get logic (if any)
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Form.Email1))
            {
                // Send email when form is submitted
                SendEmail(Form);
                return RedirectToPage("/ThankYou"); // Redirect to a thank-you page or confirmation
            }
            return Page(); // Return the page if the model is invalid
        }

        private void SendEmail(FormData formData)
        {
            if (!string.IsNullOrEmpty(Form.Email1))
            {
                CustomerEmail(formData);
            }

            switch (Form.documentSelectionGroup)
            {
                case "signed":
                    SignedEmail(formData);
                    break;
                case "notsigned":
                    NotSigned(formData);
                    break;
            }
        }

        private void CustomerEmail(FormData formData)
        {
            var fromEmail = new MailAddress(customerAttentionEmail);
            var toEmail = new MailAddress(formData.Email1);
            var subject = "Form Submission";
            var body = $"Name: {formData.Name}\nPhone: {formData.Phone1}\nEmail: {formData.Email1}\n" +
                        $"Selection: {formData.Selection}\nDate: {formData.FormDate}\nAgree: {formData.documentSelectionGroup}\n";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(customerAttentionEmail, "xxxxx"),
                EnableSsl = true,
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }

        private void SignedEmail(FormData formData)
        {
            var fromEmail = new MailAddress(signedAttentionEmail);
            var toEmail = new MailAddress(formData.Email1);
            var subject = "Form Submission";
            var body = $"Name: {formData.Name}\nPhone: {formData.Phone1}\nEmail: {formData.Email1}\n" +
                        $"Selection: {formData.Selection}\nDate: {formData.FormDate}\nAgree: {formData.documentSelectionGroup}\n";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(signedAttentionEmail, "xxxxx"),
                EnableSsl = true,
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }

        private void NotSigned(FormData formData)
        {
            var fromEmail = new MailAddress(notsignedAttentionEmail);
            var toEmail = new MailAddress(formData.Email1);
            var subject = "Form Submission";
            var body = $"Name: {formData.Name}\nPhone: {formData.Phone1}\nEmail: {formData.Email1}\n" +
                        $"Selection: {formData.Selection}\nDate: {formData.FormDate}\nAgree: {formData.documentSelectionGroup}\n";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(notsignedAttentionEmail, "xxxxx"),
                EnableSsl = true,
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }

    }

    // Form Data Model
    public class FormData
    {
        public string Name { get; set; }
        public string Phone1 { get; set; }
        public string Email1 { get; set; }
        public string Selection { get; set; }
        public string FormDate { get; set; }
        public string documentSelectionGroup { get; set; }
    }


}
