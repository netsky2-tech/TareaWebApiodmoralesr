using AWBLL.Production;
using AWEntities.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AWEntities.Runtime;
using System.Threading.Tasks;
using AWEntities.Authentication;
using System.Security.Cryptography;
using System.Web.Http.Cors;

namespace AWWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class AuthController : ApiController
    {
        public static user user = new user();
         /*[Route("api/Register")]
         public Result Post([FromBody] user User)
         {
             return AuthenticationBLL.Register(User);
         }*/
        
        [Route("api/Login")]
        public Result Post([FromBody] user User)
        {
            return AuthenticationBLL.Login(User);
        }
        
    }
}