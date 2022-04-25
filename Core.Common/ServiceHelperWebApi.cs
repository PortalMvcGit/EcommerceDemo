using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace Core.Common
{
    public class ServiceHelperWebApi : BaseServiceHelper
    {
        /// <summary>
        /// Initialize object
        /// </summary> 
        private IHttpContextAccessor _contextAccessor;

        private IConfiguration _config;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="contextAccessor"></param>
        public ServiceHelperWebApi(IHttpContextAccessor contextAccessor, IConfiguration config)
        {
            _contextAccessor = contextAccessor;
            _config = config;
        }

        /// <summary>
        /// Executes the service request.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public override TResult ExecuteServicePostRequest<TRequest, TResult>(string apiUrl, TRequest request, int cancelTime = 0)
        {
            TResult result = default(TResult);
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            using (var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            })

            using (HttpClient client = new HttpClient(handler))
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new Uri(_config["AppSettings:ServiceUrl"]);
                try
                {
                    if (cancelTime == 0)
                    {
                        httpResponse = client.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
                    }
                    else
                    {
                        CancellationTokenSource ca = new CancellationTokenSource();
                        ca.CancelAfter(cancelTime * 1000);
                        httpResponse = client.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"), ca.Token).GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    //result = httpResponse.Content.ReadAsAsync<TResult>().Result;
                    result = httpResponse.Content.ReadAsAsync<TResult>().GetAwaiter().GetResult();
                }
                else if (httpResponse.StatusCode == HttpStatusCode.NotAcceptable)
                {
                    //result = httpResponse.Content.ReadAsAsync<TResult>().Result;
                    result = httpResponse.Content.ReadAsAsync<TResult>().GetAwaiter().GetResult();
                }
                else
                {
                    if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        //throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    }
                }
            }
            return result;

        }

        /// <summary>
        /// Call Get Service
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="apiUrl"></param>
        /// <param name="cancelTime"></param>
        /// <returns></returns>
        public override TResult ExecuteServiceGetRequest<TResult>(string apiUrl, int cancelTime = 0)
        {
            TResult result = default(TResult);
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            using (var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            })

            using (HttpClient client = new HttpClient(handler))
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.BaseAddress = new Uri(_config["AppSettings:ServiceUrl"]);

                try
                {
                    if (cancelTime == 0)
                    {
                        httpResponse = client.GetAsync(apiUrl).GetAwaiter().GetResult();
                    }
                    else
                    {
                        CancellationTokenSource ca = new CancellationTokenSource();
                        ca.CancelAfter(cancelTime * 1000);
                        httpResponse = client.GetAsync(apiUrl, ca.Token).GetAwaiter().GetResult();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    result = httpResponse.Content.ReadAsAsync<TResult>().GetAwaiter().GetResult();
                }
                else if (httpResponse.StatusCode == HttpStatusCode.NotAcceptable)
                {
                    //result = httpResponse.Content.ReadAsAsync<TResult>().Result;
                    result = httpResponse.Content.ReadAsAsync<TResult>().GetAwaiter().GetResult();
                }
                else
                {
                    if (httpResponse.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        //throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
                        throw new Exception(httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                    }
                }
            }
            return result;

        }


    }
}
