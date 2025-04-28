using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
namespace BuildingManagement.API.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected string Id
        {
            get
            {
                var subClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
                if(subClaim == null)
                    throw new InvalidOperationException("User ID claim missing or invalid");
                return subClaim.Value;
            }
        }

        protected string Name
        {
            get
            {
                var nameClaim = User.FindFirst(JwtRegisteredClaimNames.Name);
                if (nameClaim == null)
                    throw new InvalidOperationException("User name claim missing or invalid");
                return nameClaim.Value;
            }
        }
    }
}
