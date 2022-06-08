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

namespace AWWebApi.Controllers
{
    public class AuthController : ApiController
    {
        public static user user = new user();
        [Route("api/[controller]")]
        /*public Result Post([FromBody] UserDTO User)
        {
            CrearHashContraseña(User.password, out byte[] passwordHash, out byte[] passwordSalt);

            user.username = User.username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }*/
        private void CrearHashContraseña(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var sha = new HMACSHA512())
            {
                passwordSalt = sha.Key;
                passwordHash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}