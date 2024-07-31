using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StudentDb.Models;

namespace StudentDb
{
    public class AddStudent
    {
        private readonly StudentDbContext _context;

        public AddStudent(StudentDbContext context)
        {
            _context = context;
        }

        [FunctionName("AddStudent")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var student = JsonConvert.DeserializeObject<Students>(requestBody);

            if (student != null)
            {
                // Ensure RollNo is not set in the incoming request
                student.RollNo = 0;

                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }

            return new OkObjectResult(student);
        }
    }
}
