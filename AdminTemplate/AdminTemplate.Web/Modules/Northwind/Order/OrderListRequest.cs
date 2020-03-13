using Serenity.Services;

namespace AdminTemplate.Northwind
{
    public class OrderListRequest : ListRequest
    {
        public int? ProductID { get; set; }
    }
}