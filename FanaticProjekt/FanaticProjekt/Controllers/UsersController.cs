using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;
using Antlr.Runtime.Debug;


namespace FanaticProjekt.Controllers
{

    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private DbModel db = new DbModel();

        // GET: api/Users/token
        [Route("{token}")]
        [ResponseType(typeof(UsersTable))]
        public IHttpActionResult GetUser(string token)
        {
            try
            {
                // opretter et string array udfra den token den modtager
                // token er en "placeholder" for noget andet, derfor er den UTF8 og fra base64 for at simulere noget sikkerhed
                // den splitter token ved ':', som appen sætter imellem username og passwordet
                var userNameAndPass = new string(Encoding.UTF8.GetChars(Convert.FromBase64String(token))).Split(':');
                // assigner username fra array'et til en variabel
                var userName = userNameAndPass[0];
                // assigner password fra array'et til en variabel
                var pass = userNameAndPass[1];
                // prøver at finde en user, hvor username matcher
                var user = db.UsersTables.FirstOrDefault(x => x.user_login.Equals(userName,
                    StringComparison.OrdinalIgnoreCase));
                // eksistere den ikke er den null, og returnere notfound, burde måske være unauthorized
                if (user == null)
                {
                    return NotFound();
                }
                // tjekker om den fundne user har et pass som er lig med pass fra token
                if (user.user_pass != pass)
                {
                    return Unauthorized();
                }
                // sender en user tilbage uden passwordet
                user.user_pass = null;
                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
