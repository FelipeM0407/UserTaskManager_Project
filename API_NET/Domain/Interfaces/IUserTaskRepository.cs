using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserTaskRepository
    {
        Task<UserTask> GetByIdAsync(int id);
        Task<IEnumerable<UserTask>> GetAllAsync();
        Task AddAsync(UserTask userTask);
        Task UpdateAsync(UserTask userTask);
        Task DeleteAsync(UserTask userTask);
    }
}