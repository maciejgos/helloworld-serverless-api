using System.Net;

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log, IEnumerable<dynamic>inputDocument)
{
    log.Info("C# HTTP trigger function processed a request.");

    int numberOfItems = inputDocument.Count();

    log.Info($"Number of items in collection: {numberOfItems}");

    return req.CreateResponse(HttpStatusCode.OK, inputDocument);
}
