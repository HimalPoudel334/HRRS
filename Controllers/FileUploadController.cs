namespace HRRS.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        // POST: api/FileUpload
        [HttpPost("upload")]
        [Consumes("multipart/form-data")] // ✅ This tells Swagger to allow file uploads
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or file is empty.");
            }

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { message = "File uploaded successfully!", fileName = file.FileName });
        }

    }
}

