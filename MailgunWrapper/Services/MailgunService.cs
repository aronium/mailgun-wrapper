using MailgunWrapper.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace MailgunWrapper
{
    public class MailgunService
    {
        #region - Constants -

        /// <summary>
        /// Indicates base Mailgun API url.
        /// </summary>
        public const string BASE_URL = "https://api.Mailgun.net/v3";

        #endregion

        #region - Constructors -

        /// <summary>
        /// Initializes new instance of MailgunService class.
        /// </summary>
        public MailgunService()
        {

        }

        /// <summary>
        /// Initializes new instance of MailgunService class with specified API key.
        /// </summary>
        /// <param name="apiKey">API key.</param>
        public MailgunService(string domain, string apiKey)
        {
            if (string.IsNullOrEmpty(domain))
                throw new ArgumentNullException("domain");

            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey");

            this.Domain = domain;
            this.ApiKey = apiKey;
        }

        #endregion

        #region - Private methods -

        private RestClient GetClient()
        {
            if (string.IsNullOrEmpty(ApiKey))
                throw new ArgumentNullException("ApiKey", "API Key is not set. Please set valid API key before executing requests.");

            if (string.IsNullOrEmpty(Domain))
                throw new ArgumentNullException("Domain", "Domain is not set. Please set valid domain before executing requests.");

            RestClient client = new RestClient();
            client.BaseUrl = new Uri(BASE_URL);
            client.Authenticator = new HttpBasicAuthenticator("api", ApiKey);
            return client;
        }

        private RestRequest CreateRequest(string resource)
        {
            RestRequest request = new RestRequest();
            request.AddParameter("domain", Domain, ParameterType.UrlSegment);
            request.Resource = $"{{domain}}/{resource}";
            return request;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets or sets domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets API key.
        /// </summary>
        public string ApiKey { get; set; }

        #endregion

        #region - Public methods -

        /// <summary>
        /// Gets API response for specified request.
        /// </summary>
        /// <param name="MailgunRequest">Request.</param>
        /// <returns>API response.</returns>
        public IRestResponse GetResponse(MailgunResourceRequest MailgunRequest)
        {
            var request = CreateRequest(MailgunRequest.Resource);

            foreach (var pair in MailgunRequest.Parameters)
            {
                request.AddParameter(pair.Key, pair.Value);
            }

            foreach (var attachment in MailgunRequest.Attachments)
            {
                request.AddFile(attachment.AttachmentType, attachment.FileName);
            }

            request.Method = MailgunRequest.Method;

            return GetClient().Execute(request);
        }

        /// <summary>
        /// Gets response for specified request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="MailgunRequest">Request</param>
        /// <returns>Instance of <typeparamref name="T"/>.</returns>
        public T GetResponse<T>(MailgunResourceRequest MailgunRequest) where T : MailgunResponse
        {
            var response = GetResponse(MailgunRequest);

            var val = JsonConvert.DeserializeObject<T>(response.Content);

            val.Success = response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == System.Net.HttpStatusCode.OK;

            return val;
        }

        /// <summary>
        /// Gets next or previous page from paged response.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="MailgunRequest">Mail gun request containing full page url as a <see cref="MailgunResourceRequest.Resource"/>.</param>
        /// <param name="pageUrl">Page url taken from <see cref="MailgunPagedResponse{T}.Paging"/> property.</param>
        /// <returns>Instance of <typeparamref name="T"/>.</returns>
        public T GetPage<T>(MailgunResourceRequest MailgunRequest, string pageUrl) where T : MailgunResponse
        {
            // Extract page key from given page url, page url must be the full url stored in paging object.
            MailgunRequest.Resource += $"/{pageUrl.Substring(pageUrl.LastIndexOf("/") + 1)}";

            return GetResponse<T>(MailgunRequest);
        }

        /// <summary>
        /// Gets a value indicating whether email address is suppressed by sarching in unsubscribes, bounces and complaints.
        /// </summary>
        /// <param name="emailAddress">Email address to search for.</param>
        /// <returns>True if email address is suppressed, otherwise false.</returns>
        public bool IsSuppressed(string emailAddress, out MailgunEventType suppressType)
        {
            // Get builder instance, we are going to reuse it
            var builder = MailgunResourceRequest.Builder;

            // Check unsubsribes first
            var request = builder.ForUnsubscribes(emailAddress).Build();
            var response = GetResponse<MailgunEmailAddress>(request);
            if (response.HasAddress)
            {
                suppressType = MailgunEventType.Unsubscribed;
                return true;
            }

            // Check bounced emails
            request = builder.ForBounces(emailAddress).Build();
            response = GetResponse<MailgunEmailAddress>(request);
            if (response.HasAddress)
            {
                suppressType = MailgunEventType.Failed;
                return true;
            }

            // Check complaints
            request = builder.ForComplaints(emailAddress).Build();
            response = GetResponse<MailgunEmailAddress>(request);
            if (response.HasAddress)
            {
                suppressType = MailgunEventType.Complained;
                return true;
            }

            suppressType = MailgunEventType.None;
            return false;
        }

        #endregion
    }
}
