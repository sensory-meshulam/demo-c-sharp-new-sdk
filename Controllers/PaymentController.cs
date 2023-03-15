using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using static demo_new_sdk.Models.paymentModels;

namespace demo_new_sdk.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController
    {
        const string MeshulamPageCode = "539888f537b7";
        const string MeshulamApiKey = "b60e1d4cbd29";
        const string MeshulamUserId = "cf2ebf779f618e59";
        const string MeshulamApiUrl = "https://sandbox.meshulam.co.il/api/light/server/1.0/";

        // keys for production (you need to change also in index.html to "PRODUCTION"):
        //const string MeshulamPageCode = "72787ad202d0";
        //const string MeshulamApiKey = "1fac7e41f501";
        //const string MeshulamUserId = "ef28478f841d8eab";
        //const string MeshulamApiUrl = "https://secure.meshulam.co.il/api/light/server/1.0/";



        [HttpPost]
        public async Task<GenericResultDto> GetPaymentLink([FromBody]GetPaymentLinkRequest req)
        {
            var result = new GenericResultDto();

            var formData = new Dictionary<string, string>
            {
                {"pageCode", MeshulamPageCode},
                {"userId", MeshulamUserId},
                {"apiKey", MeshulamApiKey},
                {"sum", req.Sum.ToString()},
                {"paymentNum", req.PaymentsNum.ToString()},
                {"description", req.Description},
                {"transactionTypes[0]", "1"},
                {"transactionTypes[1]", "6"}, // if you don't want BIT, Put 1 instead of 6 
                {"transactionTypes[2]", "13"}, // if you don't want ApplePay, Put 1 instead of 13 
                {"transactionTypes[3]", "14"}, // if you don't want GooglePay, Put 1 instead of 14
                {"cField1", "blabla"},
                {"cField2", "blabla"},
                {"cField3", MeshulamPageCode},
                {"cField4", "blabla"},
                {"cField5", "blabla"}
            };

            var form = new MultipartFormDataContent();
            foreach (var pair in formData)
            {
                form.Add(new StringContent(pair.Value), pair.Key);
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            var response = await client.PostAsync(MeshulamApiUrl + "createPaymentProcess", form);
            var responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<GetPaymentLinkResponse>(responseString);
            result.IsSuccess = res.Status > 0;
            result.Message = result.IsSuccess ? res.Data.AuthCode : res.Err.message;
            return result;
        }

        [HttpPost]
        public async Task<bool> ConfirmPayment([FromForm] ConfirmPaymentModel req)
        {
            if (req.status == "1") //todo sucsess
            {
                //you can save req.data in DB
            }

            //move to success url
            return await ApproveTransaction(req);
        }


        private async Task<bool> ApproveTransaction(ConfirmPaymentModel details)
        {

            var formData = new Dictionary<string, string> {
                { "apiKey", MeshulamApiKey },
                { "pageCode", details.data.customFields.cField3 },
                { "transactionId", details.data.transactionId.ToString() },
                { "transactionToken", details.data.transactionToken },
                { "transactionTypeId", details.data.transactionTypeId },
                { "paymentType", details.data.paymentType },
                { "processId", details.data.processId.ToString() },
                { "sum", details.data.sum },
                { "firstPaymentSum", details.data.firstPaymentSum },
                { "periodicalPaymentSum", details.data.periodicalPaymentSum },
                { "paymentsNum", details.data.paymentsNum },
                { "allPaymentsNum", details.data.allPaymentsNum },
                { "paymentDate", details.data.paymentDate },
                { "asmachta", details.data.asmachta },
                { "description", details.data.description },
                { "fullName", details.data.fullName },
                { "payerPhone", details.data.payerPhone },
                { "payerEmail", details.data.payerEmail ?? "" },
                { "cardSuffix", details.data.cardSuffix },
                { "cardType", details.data.cardType },
                { "cardTypeCode", details.data.cardTypeCode },
                { "cardBrand", details.data.cardBrand },
                { "cardBrandCode", details.data.cardBrandCode },
                { "cardExp", details.data.cardExp },
                { "processToken", details.data.processToken }
            };
            var form = new MultipartFormDataContent();
            foreach (var item in formData)
            {
                form.Add(new StringContent(item.Value), item.Key);
            }

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

            var response = await client.PostAsync(MeshulamApiUrl + "approveTransaction", form);
            var responseString = await response.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<ApproveTransactionResponse>(responseString);
            if (!string.IsNullOrEmpty(res?.err?.message))
            {
                //display error message
            }

            return res?.status == 1;
        }
    }
}


