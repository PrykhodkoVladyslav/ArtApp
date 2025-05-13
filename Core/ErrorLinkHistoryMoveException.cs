namespace Core;

public class ErrorLinkHistoryMoveException : Exception {
	public ErrorLinkHistoryMoveException() : this("Error LinkHistory move exception") { }
	public ErrorLinkHistoryMoveException(string message) : base(message) { }
}
