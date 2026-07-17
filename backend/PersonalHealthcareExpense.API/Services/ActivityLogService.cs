using AutoMapper;
using PersonalHealthcareExpense.API.DTOs.ActivityLog;
using PersonalHealthcareExpense.API.Interfaces;
using PersonalHealthcareExpense.API.Models;

namespace PersonalHealthcareExpense.API.Services
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _repository;
        private readonly IMapper _mapper;

        public ActivityLogService(IActivityLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActivityLogDTO>> GetLastByUserIdAsync(int userId, int count)
        {
            var logs = await _repository.GetLastByUserIdAsync(userId, count);
            return _mapper.Map<IEnumerable<ActivityLogDTO>>(logs);
        }

        public async Task LogAsync(int userId, string action, string? details)
        {
            var log = new ActivityLog
            {
                UserId = userId,
                Action = action,
                Details = details,
                Timestamp = DateTime.Now
            };

            await _repository.AddAsync(log);
            await _repository.SaveAsync();
        }
    }
}
