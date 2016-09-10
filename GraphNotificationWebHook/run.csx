#r "Newtonsoft.Json"

using System.Net;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info($"C# HTTP trigger function processed a request. RequestUri={req.RequestUri}");

    // parse query parameter
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

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();
    log.Info($"Body = {data}");
    // Set name to query string or body data
    //name = name ?? data?.name;
    return req.CreateResponse(HttpStatusCode.OK, "Hello");
    //return name == null
    //    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
    //    : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
}
