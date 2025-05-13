namespace Core;

public class LinkHistoryIndexIsNotAtTheEndOfHistoryException : Exception {
	public LinkHistoryIndexIsNotAtTheEndOfHistoryException() : this("LinkHistory index is not at the end of history exception") { }
	public LinkHistoryIndexIsNotAtTheEndOfHistoryException(string message) : base(message) { }
}
