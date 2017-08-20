namespace MailgunWrapper.Models
{
    public class MailgunPaging
    {
        /// <summary>
        /// Gets or sets next page url.
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// Gets or sets previous url.
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// Gets first page url.
        /// </summary>
        public string First { get; set; }

        /// <summary>
        /// Gets last page url.
        /// </summary>
        public string Last { get; set; }
    }
}
