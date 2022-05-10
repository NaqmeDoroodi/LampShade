namespace Framework.Application
{
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string TrackingNum { get; set; }

        public PaymentResult Succeeded(string message, string trackingNum)
        {
            IsSuccessful = true;
            Message = message;
            TrackingNum = trackingNum;
            return this;
        }

        public PaymentResult Failed(string message)
        {
            IsSuccessful = false;
            Message = message;
            return this;
        }
    }
}