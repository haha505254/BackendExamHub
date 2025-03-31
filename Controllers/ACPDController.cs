using BackendExamHub.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BackendExamHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ACPDController : ControllerBase
    {
        private readonly SqlDbService _dbService;

        public ACPDController(SqlDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JsonElement requestBody)
        {
            string jsonRequest = requestBody.GetRawText();
            string? result = await _dbService.ExecuteStoredProcedureAsync("usp_ACPD_Create", jsonRequest);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string jsonRequest = JsonSerializer.Serialize(new { userSID = id });
            string? result = await _dbService.ExecuteStoredProcedureAsync("usp_ACPD_Read", jsonRequest);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] JsonElement requestBody)
        {
            string jsonRequest = requestBody.GetRawText();
            string? result = await _dbService.ExecuteStoredProcedureAsync("usp_ACPD_Update", jsonRequest);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string jsonRequest = JsonSerializer.Serialize(new { userSID = id });
            string? result = await _dbService.ExecuteStoredProcedureAsync("usp_ACPD_Delete", jsonRequest);
            return Ok(result);
        }
    }
}