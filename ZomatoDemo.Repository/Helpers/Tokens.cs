using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Utility;
using ZomatoDemo.Repository.Authentication;

namespace ZomatoDemo.Repository.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(UserAC user, IList<string> userRole, ClaimsIdentity identity, IJwtFactory jwtFactory, 
                                                    string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds,
                userName = user.UserName,
                fullName = user.FullName,
                role= userRole

            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}
