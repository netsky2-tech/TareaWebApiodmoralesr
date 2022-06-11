using AWDAL.Production;
using AWEntities.Production;
using AWEntities.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWBLL.Production
{
    public class ProductBll
    {
        private readonly ProductDB dbProduct;
        public ProductBll()
        {
            dbProduct = new ProductDB();
        }
        public static List<Product> Get()
        {
            return ProductDB.Get();
        }
        public static Result Insert(Product productCategory)
        {
            return ProductDB.Insert(productCategory);
        }
        public static Result Update(Product productCategory)
        {
            return ProductDB.Update(productCategory);
        }
        public static Result Delete(int productCategoryId)
        {
            return ProductDB.Delete(productCategoryId);
        }
    }
}
