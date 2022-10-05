namespace MailService.Services
{
    public class MailSender : BackgroundTask
    {
        private readonly ILogger<MailSender> _logger;
        public MailSender(ILogger<MailSender> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} MailService is starting");

            cancellationToken.Register(() => _logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} MailService is stopping"));

            var interval = 60000; // 1 minute
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} MailService is running");

                try
                {
                    var queue = Data.EMails.GetEmails(false); // get only mails not yet sent
                    if (queue != null)
                    {
                        foreach (var mail in queue)
                        {
                            try
                            {
                                _logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} Sending '{mail.Subject}' to '{mail.To}'...");
                                Mailer.Send(mail.To, mail.Subject, mail.Body); // send mail

                                // ToDo: mark as sent and save 
                            }
                            catch (Exception err)
                            {
                                _logger.LogError($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} MailService error: {err.Message} {Environment.NewLine} {err.StackTrace}");
                            }
                        }
                    }
                }
                catch (Exception err)
                {
                    _logger.LogError($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} MailService error: {err.Message} {Environment.NewLine} {err.StackTrace}");
                }
                await Task.Delay(interval, cancellationToken); // loop back
            }
            _logger.LogInformation($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} MailService is stopping");
        }
    }
}
