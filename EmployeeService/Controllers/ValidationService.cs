using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/validations")]
    [ApiController]
    public class ValidationsController : ControllerBase
    {

        // GET api/validations/{fieldname}
        [HttpGet("{fieldname}")]
        public ActionResult<string> GetValidations(string fieldname)
        {
            if (fieldname.ToUpper() == "PHONENUMBER")
            {
                return Ok("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            }

            return NotFound();
        }
    }
}