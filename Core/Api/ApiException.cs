namespace Core.Api;

public abstract class ApiException : Exception {
	protected ApiException() : this("API exception") { }
	protected ApiException(string message) : base(message) { }
}
