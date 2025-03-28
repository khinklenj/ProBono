using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;

namespace ProBono.Pages
{
    public class CaptureModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CaptureModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public FormData Form { get; set; }

        public void OnGet()
        {
            // Get logic (if any)
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrEmpty(Form.Email))
            {
                // Send email when form is submitted
                SendEmail(Form);
                return RedirectToPage("/ThankYou"); // Redirect to a thank-you page or confirmation
            }
            return Page(); // Return the page if the model is invalid
        }

        private void SendEmail(FormData formData)
        {
            if (!string.IsNullOrEmpty(Form.Email))
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
            var fromEmail = new MailAddress("khinklenj@gmail.com", "Keith Hinkle");
            var toEmail = new MailAddress("keith@creativesimplex.com");
            var subject = "Form Submission";
            var body = $"Name: {formData.Name}\nPhone: {formData.Phone}\nEmail: {formData.Email}\n" +
                        $"Selection: {formData.Selection}\nDate: {formData.FormDate}\nAgree: {formData.documentSelectionGroup}\n";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("khinklenj@gmail.com", "xxxxx"),
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
            var fromEmail = new MailAddress("khinklenj@gmail.com", "Keith Hinkle");
            var toEmail = new MailAddress("keith@creativesimplex.com");
            var subject = "Form Submission";
            var body = $"Name: {formData.Name}\nPhone: {formData.Phone}\nEmail: {formData.Email}\n" +
                        $"Selection: {formData.Selection}\nDate: {formData.FormDate}\nAgree: {formData.documentSelectionGroup}\n";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("khinklenj@gmail.com", "xxxxx"),
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
            var fromEmail = new MailAddress("khinklenj@gmail.com", "Keith Hinkle");
            var toEmail = new MailAddress("keith@creativesimplex.com");
            var subject = "Form Submission";
            var body = $"Name: {formData.Name}\nPhone: {formData.Phone}\nEmail: {formData.Email}\n" +
                        $"Selection: {formData.Selection}\nDate: {formData.FormDate}\nAgree: {formData.documentSelectionGroup}\n";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("khinklenj@gmail.com", "xxxxx"),
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
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Selection { get; set; }
        public string FormDate { get; set; }
        public string documentSelectionGroup { get; set; }
    }


}
