#r "Newtonsoft.Json"

using System.Net;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"RequestUri={req.RequestUri}");

    // If validationToken param give, return it with plaintext.
    string token = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "validationToken", true) == 0)
        .Value;
    if (token != null)
    {
        log.Info($"Return validationToken={token}");

        var resp = new HttpResponseMessage(HttpStatusCode.OK);
        resp.Content = new StringContent(token, System.Text.Encoding.UTF8, "text/plain");
        return resp;
    }

    // Get request body (Notification Playload)
    dynamic data = await req.Content.ReadAsAsync<object>();
    log.Info($"Body = {data}");
 
    return req.CreateResponse(HttpStatusCode.OK);
}
