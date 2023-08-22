using e_TimesheetNET7.Models.Timesheet;
using e_TimesheetNET7.Usecase;
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

        [HttpPost("/PostTimesheet")]
        public async Task<ActionResult> PostTimesheet(TimesheetData tsData)
        {
            try
            {
                var result = await _tsUsecase.PostTimesheet(tsData);
                if (result == true)
                {
                    var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Ok("Timesheet data successfully post");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request 400");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
