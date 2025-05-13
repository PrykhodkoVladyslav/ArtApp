namespace Core.Picture;

public class TasksWorkIsNotCompletedException : Exception {
	public TasksWorkIsNotCompletedException() : this("Tasks work is not completed exception") { }
	public TasksWorkIsNotCompletedException(string message) : base(message) { }
}
