namespace HRRS.Controllers
{
    using Dapper;
    using HRRS.Dto.HealthStandard;
    using HRRS.Persistence.Context;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsStandardsController : ControllerBase
    {
        private readonly DapperHelper dapperHelper;

        public HospitalsStandardsController( DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
        }



        [HttpPost("bulk-insert")]
        public IActionResult BulkInsert([FromBody] List<HospitalStandardPartialDto> hospitalStandards)
        {
            if (hospitalStandards == null || hospitalStandards.Count == 0)
            {
                return BadRequest("The list cannot be empty.");
            }

            try
            {
                // Use the extension method to convert the list to a DataTable
                var tvp = hospitalStandards.ToDataTable("dbo.HospitalStandardsTVP");

                // Call the stored procedure
                dapperHelper.QueryStoredProc<object>("sp_InsertOrUpdateHospitalStandards", new { tvp = tvp.AsTableValuedParameter("dbo.HospitalStandardsTVP") });

                return Ok("Data inserted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

