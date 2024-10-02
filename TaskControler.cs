using System.Globalization;
using CLassLIbrary.DTO;

namespace taskMeneger;

public class TaskControler
{
        public class TaskController(ITaskService taskService)
        {
            public async Task AddTask()
        {
            Console.Write("Podaj tytuł: ");
            var title = Console.ReadLine();
            Console.Write("Podaj opis: ");
            var description = Console.ReadLine();
            Console.Write("Podaj termin (yyyy-MM-dd): ");
            var deadlineInput = Console.ReadLine();
            DateTime deadline;

            if (string.IsNullOrWhiteSpace(title))
            {
                await Console.Out.WriteLineAsync("Tytul nie moze byc pusty");
            }

            if (!DateTime.TryParseExact(deadlineInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out deadline))
            {
                Console.WriteLine("Nieprawidłowy format daty. Użyj formatu yyyy-MM-dd.");
                return;
            }

            Console.Write("Podaj priorytet (1-5): ");
            int.TryParse(Console.ReadLine(), out var priority);

            var taskDTO = new CreateTaskDTO(title!, description ?? string.Empty, deadline, priority);

            await taskService.AddTask(taskDTO);
            Console.WriteLine("Zadanie dodane!");
        }

        public async Task RemoveTask()
        {
            Console.Write("Podaj ID zadania do usunięcia: ");
            _ = !int.TryParse(Console.ReadLine(), out var id);

            await taskService.RemoveTask(id);
        }

        public async Task EditTask()
        {
            Console.Write("Podaj ID do edycji: ");
            _ = !int.TryParse(Console.ReadLine(), out var id);

            var existingTask = taskService.GetAllTasks().FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                Console.WriteLine("Zadanie nie zostało znalezione.");
                return;
            }

            Console.Write("Podaj nowy tytuł (lub pozostaw pusty, aby nie zmieniać): ");
            var title = Console.ReadLine();
            Console.Write("Podaj nowy opis (lub pozostaw pusty, aby nie zmieniać): ");
            var description = Console.ReadLine();
            Console.Write("Podaj nowy termin (yyyy-MM-dd, lub pozostaw pusty, aby nie zmieniać): ");
            var deadlineInput = Console.ReadLine();
            DateTime? deadline = string.IsNullOrEmpty(deadlineInput) ? (DateTime?)null : DateTime.Parse(deadlineInput);
            Console.Write("Podaj nowy priorytet (1-5, lub pozostaw pusty, aby nie zmieniać): ");
            var priorityInput = Console.ReadLine();
            int? priority = string.IsNullOrEmpty(priorityInput) ? (int?)null : int.Parse(priorityInput);

            EditTaskDTO editTaskDTO = new EditTaskDTO(
                id,
                title!,
                description ?? string.Empty,
                deadline ?? existingTask.Deadline,
                priority ?? existingTask.Priority
                );
            await taskService.EditTask(editTaskDTO);

            Console.WriteLine("Zadanie zaktualizowane.");
        }

        public void DisplayTask()
        {
            var tasks = taskService.GetAllTasks();
            foreach (var task in tasks)
            {
                Console.WriteLine($"ID: {task.Id}\nPriorytet: {task.Priority}\nTytuł: {task.Title}\nOpis: {task.Description}\nTermin: {task.Deadline.ToShortDateString()}\n");
            }
        }

        public void Choosing()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("1 - Dodaj zadanie\n2 - Usuń zadanie\n3 - Edytuj zadanie\n4 - Wyświetl zadania\n5 - Zakończ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask().Wait();
                        break;
                    case "2":
                        RemoveTask().Wait();
                        break;
                    case "3":
                        EditTask().Wait();
                        break;
                    case "4":
                        DisplayTask();
                        break;
                    case "5":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        break;
                }
            }
        }
    }
}
