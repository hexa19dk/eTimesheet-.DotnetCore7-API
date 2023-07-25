using e_TimesheetNET7.Models.SIO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace e_TimesheetNET7.Controllers
{
    public class SioController : ControllerBase
    {
        private readonly IConfiguration _config;
        public SioController(IConfiguration config) 
        {
            _config = config;
        }

        [HttpGet]
        public SIO MapSio(SIOLimo limo)
        {
            var mapData = new SIO()
            {
                sio_year = limo.SioYear,
                sio_no = limo.SioNo,
                sio_pool = limo.SioPool,
                sio_sts = limo.SioSts,
                sio_area = limo.Area,
                driver_nip = limo.DriverCode,
                driver_name = limo.DriverName,
                vehicle_code = limo.VehicleCode,
                start_date = limo.StartDate,
                end_date = limo.FinishDate,
                start_km = limo.StartKm,
                finish_km = limo.FinishKm,
                liter_bbm = limo.LiterBBM,
                //kontrak_bbm = limo.
                pool_destination = limo.PoolDestination,
                showed = limo.Showed,
                type_sio = limo.TipeSIO,
                description = limo.Keterangan
            };

            return mapData;
        }

        [HttpPost("/post/sio")]
        public async Task<IActionResult> PostSio([FromBody] SIOLimo limo)
        {
            HttpClient client = new HttpClient();
            string endpoint = string.Concat(_config["apiUrl:staging"],"/driver/v2/sio");
            HttpResponseMessage response = null;
            var respDt = MapSio(limo);

            try
            {
                if (respDt != null)
                {
                    if (!string.IsNullOrEmpty(endpoint))
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(respDt), Encoding.UTF8, "application/json");
                        var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes("partnertimesheet:4dminP4rtnertim3sh3et"));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
                        response = await client.PostAsync(endpoint, content);
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(respDt);
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var resp = JsonConvert.SerializeObject(content, Formatting.Indented);
                        return BadRequest(resp);
                    }
                }
                else
                {
                    return BadRequest("SIO not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
