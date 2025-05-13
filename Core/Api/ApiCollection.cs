namespace Core.Api;

public class ApiCollection : IApiCollection {
	protected List<Api> apis;

	public ApiCollection() {
		apis = new List<Api>();
	}

	public void AddApi(string name, string pattern) {
		if (apis.Any(api => api.Name.Equals(name))) {
			throw new ApiThereAlreadyIsException();
		}

		apis.Add(
			new Api { Name = name, Pattern = pattern }
		);
	}

	public void AddSubApi(string apiName, string subApiName, string url) {
		if (!apis.Any(x => x.Name.Equals(apiName))) {
			throw new ApiNotFoundException();
		}

		Api api = apis.First(x => x.Name.Equals(apiName));

		if (api.SubApis.Any(x => x.SubApiName.Equals(subApiName))) {
			throw new SubApiThereAlreadyIsException();
		}

		api.Add(subApiName, url);
	}

	public List<SubApi> SubApis {
		get {
			List<SubApi> subApi = new List<SubApi>();
			foreach (var item in apis) {
				subApi.AddRange(item.SubApis);
			}

			return subApi;
		}
	}

	public SubApi this[int index] {
		get { return SubApis[index]; }
	}
}
