namespace MailgunWrapper.Models
{
    public class MailgunSendEmailResponse : MailgunResponse
    {
        /// <summary>
        /// Gets or sets sent message id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets respnse message.
        /// </summary>
        public string Message { get; set; }
    }
}
