using e_TimesheetNET7.Models.Driver;
using e_TimesheetNET7.Usecase;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace e_TimesheetNET7.Controllers
{
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly IDriverUsecase _dvrUsecase;
        private readonly IConfiguration _config;
        public DriverController(IDriverUsecase dvrUsecase, IConfiguration config) 
        {
            _dvrUsecase = dvrUsecase;
            _config = config;
        }

        [HttpPut("/driver/UpdateDriver")]
        public async Task<IActionResult> UpdateDriver(string nip)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = null;
                string gcp_ts = string.Concat(_config["apiUrl:staging"], "/driver/v2/group-code/" + nip);
                var data = await _dvrUsecase.GetDriver(nip);
                var mapData = new DriverRequest
                {
                    nip = data.NIP,
                    group_code = data.KdGolongan2,
                    mobile_phone_number = data.NoHp
                };

                if(!string.IsNullOrEmpty(gcp_ts))
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(mapData), Encoding.UTF8, "application/json");
                    var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes("partnertimesheet:4dminP4rtnertim3sh3et"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
                    response = await client.PutAsync(gcp_ts, content);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok("Successfully update driver data");
                    }
                    else
                    {
                        var readContent = await response.Content.ReadAsStringAsync();
                        var resp = JsonConvert.SerializeObject(readContent, Formatting.Indented);
                        return BadRequest(resp);
                    }
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return NotFound(content);
                   
                }                
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("/driver/PostDriverPermit")]
        public async Task<IActionResult> PostDriverPermit(DriverTimesheetRequest request)
        {
            var driver = new DriverGbLimoRequest()
            {
                Id = request.id,
                NIP = request.nip,
                NoKontrak = request.contract_number,
                NoItem = request.item_number,
                NoDetil = request.detail_number,
                NIPPengganti = request.replacement_driver_nip,
                TanggalIzin = request.permission_date,
                Status = request.status,
            };

            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }

                var result = await _dvrUsecase.PostDriverPermit(driver);
                if (result == true)
                {
                    var json = JsonConvert.SerializeObject(result, Formatting.Indented);
                    return Ok("Insert driver permit successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Insert driver permitd failed");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
