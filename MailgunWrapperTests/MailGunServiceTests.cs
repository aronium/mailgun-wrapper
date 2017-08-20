using MailgunWrapper;
using MailgunWrapper.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;

namespace MailgunWrapper.Tests
{
    [TestClass()]
    public class MailgunServiceTests
    {
        private MailgunService service;

        [TestInitialize]
        public void Setup()
        {
            service = new MailgunService(ConfigurationManager.AppSettings["Mailgun.Domain"], ConfigurationManager.AppSettings["Mailgun.ApiKey"]);
        }

        [TestMethod()]
        public void SendEmailTest()
        {
            var response = service.GetResponse<MailgunSendEmailResponse>(MailgunResourceRequest.Builder
                .ForSendMessage()
                .From("Aronium <me@aronium.com>")
                .To("mail@example.com")
                .Subject("Hi")
                .Text("It is I, Leclerc!")
                .Build());

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetEventsTest()
        {
            var request = MailgunResourceRequest.Builder
                .ForEvents(MailgunEventType.Unsubscribed | MailgunEventType.Failed)
                .Begin(DateTime.Now.AddDays(-1))
                .Limit(10)
                .Build();

            var response = service.GetResponse<MailgunEventCollection>(request);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success);
        }

        [TestMethod()]
        public void PagingTest()
        {
            var request = MailgunResourceRequest.Builder
                .ForEvents(MailgunEventType.Accepted)
                .Limit(10)
                .Build();

            var response = service.GetResponse<MailgunPagedResponse<MailgunEvent>>(request);

            var items = response.Items;

            if (!string.IsNullOrEmpty(response.Paging?.Next))
            {
                response = service.GetPage<MailgunPagedResponse<MailgunEvent>>(request, response.Paging.Next);

                items.AddRange(response.Items);
            }

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetUnsubscribesByAddressTest()
        {
            var response = service.GetResponse<MailgunEmailAddress>(MailgunResourceRequest.Builder
                .ForUnsubscribes("test@example.com")
                .Build());

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetUnsubscribesTest()
        {
            var response = service.GetResponse<MailgunEmailAddressCollection>(MailgunResourceRequest.Builder
                .ForUnsubscribes()
                .Build());

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetComplaintByAddressTest()
        {
            var response = service.GetResponse<MailgunEmailAddress>(MailgunResourceRequest.Builder
                .ForComplaints("test@example.com")
                .Build());

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetComplaintsTest()
        {
            var response = service.GetResponse<MailgunEmailAddressCollection>(MailgunResourceRequest.Builder
                .ForComplaints()
                .Build());

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetBounceByAddressTest()
        {
            var response = service.GetResponse<MailgunEmailAddress>(MailgunResourceRequest.Builder
                .ForBounces("test@example.com")
                .Build());

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void GetBouncesTest()
        {
            var response = service.GetResponse<MailgunEmailAddressCollection>(MailgunResourceRequest.Builder
                .ForBounces()
                .Build());

            Assert.IsNotNull(response);
        }

        [TestMethod()]
        public void IsSuppressedTest()
        {
            var suppressType = MailgunEventType.None;

            var response = service.IsSuppressed("unsubscribed@example.com", out suppressType);
            Assert.IsTrue(suppressType == MailgunEventType.Unsubscribed);
            Assert.IsTrue(response);

            response = service.IsSuppressed("bounced@example.com", out suppressType);
            Assert.IsTrue(suppressType == MailgunEventType.Failed);
            Assert.IsTrue(response);

            response = service.IsSuppressed("complained@example.com", out suppressType);
            Assert.IsTrue(suppressType == MailgunEventType.Complained);
            Assert.IsTrue(response);

            response = service.IsSuppressed("unexisting@example.com", out suppressType);
            Assert.IsTrue(suppressType == MailgunEventType.None);
            Assert.IsFalse(response);
        }
    }
}