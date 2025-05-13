namespace Core.Api;

public class ApiNotFoundException : ApiException {
	public ApiNotFoundException() : this("API with this name not found exception") { }
	public ApiNotFoundException(string message) : base(message) { }
}
