using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.BLL.Ultil
{
    public class NganLuongModels
    {
        //Thông tin đầu vào của hàm yêu cầu rút tiền SetCashoutRequest
        public class WithdrawMoneyRequest
        {
            private String _Funtion = "SetCashoutRequest";

            public String Funtion
            {
                get { return _Funtion; }
            }
            private String _Merchant_id = String.Empty;
            public String Merchant_id
            {
                get { return _Merchant_id; }
                set { _Merchant_id = value; }
            }
            private String _Receiver_email = String.Empty;

            public String Receiver_email
            {
                get { return _Receiver_email; }
                set { _Receiver_email = value; }
            }
            private String _Merchant_password = String.Empty;

            public String Merchant_password
            {
                get { return _Merchant_password; }
                set { _Merchant_password = value; }
            }
            private String _Ref_code = String.Empty;

            public String RefCode
            {
                get { return _Ref_code; }
                set { _Ref_code = value; }
            }
            private long _Total_amount = 0;
            public long Total_amount
            {
                get { return _Total_amount; }
                set { _Total_amount = value; }
            }
            private int _Account_type = 0;
            public int AccountType
            {
                get { return _Account_type; }
                set { _Account_type = value; }
            }
            private String _bank_code = String.Empty;

            public String bank_code
            {
                get { return _bank_code; }
                set { _bank_code = value; }
            }
            private String _Card_fullname = String.Empty;
            public String Card_fullname
            {
                get { return _Card_fullname; }
                set { _Card_fullname = value; }
            }
            private String _Card_number = String.Empty;
            public String CardNumber
            {
                get { return _Card_number; }
                set { _Card_number = value; }
            }
            private String _Card_month = String.Empty;
            public String CardMonth
            {
                get { return _Card_month; }
                set { _Card_month = value; }
            }
            private String _Card_year = String.Empty;
            public String CardYear
            {
                get { return _Card_year; }
                set { _Card_year = value; }
            }
            private String _branch_name = String.Empty;
            public String BranchName
            {
                get { return _branch_name; }
                set { _branch_name = value; }
            }
            private int? _zone_id = null;
            public int? ZoneId
            {
                get { return _zone_id; }
                set { _zone_id = value; }
            }
            private String _reason = String.Empty;
            public String Reason
            {
                get { return _reason; }
                set { _reason = value; }
            }
        }
        //Thông tin trả về cảu hàm yêu cầu rút tiền SetCashoutRequest
        public class WithdrawMoneyResponse
        {
            private String _error_code = string.Empty;

            public String Error_code
            {
                get { return _error_code; }
                set { _error_code = value; }
            }
            private String _Ref_code = String.Empty;

            public String Ref_Code
            {
                get { return _Ref_code; }
                set { _Ref_code = value; }
            }
            private String _transaction_id = String.Empty;

            public String Transaction_Id
            {
                get { return _transaction_id; }
                set { _transaction_id = value; }
            }

            private String _transaction_status = String.Empty;

            public String Transaction_Status
            {
                get { return _transaction_status; }
                set { _transaction_status = value; }
            }

            private int? _total_amount = null;

            public int? Total_Amount
            {
                get { return _total_amount; }
                set { _total_amount = value; }
            }
        }
        //Thông tin đầu vào của hàm kiểm tra trạng thái giao dịch rút tiền(hàm cũ)
        public class CheckWithdrawalStatusRequestOld
        {
            private String _Funtion = "CheckCashout";

            public String Funtion
            {
                get { return _Funtion; }
            }
            private String _Merchant_id = String.Empty;
            public String Merchant_id
            {
                get { return _Merchant_id; }
                set { _Merchant_id = value; }
            }
            private String _Receiver_email = String.Empty;

            public String Receiver_email
            {
                get { return _Receiver_email; }
                set { _Receiver_email = value; }
            }
            private String _Merchant_password = String.Empty;

            public String Merchant_password
            {
                get { return _Merchant_password; }
                set { _Merchant_password = value; }
            }
            private String _Ref_code = String.Empty;

            public String RefCode
            {
                get { return _Ref_code; }
                set { _Ref_code = value; }
            }
            private String _transaction_id = String.Empty;

            public String TransactionId
            {
                get { return _transaction_id; }
                set { _transaction_id = value; }
            }
        }
        //Thông tin đầu vào của hàm kiểm tra trạng thái giao dịch rút tiền(hàm mới)
        public class CheckWithdrawalStatusRequestNew
        {
            private String _Funtion = "CheckCashout";

            public String Funtion
            {
                get { return _Funtion; }
            }
            private String _Merchant_id = String.Empty;
            public String Merchant_id
            {
                get { return _Merchant_id; }
                set { _Merchant_id = value; }
            }
            private String _Receiver_email = String.Empty;

            public String Receiver_email
            {
                get { return _Receiver_email; }
                set { _Receiver_email = value; }
            }
            private String _Merchant_password = String.Empty;

            public String Merchant_password
            {
                get { return _Merchant_password; }
                set { _Merchant_password = value; }
            }
            private String _Ref_code = String.Empty;

            public String RefCode
            {
                get { return _Ref_code; }
                set { _Ref_code = value; }
            }
            private String _transaction_id = String.Empty;

            public String TransactionId
            {
                get { return _transaction_id; }
                set { _transaction_id = value; }
            }
        }
        //Thông tin quả trả về của hàm kiểm tra trạng thái giao dịch rút tiền
        public class CheckWithdrawalStatusResponse
        {
            private String _error_code = string.Empty;
            public String Error_code
            {
                get { return _error_code; }
                set { _error_code = value; }
            }
            private String _Ref_code = String.Empty;
            public String Ref_Code
            {
                get { return _Ref_code; }
                set { _Ref_code = value; }
            }
            private int? _total_amount = null;
            public int? Total_Amount
            {
                get { return _total_amount; }
                set { _total_amount = value; }
            }
            private int? _transaction_status = null;
            public int? Transaction_Status
            {
                get { return _transaction_status; }
                set { _transaction_status = value; }
            }
            private int? _transaction_id = null;
            public int? Transaction_Id
            {
                get { return _transaction_id; }
                set { _transaction_id = value; }
            }
            private String _transaction_refund_id = String.Empty;
            public String Transaction_Refund_Id
            {
                get { return _transaction_refund_id; }
                set { _transaction_refund_id = value; }
            }
        }
        public class WithdrawalMoney
        {
            public string RefCode { get; set; }
            public long TotalAmount { get; set; }
            public string BankCode { get; set; }
            public string CardFullName { get; set; }
            public string CardNumber { get; set; }
            public string CardMonth { get; set; }
            public string CardYear { get; set; }
            public string BranchName { get; set; }
            public int? ZoneId { get; set; }
            public string Reason { get; set; }
        }
        public class CheckWithdrawalStatus
        {
            public string TransactionId { get; set; }
            public string RefCode { get; set; }
        }
        //Tham số của thanh toán
        public class PaymentRequest
        {
            public int checkoutType { get; set; } = 4;
            //public string paymentMethod { get; set; } = "ATM_ON";
            public string tokenKey { get; set; }
            public bool allowDomestic { get; set; } = true;
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
        public class CheckPaymentRequest
        {
            public string tokenKey { get; set; }
            public string transactionCode { get; set; }
            public string signature { get; set; }
        }
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
}
