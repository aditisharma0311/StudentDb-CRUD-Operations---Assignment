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
    public class AddOrUpdateStudentMarks
    {
        private readonly StudentDbContext _context;

        public AddOrUpdateStudentMarks(StudentDbContext context)
        {
            _context = context;
        }

        [FunctionName("AddOrUpdateStudentMarks")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var studentResult = JsonConvert.DeserializeObject<Result>(requestBody);

            if (studentResult != null)
            {
                // Check if the student already has marks
                var existingResult = await _context.Results
                    .FirstOrDefaultAsync(r => r.RollNo == studentResult.RollNo);

                if (existingResult != null)
                {
                    // Update existing marks
                    existingResult.Hindi = studentResult.Hindi;
                    existingResult.English = studentResult.English;
                    existingResult.Science = studentResult.Science;
                    existingResult.History = studentResult.History;
                    existingResult.GK = studentResult.GK;
                }
                else
                {
                    // Add new marks
                    _context.Results.Add(studentResult);
                }

                await _context.SaveChangesAsync();
            }

            return new OkObjectResult(studentResult);
        }
    }
}
