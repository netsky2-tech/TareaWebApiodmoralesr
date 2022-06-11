using AWBLL.Production;
using AWEntities.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AWEntities.Runtime;
using System.Web.Http.Cors;

namespace AWWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class ArticulesController : ApiController
    {
        private readonly ProductBll categoryBll;
        public ArticulesController()
        {
            categoryBll = new ProductBll();
        }
        // GET api/<controller>
        public IEnumerable<Product> Get()
        {
            return ProductBll.Get();
        }


        // POST api/<controller>
        public Result Post([FromBody] Product productCategory)
        {
            return ProductBll.Insert(productCategory);
        }

        // PUT api/<controller>/5
        public Result Put([FromBody] Product productCategory)
        {
            return ProductBll.Update(productCategory);
        }

        // DELETE api/<controller>/5
        public Result Delete(int id)
        {
            return ProductBll.Delete(id);
        }
    }
}