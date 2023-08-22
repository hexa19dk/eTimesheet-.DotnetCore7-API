using e_TimesheetNET7.Usecase;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace e_TimesheetNET7.Controllers
{
    [Route("/api/contracts")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractUsecase _ctrUsecase;
        private readonly IConfiguration _config;
        public ContractController(IContractUsecase ctrUsecase, IConfiguration config) 
        {
            _ctrUsecase = ctrUsecase;
            _config = config;
        }

        [HttpGet("/GetContractData/{contractNo}")]
        public async Task<ActionResult> GetContract(string contractNo)
        {
            try
            {
                var result = await _ctrUsecase.GetContract(contractNo);
                if (result == null)
                {
                    return BadRequest("Not found");
                }
                var json = JsonConvert.SerializeObject(result, Formatting.Indented);

                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("/PostContract")]
        public async Task<ActionResult> PostContract(List<string> contractNo)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = null;
                string gcp_ts = string.Concat(_config["apiUrl:staging"],"/fok/receiver/v2/contract");

                foreach (var noKontrak in contractNo)
                {
                    var data = await _ctrUsecase.GetContract(noKontrak);
                    var json = JsonConvert.SerializeObject(data);

                    if (data.Header != null && data.Detail != null && data.DetailDetail != null)
                    {
                        if (!string.IsNullOrEmpty(gcp_ts))
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                            var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes("partnertimesheet:4dminP4rtnertim3sh3et"));
                            //var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes("Y0dGeWRHNWxjblJwYldWemFHVmxkQT09:NGRtaW5QNHJ0bmVydGltM3NoM2V0"));
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
                            response = await client.PostAsync(gcp_ts, content);
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status404NotFound);
                        }                       
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return BadRequest(content);
                    }
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    //return Ok(data);
                    return Ok("Successfully push contract data");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var resp = JsonConvert.SerializeObject(content, Formatting.Indented);
                    return BadRequest(resp);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
