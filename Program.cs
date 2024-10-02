
using Microsoft.Extensions.DependencyInjection;
using taskMeneger;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ITaskRespository, JsonClassRepository>()
    .AddSingleton<ITaskService, TaskService1.TaskService>()
    .AddSingleton<TaskControler.TaskController>()
    .BuildServiceProvider();

var taskController = serviceProvider.GetRequiredService<TaskControler.TaskController>();
taskController.Choosing();