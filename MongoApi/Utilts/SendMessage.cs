using RestSharp;
using RestSharp.Authenticators;
using System;

namespace MongoApi.Utilts
{
    public class SendMessage
    {
        public static IRestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "key-");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandbox61f6ddc0967843f4b3f2d1a68194050b.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Jaski<mailgun@sandbox61f6ddc0967843f4b3f2d1a68194050b.mailgun.org>");
            request.AddParameter("to", "janwiecinski1985@gmail.com");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness! Kurwa Tuutaaaj :)");
            request.Method = Method.POST;
            return client.Execute(request);
        }
    }
}
