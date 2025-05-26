using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalFarmerMarket.Core.Models;


namespace LocalFarmerMarket.Data.Repositories
{
    public class ProductsRepository : BaseRepository<Product>
    {
        public ProductsRepository(LocalFarmerMarketDbContext context) : base(context)
        {

        }
    }
}
