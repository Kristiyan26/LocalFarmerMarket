using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalFarmerMarket.Core.Models;


namespace LocalFarmerMarket.Data.Repositories
{
    public class OrdersRepository : BaseRepository<Order>
    {
        public OrdersRepository(LocalFarmerMarketDbContext context) : base(context)
        {
        }
    }
    
}
