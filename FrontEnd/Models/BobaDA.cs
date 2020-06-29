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
    }
}
