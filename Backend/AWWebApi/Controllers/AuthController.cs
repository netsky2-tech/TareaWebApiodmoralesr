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
            CrearHashContraseña(User.password, out byte[] passwordHash, out byte[] passwordSalt);

            User.PasswordHash = passwordHash;
            User.PasswordSalt = passwordSalt;

            return AuthenticationBLL.Register(User);
        }*/

        [Route("api/Login")]
        public Result Post([FromBody] user User)
        {
            VerificarHashContraseña(User.password, User.PasswordHash, out byte[] passwordSalt);
           
            return AuthenticationBLL.Login(User);
        }

        private void CrearHashContraseña(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var sha = new HMACSHA512())
            {
                passwordSalt = sha.Key;
                passwordHash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerificarHashContraseña(string password,  byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                passwordSalt = null;
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}