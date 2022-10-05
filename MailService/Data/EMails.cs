namespace MailService.Data
{
    public class EMails
    {
        private static readonly Models.EMail[] EmailsData = new[] // mock email data
        {
            new Models.EMail() 
            { 
                Subject = "MailService test", 
                To = "sam_juan@yahoo.com", 
                Body = "This is a test email. Do not reply!", 
                IsSent = true 
            },
            new Models.EMail() 
            { 
                Subject = "Re: MailService test", 
                To = "nowan.izeer@mail.ru", 
                Body = "Did you receive my mail?! <b>PLEASE REPLY!!!</b>", 
                IsSent = false 
            },
            new Models.EMail() 
            { 
                Subject = "Fwd: MailService test", 
                To = "somebodyelse2015@mail.tk", 
                Body = "NOOOOOOOOOO!!!!!!!!!!", 
                IsSent = false
            },
        };

        /// <summary>
        /// get emails
        /// </summary>
        /// <param name="isSent"></param>
        /// <returns></returns>
        public static IEnumerable<Models.EMail>? GetEmails(bool isSent)
        {
            return EmailsData.AsEnumerable().Where(m => m.IsSent == isSent);
        }
    }
}
