using e_TimesheetNET7.Usecase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace e_TimesheetNET7.Controllers
{
    [Route("api/timesheet")]
    [ApiController]
    public class TimesheetController : ControllerBase
    {
        private readonly ITimesheetUsecase _tsUsecase;
        public TimesheetController(ITimesheetUsecase tsUsecase)
        {
            _tsUsecase = tsUsecase;
        }

        [HttpGet("/TimesheetData/{internalTsNo}/{tahun}")]
        public async Task<ActionResult> GetTimesheetData(string internalTsNo, string tahun)
        {
            try
            {
                var result = await _tsUsecase.GetTimesheetData(internalTsNo, tahun);
                if (result == null)
                {
                    return BadRequest("Not found");
                }
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                return Ok(json);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        //[HttpPost("/PostTimesheet/{InternalTSNo}{Tahun}")]
        //public async Task<ActionResult> PostTimesheet(string internalTsNo, string tahun)
        //{

        //}
    }
}
