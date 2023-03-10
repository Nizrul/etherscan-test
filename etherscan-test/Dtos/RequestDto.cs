// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class RequestBody<T>
{
    public string jsonrpc { get; set; }
    public string method { get; set; }
    public T param { get; set; }
    public int id { get; set; }

    public RequestBody(string method, T param)
    {
        jsonrpc = "2.0";
        this.method = method;
        this.param = param;
        id = 0;
    }
}

