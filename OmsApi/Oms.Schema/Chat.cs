namespace Oms.Schema;

public class ChatRequest
{
    public string ReceiverEmail { get; set; }
    public string Message { get; set; }
}

public class ChatResponse
{
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    public string Message { get; set; }
    public string InsertDate { get; set; }
}