using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Controllers;


namespace FrontEnd.Models
{
    public class BobaDA
    {
        protected readonly bobachaContext _bobadb;

        public BobaDA(bobachaContext bobaContext)
        {
            _bobadb = bobaContext;
        }
        public List<Menu> GetListItem()
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[PRC_GetData]")
                       .ExecuteStoredProc<Menu>();
            return lst.ToList();
        }

        public int AddOrder(OrderView o)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[AddOrder]")
                        .WithSqlParam("@fname", o.CustomerName)
                        .WithSqlParam("@fphone", o.CustomerPhone)
                        .WithSqlParam("@totalMoney", o.TotalMoney)
                        .WithSqlParam("@location", o.Address)
                        .WithSqlParam("@note", o.Note)
                        .WithSqlParam("@ShippingTypeID", o.ShippingTypeID)
                        .WithSqlParam("@ShippingFee", o.ShippingFee)
                       .ExecuteStoredProcValues<int>();
            return lst.Value;
        }
        public void AddOrderItem(int id, int pid, int quantity, string price)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[AddOrderItems]")
                        .WithSqlParam("@OrderID", id)
                        .WithSqlParam("@ProductID", pid)
                        .WithSqlParam("@Quantity", quantity)
                        .WithSqlParam("@Price", price)
                       .ExecuteStoredNonQuery();
        }
        public Menu GetItembyID(int ID)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[GetItembyID]")
                        .WithSqlParam("@ID", ID)
                       .ExecuteStoredProc<Menu>();
            return lst.FirstOrDefault();
        }
        public OrderView GetOrder(int id)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[PRC_GetOrderbyID]")
                        .WithSqlParam("@ID", id)
                       .ExecuteStoredProc<OrderView>().FirstOrDefault();
            return lst;
        }
        
    }
}
