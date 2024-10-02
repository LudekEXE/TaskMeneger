﻿using CLassLIbrary.DTO;

namespace taskMeneger;

public interface ITaskRespository
{
    List<TaskInfo> GetAll();
    void CreateTask(TaskInfo task);

    Task<TaskInfo?>GetById(int id);
    Task DeleteTaskAsync(List<TaskInfo> task, int id);
    Task UpdateTaskAsync(TaskInfo task);
}
public interface ITaskService
{
    Task AddTask(CreateTaskDTO addTaskDTO);
    Task EditTask(EditTaskDTO editTaskDTO);
    Task RemoveTask(int id);  // Usuwanie po ID, bez potrzeby przekazywania TaskInfo
    List<TaskInfo> GetAllTasks();
}