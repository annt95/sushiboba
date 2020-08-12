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
