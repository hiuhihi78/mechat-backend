namespace MeChat.Application.Abstractions.Emails;

public sealed class EmailTemplateService : IEmailTemplateService
{
    public string BuildSignUpConfirmationEmail(string confirmUrl)
    {
        if (string.IsNullOrWhiteSpace(confirmUrl))
            throw new ArgumentException("confirmUrl is required.", nameof(confirmUrl));

        return $@"
        <html>
        <body style='font-family: Arial, sans-serif; background-color:#f5f5f5; padding: 20px;'>
            <div style='max-width:600px; margin:auto; background:#ffffff; padding:20px; border-radius:10px;'>
                
                <h2 style='color:#2c3e50;'>Welcome to MeChat!</h2>

                <p>Thanks for signing up. Please confirm your email address to activate your account.</p>

                <div style='text-align:center; margin:30px 0;'>
                    <a href='{confirmUrl}'
                       style='background:#4CAF50; color:white; padding:12px 24px;
                              text-decoration:none; border-radius:6px; font-weight:bold; display:inline-block;'>
                        Confirm My Account
                    </a>
                </div>

                <p>If the button doesn&apos;t work, copy and paste this link into your browser:</p>
                <p style='word-break: break-all; color:#555;'>{confirmUrl}</p>

                <hr style='margin:20px 0;' />

                <p style='font-size:12px; color:#888;'>
                    If you didn&apos;t create this account, you can safely ignore this email.
                </p>

                <p style='font-size:12px; color:#888;'>
                    © {DateTime.UtcNow.Year} MeChat
                </p>
            </div>
        </body>
        </html>";
    }
}
