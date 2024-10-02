using CLassLIbrary.DTO;

namespace taskMeneger;

public class TaskService1
{
    public class TaskService(ITaskRespository taskRepository) : ITaskService
    {
        public async Task AddTask(CreateTaskDTO addTaskDTO)
        {
            var taskInfo = new TaskInfo()
            {
                Title = addTaskDTO.Title,
                Description = addTaskDTO.Description,
                Deadline = addTaskDTO.Deadline,
                Priority = addTaskDTO.Priority
            };
            taskRepository.CreateTask(taskInfo);
            await Task.CompletedTask;
        }

        public async Task EditTask(EditTaskDTO editTaskDTO)
        {
            var existingTask = await taskRepository.GetById(editTaskDTO.ID);
            if (existingTask == null)
            {
                throw new Exception("Zadanie nie zostało znalezione.");
            }
            existingTask.Title = editTaskDTO.Title;
            existingTask.Description = editTaskDTO.Description;
            existingTask.Deadline = editTaskDTO.Deadline;
            existingTask.Priority = editTaskDTO.Priority;
            await taskRepository.UpdateTaskAsync(existingTask);
        }

        public List<TaskInfo> GetAllTasks()
        {
            return taskRepository.GetAll();
        }

        public async Task RemoveTask(int id)
        {
            var tasks = taskRepository.GetAll();
            var taskToDelete = tasks.FirstOrDefault(t => t.Id == id);
            if (taskToDelete == null)
            {
                throw new Exception("Zadanie nie zostało znalezione.");
            }

            await taskRepository.DeleteTaskAsync(tasks, id);
        }
    }
}