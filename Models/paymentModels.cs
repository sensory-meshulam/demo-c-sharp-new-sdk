namespace demo_new_sdk.Models
{
    public class paymentModels
    {
        public class GenericResultDto
        {
            public bool IsSuccess { get; set; }
            public string? Message { get; set; }
        }

        public class GetPaymentLinkRequest
        {
            public int Sum { get; set; }
            public int PaymentsNum { get; set; } = 1;
            public string Description { get; set; }
        }

        public class GetPaymentLinkResponse
        {
            public int Status { get; set; }
            public dynamic Err { get; set; }
            public GetPaymentUrlData Data { get; set; }
        }
        public class GetPaymentUrlData
        {
            public int ProcessId { get; set; }
            public string ProcessToken { get; set; }
            public string AuthCode { get; set; }
        }

        public class ConfirmPaymentModel
        {
            public string? err { get; set; }
            public string? status { get; set; }
            public SavePaymentProcessDetailsData? data { get; set; }
        }

        public class SavePaymentProcessDetailsData
        {
            public string status { get; set; }
            public string statusCode { get; set; }
            public string transactionTypeId { get; set; }
            public string paymentType { get; set; }
            public string sum { get; set; }
            public string paymentsNum { get; set; }
            public string allPaymentsNum { get; set; }
            public string paymentDate { get; set; }
            public string description { get; set; }
            public string fullName { get; set; }
            public string payerPhone { get; set; }
            public string? payerEmail { get; set; }
            public CustomFields customFields { get; set; }
            public string? cardToken { get; set; }
            public int transactionId { get; set; }
            public string transactionToken { get; set; }
            public int processId { get; set; }
            public string processToken { get; set; }
            public string cardType { get; set; }
            public string cardTypeCode { get; set; }
            public string cardBrand { get; set; }
            public string cardBrandCode { get; set; }
            public string cardExp { get; set; }
            public string cardSuffix { get; set; }
            public string firstPaymentSum { get; set; }
            public string periodicalPaymentSum { get; set; }
            public string asmachta { get; set; }

        }

        public class CustomFields
        {
            public string? cField1 { get; set; }
            public string? cField2 { get; set; }
            public string? cField3 { get; set; }
            public string? cField4 { get; set; }
            public string? cField5 { get; set; }
        }

        public class ApproveTransactionResponse
        {
            public ApproveTransactionResponseError err { get; set; }
            public int status { get; set; }
            public string data { get; set; }
        }

        public class ApproveTransactionResponseError
        {
            public int id { get; set; }
            public string message { get; set; }
        }
    }
}
