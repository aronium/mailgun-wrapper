using System.Collections.Generic;

namespace MailgunWrapper.Models
{
    public class MailgunPagedResponse<T> : MailgunResponse
    {
        /// <summary>
        /// Gets list of <typeparam>T</typeparam>.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Gets or sets response paging.
        /// </summary>
        public MailgunPaging Paging { get; set; }
    }
}
