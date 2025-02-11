namespace HRRS.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Dapper;
    using HRRS.Persistence.Context;
    [ApiController]
    [Route("api/[controller]")] // Base route for the controller
    public partial class DapperController : ControllerBase
    {
        private readonly DapperHelper _dbHelper;

        public DapperController(DapperHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpGet("GetAll")] // Route: api/Api/GetAll
        public IActionResult GetAll()
        {
            string query = "SELECT * FROM HospitalStandards";

            // Execute the query using Dapper
            var result = _dbHelper.Query<dynamic>(query);

            // Return the result as JSON
            return Ok(result);
        }
         [HttpGet("GetAnusuchi")] // Route: api/Api/GetAll
        public IActionResult CheckStoredProc(int anusuchi)
        {
          

            // Execute the query using Dapper
            var result = _dbHelper.QueryStoredProc<dynamic>("sp_getMapdanda", new { anusuchi = anusuchi });

            // Return the result as JSON
            return Ok(result);
        }
        //[HttpGet("GetAnusuchiByHosp")]
        //public IActionResult GetAnusuchiByHosp(int hospid)
        //{


        //    // Execute the query using Dapper
        //    var result = _dbHelper.QueryStoredProc<dynamic>("sp_getMapdanda", new { anusuchi = anusuchi });

        //    // Return the result as JSON
        //    return Ok(result);
        //}
        
        [HttpPost("HospitalStandardInsert")] // Route: api/Api/GetAll
        public IActionResult HospitalStandardInsert(HospitalStandardInsertDto dto)
        {

            var res = new
            {
                dto.MapdandaId,
                dto.HealthFacilityId,
                dto.IsAvailable,
                Has25 = dto.Has25 ? "True" : "False",
                Has50 = dto.Has50 ? "True" : "False",
                Has100 = dto.Has100 ? "True" : "False",
                Has200 = dto.Has200 ? "True" : "False",
                dto.Remarks,
                dto.FilePath,
            };

            try
            {
                // Execute the query using Dapper
                var result = _dbHelper.QueryStoredProc<dynamic>("sp_getMapdanda", res);
                return Ok(result);
            }
            catch (Exception ex)
            {

            }

            return BadRequest();

            // Return the result as JSON
        }

        public class HospitalStandardInsertDto {
            public int HealthFacilityId { get; set; }
            public int MapdandaId { get; set; }
            public bool IsAvailable { get; set; }
            public bool Has25  { get; set; }
            public bool Has50  { get; set; }
            public bool Has100  { get; set; }
            public bool Has200 { get; set; }
            public bool Remarks { get; set; }
            public string FilePath{ get; set; }
        }
    }

}
