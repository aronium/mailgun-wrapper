using MailgunWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MailgunWrapper
{
    public class MailgunResourceRequestBuilder
    {
        #region - Fields -
        private MailgunResourceRequest request;
        #endregion

        #region - Constructor -

        /// <summary>
        /// Initializes new instance of MailgunResourceRequestBuilder class.
        /// </summary>
        public MailgunResourceRequestBuilder()
        {
            request = new MailgunResourceRequest();
        }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Specifies resource used in request.
        /// </summary>
        /// <param name="resource">Resource to use.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Resource(string resource)
        {
            request.Resource = resource;

            return this;
        }

        /// <summary>
        /// Specifies request method type. Default is GET.
        /// </summary>
        /// <param name="method">Request method to use.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Method(RestSharp.Method method)
        {
            request.Method = method;

            return this;
        }

        /// <summary>
        /// Adds parameter with specified name and value.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder WithParameter(string name, object value)
        {
            request.Parameters.Add(name, value);

            return this;
        }

        /// <summary>
        /// Adds file with specified attachment type and file name.
        /// </summary>
        /// <param name="parameterName">Parameter to use. Can be "inline" or "attachment".</param>
        /// <param name="fileName">File name to attach.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder AddFile(string parameterName, string fileName)
        {
            if (string.IsNullOrEmpty(parameterName))
                throw new ArgumentNullException("parameterName");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            request.Attachments.Add(new MailgunAttachment(parameterName, fileName));

            return this;
        }

        /// <summary>
        /// Specifies events resource.
        /// </summary>
        /// <param name="eventType">Event types to query.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForEvents(MailgunEventType eventType)
        {
            request.Resource = "events";

            if (eventType != MailgunEventType.None)
            {
                var stringEnumValue = Enum.GetValues(eventType.GetType())
                .Cast<MailgunEventType>()
                .Where(x => x != MailgunEventType.None)
                .Cast<Enum>()
                .Where(eventType.HasFlag)
                .Select(x => x.ToString().ToLower())
                .ToArray();

                var filter = string.Join(" OR ", stringEnumValue);

                return WithParameter("event", filter);
            }

            return this;
        }

        /// <summary>
        /// Specifies messages resource.
        /// </summary>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForMessages()
        {
            request.Resource = "messages";

            return this;
        }

        /// <summary>
        /// Specifies messages resource and sets request method to POST.
        /// </summary>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForSendMessage()
        {
            return ForMessages().Method(RestSharp.Method.POST);
        }

        /// <summary>
        /// Specifies unsubscribes resource. Request returns a list of email addresses.
        /// </summary>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForUnsubscribes()
        {
            request.Resource = "unsubscribes";

            return this;
        }

        /// <summary>
        /// Specifies unsubscribes resource with specified email address.
        /// </summary>
        /// <param name="emailAddress">Email address to query unsubscribes for.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForUnsubscribes(string emailAddress)
        {
            request.Resource = $"unsubscribes/{emailAddress}";

            return this;
        }

        /// <summary>
        /// Specifies bounces resource. Request returns a list of email addresses.
        /// </summary>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForBounces()
        {
            request.Resource = "bounces";
            return this;
        }

        /// <summary>
        /// Specifies bounces resource with specified email address.
        /// </summary>
        /// <param name="emailAddress">Email address to query bounces for.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForBounces(string emailAddress)
        {
            request.Resource = $"bounces/{emailAddress}";
            return this;
        }

        /// <summary>
        /// Specifies complaints resource. Request returns a list of email addresses.
        /// </summary>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForComplaints()
        {
            request.Resource = "complaints";
            return this;
        }

        /// <summary>
        /// Specifies complaints resource with specified email address.
        /// </summary>
        /// <param name="emailAddress">Email address to query complaints for.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ForComplaints(string emailAddress)
        {
            request.Resource = $"complaints/{emailAddress}";
            return this;
        }

        /// <summary>
        /// Adds begin parameter.
        /// </summary>
        /// <param name="begin">Begin filter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Begin(DateTime begin)
        {
            return WithParameter("begin", begin.ToString("r"));
        }

        /// <summary>
        /// Adds pretty parameter.
        /// </summary>
        /// <param name="prettyResponse">Specifies whether response should be pretty printed.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Pretty(bool prettyResponse)
        {
            if (prettyResponse)
                return WithParameter("pretty", "yes");

            return this;
        }

        /// <summary>
        /// Adds from parameter.
        /// </summary>
        /// <param name="from">From parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder From(string from)
        {
            if (string.IsNullOrEmpty(from))
                throw new ArgumentNullException("from");

            return WithParameter("from", from);
        }

        /// <summary>
        /// Adds to parameter.
        /// </summary>
        /// <param name="to">To parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder To(string to)
        {
            if (string.IsNullOrEmpty(to))
                throw new ArgumentNullException("to");

            return WithParameter("to", to);
        }

        /// <summary>
        /// Adds cc parameter.
        /// </summary>
        /// <param name="cc">Cc parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Cc(string cc)
        {
            if (string.IsNullOrEmpty(cc))
                throw new ArgumentNullException("cc");

            return WithParameter("cc", cc);
        }
        /// <summary>
        /// Adds bcc parameter.
        /// </summary>
        /// <param name="bcc">Bcc parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Bcc(string bcc)
        {
            if (string.IsNullOrEmpty(bcc))
                throw new ArgumentNullException("bcc");

            return WithParameter("bcc", bcc);
        }

        /// <summary>
        /// Adds subject parameter.
        /// </summary>
        /// <param name="subject">Subject parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Subject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException("subject");

            return WithParameter("subject", subject);
        }

        /// <summary>
        /// Adds message as text.
        /// </summary>
        /// <param name="text">Text message.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Text(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return WithParameter("text", text);

            return this;
        }

        /// <summary>
        /// Adds html message.
        /// </summary>
        /// <param name="html">Message html.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Html(string html)
        {
            if (!string.IsNullOrEmpty(html))
                return WithParameter("html", html);

            return this;
        }

        /// <summary>
        /// Adds recipient parameter.
        /// </summary>
        /// <param name="recipient">Recipient parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Recipient(string recipient)
        {
            if (string.IsNullOrEmpty(recipient))
                throw new ArgumentNullException("recipient");

            return WithParameter("recipient", recipient);
        }

        /// <summary>
        /// Adds response limit parameter.
        /// </summary>
        /// <param name="limit">Limit parameter value.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Limit(int limit)
        {
            if (limit > 0)
                return WithParameter("limit", limit);

            return this;
        }

        /// <summary>
        /// Adds inline file as a parameter.
        /// </summary>
        /// <param name="fileName">Specifies file to add.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Inline(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            request.Attachments.Add(new MailgunAttachment("inline", fileName));

            return this;
        }

        /// <summary>
        /// Adds inline files as a parameter.
        /// </summary>
        /// <param name="fileNames">Specifies file to add.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Inline(IEnumerable<string> files)
        {
            foreach (var file in files)
                AddFile("inline", file);

            return this;
        }

        /// <summary>
        /// Adds attachment.
        /// </summary>
        /// <param name="fileName">Specifies file path to add as an attachment.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Attachment(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            AddFile("attachment", fileName);

            return this;
        }

        /// <summary>
        /// Adds attachment.
        /// </summary>
        /// <param name="fileName">Specifies files to add as an attachment.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Attachment(IEnumerable<string> files)
        {
            foreach (var file in files)
                AddFile("attachment", file);

            return this;
        }

        /// <summary>
        /// Specifies the reply-to email address to be added as a request header.
        /// </summary>
        /// <param name="replyToAddress">Email address used for reply option.</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder ReplyTo(string replyToAddress)
        {
            if (!string.IsNullOrEmpty(replyToAddress))
                return WithParameter("h:Reply-To", replyToAddress);

            return this;
        }

        /// <summary>
        /// Specifies tag for email message.
        /// </summary>
        /// <param name="tagName">Tag name</param>
        /// <returns>Builder instance.</returns>
        public MailgunResourceRequestBuilder Tag(string tagName)
        {
            if (!string.IsNullOrEmpty(tagName))
                WithParameter("o:tag", tagName);

            return this;
        }

        /// <summary>
        /// Returns generated request.
        /// </summary>
        /// <returns>MailgunResourceRequest instance.</returns>
        public MailgunResourceRequest Build()
        {
            return request;
        }

        #endregion
    }
}
