namespace CLassLIbrary.DTO
{
    public record CreateTaskDTO(string Title, string Description, DateTime Deadline, int Priority);
    public record EditTaskDTO(int ID, string? Title, string Description, DateTime Deadline, int Priority);
}
