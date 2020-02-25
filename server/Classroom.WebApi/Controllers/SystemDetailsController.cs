using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Classroom.WebApi.Delegate;
using Classroom.WebApi.ViewModels;

namespace Classroom.WebApi.Controllers
{
    public class SystemDetailsController : BaseController
    {
        private readonly ISystemDelegate systemDelegate;

        public SystemDetailsController(ISystemDelegate systemDelegate)
        {
            this.systemDelegate = systemDelegate;
        }

        [HttpGet]
        public async Task<ActionResult<SystemDetailsVm>> GetSystemDetails()
        {
            var systemDetails = await systemDelegate.GetSystemDetails();
            return Ok(systemDetails);
        }
    }
}