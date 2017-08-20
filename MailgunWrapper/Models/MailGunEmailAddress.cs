namespace MailgunWrapper.Models
{
    public class MailgunEmailAddress : MailgunResponse
    {
        /// <summary>
        /// Gets or sets email addresss.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets a value indicating whether email address exists and is set.
        /// </summary>
        public bool HasAddress { get { return !string.IsNullOrEmpty(Address); } }

        public override string ToString()
        {
            return Address ?? base.ToString();
        }
    }
}
