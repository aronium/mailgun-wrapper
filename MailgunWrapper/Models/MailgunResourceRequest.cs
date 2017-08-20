using System.Collections.Generic;
using RestSharp;
using System.Collections.Specialized;

namespace MailgunWrapper.Models
{
    public class MailgunResourceRequest
    {
        /// <summary>
        /// Gets new builder instance.
        /// </summary>
        public static MailgunResourceRequestBuilder Builder
        {
            get
            {
                return new MailgunResourceRequestBuilder();
            }
        }

        /// <summary>
        /// Gets or sets resource.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets request method.
        /// </summary>
        public Method Method { get; set; }

        /// <summary>
        /// Gets or sets list of parameters.
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets list of attachments. Files can be added "inline" or as an "attachment".
        /// </summary>
        public List<MailgunAttachment> Attachments { get; set; } = new List<MailgunAttachment>();
    }
}
