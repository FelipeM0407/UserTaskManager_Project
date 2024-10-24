using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class UserTaskUseCase
    {
        private readonly IUserTaskRepository _userTaskRepository;
        private readonly IMapper _mapper;

        public UserTaskUseCase(IUserTaskRepository userTaskRepository, IMapper mapper)
        {
            _userTaskRepository = userTaskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _userTaskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserTaskDTO>>(tasks);
        }

        public async Task<UserTaskDTO> GetTaskByIdAsync(int id)
        {
            var task = await _userTaskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return _mapper.Map<UserTaskDTO>(task);
        }

        public async Task CreateTaskAsync(CreateUserTaskDTO dto)
        {
            var task = _mapper.Map<UserTask>(dto);
            await _userTaskRepository.AddAsync(task);
        }

        public async Task UpdateTaskAsync(int id, UserTaskDTO dto)
        {
            var task = await _userTaskRepository.GetByIdAsync(id);
            if (task == null) throw new Exception("Task not found");

            _mapper.Map(dto, task);
            await _userTaskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _userTaskRepository.GetByIdAsync(id);
            if (task == null) throw new Exception("Task not found");

            await _userTaskRepository.DeleteAsync(task);
        }
    }
}