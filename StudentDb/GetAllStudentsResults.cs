using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using StudentDb.Models;

namespace StudentDb
{
    public class GetAllStudentsResults
    {
        private readonly StudentDbContext _context;

        public GetAllStudentsResults(StudentDbContext context)
        {
            _context = context;
        }

        [FunctionName("GetAllStudentsResults")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "results")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var results = await _context.Results
                .OrderByDescending(r => r.TotalMarks)
                .ToListAsync();

            return new OkObjectResult(results);
        }
    }
}
