using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
namespace BuildingManagement.API.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        protected int Id
        {
            get
            {
                var subClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if(subClaim == null)
                    throw new InvalidOperationException("User ID claim missing or invalid");
                return Convert.ToInt32(subClaim.Value);
            }
        }

        protected string Name
        {
            get
            {
                var nameClaim = User.FindFirst(ClaimTypes.Name);
                if (nameClaim == null)
                    throw new InvalidOperationException("User name claim missing or invalid");
                return nameClaim.Value;
            }
        }
    }
}
