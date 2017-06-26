using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppLunch.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var userName = "testing2@tester.com";
            var userPassword = "Password01";

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            var result = await userManager.CreateAsync(new IdentityUser(userName), userPassword);
            var user = userManager.FindByName(userName);
            var claimResult = userManager.AddClaim(user.Id, new Claim("Level", "Admin"));

            return Ok();
        }
    }
}