using System;
using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log, IAsyncCollector<object> outputDocument)
{
    log.Info("C# HTTP trigger function processed a request.");
    
    // parse query parameter
    string name = req.GetQueryNameValuePairs()
        .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
        .Value;

    // Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();

    // Set name to query string or body data
    name = name ?? data?.name;

    log.Info($"Input parameter: {name}");

    var newHello = new HelloItem
    {
        Id = Guid.NewGuid(),
        Name = name
    };

    await outputDocument.AddAsync(newHello);
    
    return name == null
    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
    : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
}

public class HelloItem
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}