using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalFarmerMarket.Core.Models;


namespace LocalFarmerMarket.Data.Repositories
{
    public class OrderProductRepository : BaseRepository<OrderProduct>
    {
        public OrderProductRepository(LocalFarmerMarketDbContext context) : base(context)
        {
        }
 
    }
}
