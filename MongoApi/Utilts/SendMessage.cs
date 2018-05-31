using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace MongoApi.Utilts
{
    public static class SendMessage
    {

        public static IRestResponse  SendSimpleMessage(SendMessageParams param)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                          param.ApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", param.Domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", $"Jaski<mailgun@{param.Domain}>");
            request.AddParameter("to", param.Email);
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!:)");
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
