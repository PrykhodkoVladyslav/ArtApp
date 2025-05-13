namespace Core.Api;

public class SubApiThereAlreadyIsException : ApiException {
	public SubApiThereAlreadyIsException() : this("SubAPI there already is exception") { }
	public SubApiThereAlreadyIsException(string message) : base(message) { }
}
