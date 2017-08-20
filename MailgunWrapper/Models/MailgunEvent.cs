namespace MailgunWrapper.Models
{
    public class MailgunEvent
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets event.
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// Gets or sets recepient.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Gets string representation of this instance.
        /// </summary>
        /// <returns>Event name.</returns>
        public override string ToString()
        {
            return Event ?? base.ToString();
        }
    }
}
