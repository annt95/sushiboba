using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bobaadmin.Controllers;


namespace bobaadmin.Models
{
    public class BobaDA
    {
        protected readonly bobachaContext _bobadb;

        public BobaDA(bobachaContext bobaContext)
        {
            _bobadb = bobaContext;
        }
        
        public List<Menu> GetListAdminItem()
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[PRC_GetDataAdmin]")
                       .ExecuteStoredProc<Menu>();
            return lst.ToList();
        }
        public List<OrderView> GetListOrder(string stt)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[PRC_GetOrderbystt]")
                        .WithSqlParam("@stt", stt)
                       .ExecuteStoredProc<OrderView>();
            return lst.ToList();
        }
        public Menu GetListAdminItembyId(int ID)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[PRC_GetDataAdmin]")
                    .WithSqlParam("@ID", Convert.ToInt64(ID))
                       .ExecuteStoredProc<Menu>().FirstOrDefault();
            return lst;
        }
        public Admins CheckAccount(string username)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[PRC_CheckAccount]")
                    .WithSqlParam("@username", username)
                       .ExecuteStoredProc<Admins>().FirstOrDefault();
            return lst;
        }
        public List<CountOrder> CountOrder()
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[CountOrder]")
                       .ExecuteStoredProc<CountOrder>().ToList();
            return lst;
        }

    }
}
