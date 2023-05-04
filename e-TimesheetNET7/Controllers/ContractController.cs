using e_TimesheetNET7.Usecase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using static System.Net.WebRequestMethods;

namespace e_TimesheetNET7.Controllers
{
    [Route("/api/contracts")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractUsecase _ctrUsecase;
        public ContractController(IContractUsecase ctrUsecase) 
        {
            _ctrUsecase = ctrUsecase;
        }

        [HttpGet("/ContractHeader/{contractNo}")]
        public async Task<ActionResult> GetHeader(string contractNo)
        {
            try
            {
                var result = await _ctrUsecase.GetHeader(contractNo);
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

        [HttpGet("/ContractData/{contractNo}")]
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
        public async Task<ActionResult> PostContract(string contractNo)
        {
            string gcp_ts = "https://stgapi-corptimesheet.bluebird.id";
            string endpoint = string.Concat(gcp_ts, "/fok/receiver/v1/contract");
            HttpResponseMessage response = null;
            var data = await _ctrUsecase.GetContract(contractNo);

            try
            {
                if (data.Header != null && data.Detail != null && data.DetailDetail != null)
                {
                    if (!string.IsNullOrEmpty(gcp_ts))
                    {
                        using (var client = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(data));
                            response = await client.PostAsync(endpoint, content);
                        }
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return Ok(data);
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
                    var content = await response.Content.ReadAsStringAsync();
                    return BadRequest(content);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
