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
    public class ProductCategoryBll
    {
        private readonly ProductCategoryDB dbCategory;
        public ProductCategoryBll()
        {
            dbCategory = new ProductCategoryDB();
        }
        public static List<ProductCategory> Get()
        {
            return ProductCategoryDB.Get();
        }
        public static Result Insert(ProductCategory productCategory)
        {
            return ProductCategoryDB.Insert(productCategory);
        }
        public static Result Update(ProductCategory productCategory)
        {
            return ProductCategoryDB.Update(productCategory);
        }
        public static Result Delete(int productCategoryId)
        {
            return ProductCategoryDB.Delete(productCategoryId);
        }
    }
}
