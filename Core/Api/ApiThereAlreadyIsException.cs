namespace Core.Api;

public class ApiThereAlreadyIsException : ApiException {
	public ApiThereAlreadyIsException() : this("API there already is exception") { }
	public ApiThereAlreadyIsException(string message) : base(message) { }
}
