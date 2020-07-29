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
        public Menu GetListAdminItembyId(int ID)
        {
            var lst = _bobadb.LoadStoredProc("[dbo].[PRC_GetDataAdmin]")
                    .WithSqlParam("@ID", Convert.ToInt64(ID))
                       .ExecuteStoredProc<Menu>().FirstOrDefault();
            return lst;
        }
        
    }
}
