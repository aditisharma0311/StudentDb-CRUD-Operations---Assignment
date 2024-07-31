using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StudentDb.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentDb
{
    public class GetStudentMarks
    {
        private readonly StudentDbContext _context;

        public GetStudentMarks(StudentDbContext context)
        {
            _context = context;
        }

        [FunctionName("GetStudentMarks")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var rollNo = JsonConvert.DeserializeObject<int>(requestBody);

            var result = await _context.Results
                .FirstOrDefaultAsync(r => r.RollNo == rollNo);

            if (result == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(result);
        }
    }
}
