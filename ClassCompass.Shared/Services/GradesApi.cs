using System.Collections.Generic;
using System.Threading.Tasks;
using ClassCompass.Shared.Models;
using ClassCompass.Shared.Data;

namespace ClassCompass.Shared.Services
{
    public class GradesApi
    {
        private readonly AppDbContext _dbContext;

        // Add constructor
        public GradesApi(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Grade?> AssignGrade(Grade grade)
        {
            _dbContext.Grades.Add(grade);
            var saved = await _dbContext.SaveChangesAsync();
            if (saved > 0)
                return grade;
            return null;
        }

        public Task<List<Grade>> GetGrades(string studentId)
        {
            // Implement retrieval logic with dbContext here if needed
            return Task.FromResult(new List<Grade>());
        }
    }
}
