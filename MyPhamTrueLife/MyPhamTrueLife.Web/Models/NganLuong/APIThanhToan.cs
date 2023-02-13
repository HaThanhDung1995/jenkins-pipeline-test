using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static MyPhamTrueLife.BLL.Ultil.NganLuongModels;

namespace MyPhamTrueLife.Web.Models.NganLuong
{
    public class APIThanhToan
    {
        public async Task<PaymentResponse> Payment(PaymentRequest value, string TokenKey, string CheckSum)
        {
            if (string.IsNullOrEmpty(TokenKey) || string.IsNullOrEmpty(CheckSum) || value == null)
            {
                return null;
            }
            value.tokenKey = TokenKey;
            value.signature = CreateSignaturePayment(CheckSum, value);
            var jsonData = JsonConvert.SerializeObject(value);
            byte[] payload = Encoding.UTF8.GetBytes(jsonData);
            #region Headers for Request
            IDictionary<string, string> signedHeaders = new Dictionary<string, string>();
            signedHeaders.Add("Content-Type", "application/json");
            signedHeaders.Add("Accept", "*/*");
            signedHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            signedHeaders.Add("Connection", "keep-alive");
            signedHeaders.Add("Cookie", "PHPSESSID=dv86bnthu79t934jqvqp3rei42");
            signedHeaders.Add("Cache-Control", "no-cache");
            #endregion

            #region Call Api
            //onepayout/api/v1/customers should use in appsetting with configuration
            var url = "https://alepay-v3-sandbox.nganluong.vn/api/v3/checkout/request-payment";

            var response = await FundTransferPayment(value, url, signedHeaders);
            return response ?? null;
            #endregion
        }
        private async Task<PaymentResponse> FundTransferPayment(PaymentRequest value, string uri, IDictionary<string, string> signedHeaders)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.Post.ToString());
            request.AddJsonBody(value, contentType: "application/json");
            request.AddHeaders(signedHeaders);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var resContent = response.Content;
                var data = JsonConvert.DeserializeObject<PaymentResponse>(resContent);
                return data;
            }
            else
            {
                var resErrContent = response.Content;
                var errData = JsonConvert.DeserializeObject<PaymentResponse>(resErrContent);
                return errData.message != "" ? errData : null;
            }
        }
        //Kiểm tra thanh toán
        public async Task<CheckPaymentResponse> CheckPayment(CheckPaymentRequest value, string TokenKey, string CheckSum)
        {
            if (string.IsNullOrEmpty(TokenKey) || string.IsNullOrEmpty(CheckSum) || value == null)
            {
                return null;
            }
            value.tokenKey = TokenKey;
            value.signature = CreateSignatureCheckPayment(CheckSum, value);
            var jsonData = JsonConvert.SerializeObject(value);
            byte[] payload = Encoding.UTF8.GetBytes(jsonData);
            #region Headers for Request
            IDictionary<string, string> signedHeaders = new Dictionary<string, string>();
            signedHeaders.Add("Content-Type", "application/json");
            signedHeaders.Add("Accept", "*/*");
            signedHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            signedHeaders.Add("Connection", "keep-alive");
            signedHeaders.Add("Cookie", "PHPSESSID=dv86bnthu79t934jqvqp3rei42");
            signedHeaders.Add("Cache-Control", "no-cache");
            #endregion

            #region Call Api
            //onepayout/api/v1/customers should use in appsetting with configuration
            var url = "https://alepay-v3-sandbox.nganluong.vn/api/v3/checkout/get-transaction-info";

            var response = await FundTransferCheckPayment(value, url, signedHeaders);
            return response ?? null;
            #endregion
        }
        private async Task<CheckPaymentResponse> FundTransferCheckPayment(CheckPaymentRequest value, string uri, IDictionary<string, string> signedHeaders)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.Post.ToString());
            request.AddJsonBody(value, contentType: "application/json");
            request.AddHeaders(signedHeaders);
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var resContent = response.Content;
                var data = JsonConvert.DeserializeObject<CheckPaymentResponse>(resContent);
                return data;
            }
            else
            {
                var resErrContent = response.Content;
                var errData = JsonConvert.DeserializeObject<CheckPaymentResponse>(resErrContent);
                return errData.message != "" ? errData : null;
            }
        }
        //Tạo Signature thanh toán
        public string CreateSignaturePayment(string CheckSum, PaymentRequest request)
        {
            if (request == null || string.IsNullOrEmpty(CheckSum))
            {
                return string.Empty;
            }
            //Chổ này không thay đổi vị trí vì chuỗi đã được sắp xếp theo thứ tự alphabet.
            string data = "amount=" + request.amount + "&buyerAddress=" + request.buyerAddress + "&buyerCity=" + request.buyerCity + "&buyerCountry=" + request.buyerCountry + "&buyerEmail=" + request.buyerEmail +
                "&buyerName=" + request.buyerName + "&buyerPhone=" + request.buyerPhone + "&cancelUrl=" + request.cancelUrl + "&currency=" + request.currency + "&customMerchantId=" + request.customMerchantId +
                "&orderCode=" + request.orderCode + "&orderDescription=" + request.orderDescription + "&returnUrl=" + request.returnUrl + "&tokenKey=" + request.tokenKey + "&totalItem=" + request.totalItem;
            var key = hmacSha256(CheckSum, data);
            return hex(key);
        }
        //Tạo Signature kiểm tra thanh toán
        public string CreateSignatureCheckPayment(string CheckSum, CheckPaymentRequest request)
        {
            if (request == null || string.IsNullOrEmpty(CheckSum))
            {
                return string.Empty;
            }
            //Chổ này không thay đổi vị trí vì chuỗi đã được sắp xếp theo thứ tự alphabet.
            string data = "tokenKey=" + request.tokenKey + "&transactionCode=" + request.transactionCode;
            var key = hmacSha256(CheckSum, data);
            return hex(key);
        }
        //Thuật toán HMAC
        private static byte[] hmacSha256(string key, string data) { return hmacSha256(Encoding.UTF8.GetBytes(key), data); }
        private static byte[] hmacSha256(byte[] key, string data)
        {
            var kha = KeyedHashAlgorithm.Create("HMACSHA256");
            kha.Key = key;
            return kha.ComputeHash(Encoding.UTF8.GetBytes(data));
        }
        private static byte[] sha256Hash(string data) { return sha256Hash(Encoding.UTF8.GetBytes(data)); }
        private static HashAlgorithm SHA256Algorithm = HashAlgorithm.Create("SHA-256");
        private static byte[] sha256Hash(byte[] data) { return SHA256Algorithm.ComputeHash(data); }
        private static string hex(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }

    //Tham số của thanh toán
    public class PaymentRequest
    {
        public string tokenKey { get; set; }
        public string orderCode { get; set; }
        public string customMerchantId { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string orderDescription { get; set; }
        public int totalItem { get; set; }
        public string returnUrl { get; set; } //Đưa đường dẫn link cần đá về app chổ này
        public string cancelUrl { get; set; }
        public string buyerName { get; set; }
        public string buyerEmail { get; set; }
        public string buyerPhone { get; set; }
        public string buyerAddress { get; set; }
        public string buyerCity { get; set; }
        public string buyerCountry { get; set; }
        public string signature { get; set; }
    }
    public class PaymentResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public string checkoutUrl { get; set; }
        public string transactionCode { get; set; }
        public string signature { get; set; }
    }
    //Tham số của kiểm tra thanh toán
    //public class CheckPaymentRequest
    //{
    //    public string tokenKey { get; set; }
    //    public string transactionCode { get; set; }
    //    public string signature { get; set; }
    //}
    public class CheckPaymentResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public string signature { get; set; }
        public string transactionCode { get; set; }
        public string orderCode { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string buyerEmail { get; set; }
        public string buyerPhone { get; set; }
        public string cardNumber { get; set; }
        public string buyerName { get; set; }
        public string status { get; set; }
        public string reason { get; set; }
        public string description { get; set; }
        public bool installment { get; set; }
        public bool is3D { get; set; }
        public int month { get; set; }
        public string bankCode { get; set; }
        public string bankName { get; set; }
        public string method { get; set; }
        public string bankType { get; set; }
        public long transactionTime { get; set; }
        public long successTime { get; set; }
        public string bankHotline { get; set; }
        public double merchantFee { get; set; }
        public double payerFee { get; set; }
        public string authenCode { get; set; }
    }
}
