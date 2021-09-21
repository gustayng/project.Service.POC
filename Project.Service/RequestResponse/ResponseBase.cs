namespace Project.Service.RequestResponse
{
    public class ResponseBase
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
        public string CorrelationId { get; set; }
    }
}