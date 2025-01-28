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
         [HttpGet("StoredProc")] // Route: api/Api/GetAll
        public IActionResult CheckStoredProc(int anusuchi)
        {
          

            // Execute the query using Dapper
            var result = _dbHelper.QueryStoredProc<dynamic>("sp_getMapdanda", new { anusuchi = anusuchi });

            // Return the result as JSON
            return Ok(result);
        }
    }

}
