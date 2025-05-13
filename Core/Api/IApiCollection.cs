namespace Core.Api;

public interface IApiCollection {
	SubApi this[int index] { get; }

	List<SubApi> SubApis { get; }

	void AddApi(string name, string pattern);
	void AddSubApi(string apiName, string subApiName, string url);
}
