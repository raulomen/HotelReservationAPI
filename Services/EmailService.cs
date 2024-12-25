using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HotelReservationAPI.Services
{
    public class EmailService
    {
        private readonly string _smtpServer = "smtp.example.com";  // SMTP server
        private readonly int _smtpPort = 587;  // Port for SMTP
        private readonly string _fromEmail = "your-email@example.com";  // From address
        private readonly string _fromPassword = "your-password";  // Your email password

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var message = new MailMessage(_fromEmail, toEmail, subject, body);
                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.EnableSsl = true; // Asegúrate de que SSL está habilitado
                    smtpClient.Credentials = new NetworkCredential(_fromEmail, _fromPassword);
                    await smtpClient.SendMailAsync(message);
                }
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Error: {smtpEx.Message}");
                throw; // Lanza el error para que puedas manejarlo adecuadamente
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }
    }
}
