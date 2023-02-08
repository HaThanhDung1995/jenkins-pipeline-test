using MyPhamTrueLife.DAL.Models1;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhamTrueLife.DAL.Models.Utils
{
    public class ProductReq
    {
    }
    public class ListProductPortfolioUser
    {
        public string PortfolioName { get; set; }
        public List<ListTypeProductUser> listTypeProductUsers { get; set; }
    }
    public class ListTypeProductUser
    {
        public int TypeProductId { get; set; }
        public string TypeProductName { get; set; }
    }
    public class ListProductSelling
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Avatar { get; set; }
        public int Price { get; set; }
        public int PriceDiscount { get; set; }
    }
    public class CountProduct
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class ListProductPromotion
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Avatar { get; set; }
        public int Price { get; set; }
        public int PriceDiscount { get; set; }
    }
    public class ProductFilterRequest
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int? ProductTypeId { get; set; }
    }

    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
        public int? PriceDiscount { get; set; }
        public string Trademark { get; set; }
        public string StatusProduct { get; set; }
        public int? NatureId { get; set; }
        public string NatureName { get; set; }
        public List<InfoCapacity> listCapacity { get; set; }
        public string Describe { get; set; }
        public List<string> listImage { get; set; }
        public List<ListProductSelling> listProductRelateTo { get; set; }
        public List<InfoComent> listComent { get; set; }
        public List<InfoEvaluate> listEvaluate { get; set; }
        public int? QuantityOrder { get; set; }
    }

    public class ProductDetailAdmin
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Price { get; set; }
        public int? PriceDiscount { get; set; }
        public string Trademark { get; set; }
        public string StatusProduct { get; set; }
        public int? NatureId { get; set; }
        public string NatureName { get; set; }
        public List<CapacityProductRes> listCapacityProductRes { get; set; }
        public string Describe { get; set; }
        public List<InfoImageProduct> listImage { get; set; }
        public List<InfoExpiryProduct> listInfoExpiryProducts { get; set; }
    }

    public class CapacityProductRes
    {
        public int CapacityId { get; set; }
        public string CapacityName { get; set; }
        public string Unit { get; set; }

        public int PriceProductId { get; set; }
        public int? Price { get; set; }
    }
}
