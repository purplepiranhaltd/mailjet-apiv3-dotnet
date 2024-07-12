﻿using Mailjet.Client.Exceptions;
using Mailjet.Client.Helpers;
using Mailjet.Client.Resources;
using Mailjet.Client.Serialization;
using Mailjet.Client.TransactionalEmails;
using Mailjet.Client.TransactionalEmails.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mailjet.Client
{
    /// <summary>
    /// Mailjet API wrapper
    /// </summary>
    public class MailjetClient : IMailjetClient
    {
        private HttpClient _httpClient;

        public MailjetClient(string apiKey, string apiSecret, HttpMessageHandler httpMessageHandler = null)
        {
            InitHttpClient(httpMessageHandler);
            _httpClient.UseBasicAuthentication(apiKey, apiSecret);
        }

        public MailjetClient(string token, HttpMessageHandler httpMessageHandler = null)
        {
            InitHttpClient(httpMessageHandler);
            _httpClient.UseBearerAuthentication(token);
        }

        /// <summary>
        /// Create MailJet client with predefined HttpClient instance
        /// </summary>
        /// <param name="httpClient"></param>
        public MailjetClient(HttpClient httpClient)
        {
            if (httpClient == null)
            {
                throw new ArgumentNullException(nameof(httpClient));
            }

            if (httpClient.BaseAddress == null)
            {
                httpClient.SetDefaultSettings();
            }

            _httpClient = httpClient;
        }

        public string BaseAdress
        {
            get { return _httpClient.BaseAddress != null ? _httpClient.BaseAddress.ToString() : null; }
            set { _httpClient.BaseAddress = !string.IsNullOrEmpty(value) ? new Uri(value) : null; }
        }

        public async Task<MailjetResponse> GetAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var responseMessage = await _httpClient.GetAsync(url).ConfigureAwait(false);

            JObject content = await HttpContentHelper.GetContentAsync(responseMessage).ConfigureAwait(false);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public async Task<MailjetResponse> PostAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPost = new StringContent(output, Encoding.UTF8, MailjetConstants.JsonMediaType);
            var responseMessage = await _httpClient.PostAsync(url, contentPost).ConfigureAwait(false);

            JObject content = await HttpContentHelper.GetContentAsync(responseMessage).ConfigureAwait(false);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        public async Task<MailjetResponse> PutAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var output = request.Body.ToString(Formatting.None);
            HttpContent contentPut = new StringContent(output, Encoding.UTF8, MailjetConstants.JsonMediaType);
            var responseMessage = await _httpClient.PutAsync(url, contentPut).ConfigureAwait(false);

            JObject content = await HttpContentHelper.GetContentAsync(responseMessage).ConfigureAwait(false);
            MailjetResponse mailjetResponse = new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
            return mailjetResponse;
        }

        public async Task<MailjetResponse> DeleteAsync(MailjetRequest request)
        {
            string url = request.BuildUrl();

            var responseMessage = await _httpClient.DeleteAsync(url).ConfigureAwait(false);

            JObject content = await HttpContentHelper.GetContentAsync(responseMessage).ConfigureAwait(false);
            return new MailjetResponse(responseMessage.IsSuccessStatusCode, (int)responseMessage.StatusCode, content);
        }

        private void InitHttpClient(HttpMessageHandler httpMessageHandler)
        {
            // Create HttpClient
            _httpClient = (httpMessageHandler != null) ? new HttpClient(httpMessageHandler) : new HttpClient();

            _httpClient.SetDefaultSettings();
        }

        /// <summary>
        /// Sends a single transactional email using send API v3.1
        /// </summary>
        /// <exception cref="MailjetClientConfigurationException">Thrown when email count exceeds the max allowed number</exception>
        /// <exception cref="MailjetServerException">Thrown when generic error returned from the server</exception>
        public Task<TransactionalEmailResponse> SendTransactionalEmailAsync(TransactionalEmail transactionalEmail, bool isSandboxMode = false, bool advanceErrorHandling = true)
        {
            return SendTransactionalEmailsAsync(new[] { transactionalEmail }, isSandboxMode, advanceErrorHandling);
        }

        /// <summary>
        /// Sends transactional emails using send API v3.1
        /// </summary>
        /// <exception cref="MailjetClientConfigurationException">Thrown when email count exceeds the max allowed number</exception>
        /// <exception cref="MailjetServerException">Thrown when generic error returned from the server</exception>
        public async Task<TransactionalEmailResponse> SendTransactionalEmailsAsync(IEnumerable<TransactionalEmail> transactionalEmails, bool isSandboxMode = false, bool advanceErrorHandling = true)
        {
            if (transactionalEmails.Count() > SendV31.MaxEmailsPerBatch || !transactionalEmails.Any())
                throw new MailjetClientConfigurationException($"Send Emails API v3.1 allows to send not more than {SendV31.MaxEmailsPerBatch} emails per call");

            var request = new SendEmailRequest
            {
                Messages = transactionalEmails.ToList(),
                SandboxMode = isSandboxMode,
                AdvanceErrorHandling = advanceErrorHandling
            };

            var clientRequest = new MailjetRequest
            {
                Resource = SendV31.Resource,
                Body = JObject.FromObject(request, JsonSerializers.MailjetJsonSerializer)
            };

            MailjetResponse clientResponse = await PostAsync(clientRequest)
                .ConfigureAwait(false);

            TransactionalEmailResponse result = clientResponse.Content.ToObject<TransactionalEmailResponse>();

            if (result.Messages == null && !clientResponse.IsSuccessStatusCode)
            {
                throw new MailjetServerException(clientResponse);
            }

            return result;
        }
    }
}
