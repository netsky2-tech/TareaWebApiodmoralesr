using AWBLL.Production;
using AWEntities.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AWEntities.Runtime;

namespace AWWebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ProductCategoryBll categoryBll;
        public CategoriesController()
        {
            categoryBll = new ProductCategoryBll();
        }
        // GET api/<controller>
        public IEnumerable<ProductCategory> Get()
        {
            return ProductCategoryBll.Get();
        }


        // POST api/<controller>
        public Result Post([FromBody] ProductCategory productCategory)
        {
            return ProductCategoryBll.Insert(productCategory);
        }

        // PUT api/<controller>/5
        public Result Put([FromBody] ProductCategory productCategory)
        {
            return ProductCategoryBll.Update(productCategory);
        }

        // DELETE api/<controller>/5
        public Result Delete(int id)
        {
            return ProductCategoryBll.Delete(id);
        }
    }
}