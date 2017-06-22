using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppLunch.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            var result = await userManager.CreateAsync(new IdentityUser("testing@tester.com"),"Password01");
            return Ok();
        }
    }
}
