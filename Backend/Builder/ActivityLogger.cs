using Real_Time_Chat_Application.Data;
using Real_Time_Chat_Application.Models;

namespace Real_Time_Chat_Application.Builder
{
    public class ActivityLogger
    {
        private readonly ApplicationDbContext _context;

        public ActivityLogger(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogActivity(string description, string level, string createdBy, string exception = null)
        {
            var logEntry = new ActivityStream
            {
                Description = description,
                Level = level,
                Created_By = createdBy,
                Created_On = DateTime.UtcNow,
                Exception = exception
            };

            //await _context.ActivityStream.AddAsync(logEntry);
            await _context.SaveChangesAsync();
        }
    }
}
