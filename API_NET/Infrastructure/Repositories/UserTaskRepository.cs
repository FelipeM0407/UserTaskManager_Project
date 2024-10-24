using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserTaskRepository : IUserTaskRepository
    {
        private readonly ApplicationDbContext _context;

        public UserTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserTask> GetByIdAsync(int id)
        {
            return await _context.UserTasks.FindAsync(id);
        }

        public async Task<IEnumerable<UserTask>> GetAllAsync()
        {
            return await _context.UserTasks.ToListAsync();
        }

        public async Task AddAsync(UserTask task)
        {
            await _context.UserTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserTask task)
        {
            _context.UserTasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserTask task)
        {
            _context.UserTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}