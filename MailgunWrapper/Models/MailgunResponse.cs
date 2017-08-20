namespace MailgunWrapper.Models
{
    public abstract class MailgunResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether request was executed successfully. 
        /// <para>Vlaue is taken from HTTP response status code.</para>
        /// </summary>
        public bool Success{ get; set; }
    }
}