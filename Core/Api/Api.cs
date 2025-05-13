namespace Core.Api;

public class Api {
	protected string name;
	protected string pattern;
	protected List<SubApi> subApis;

	public Api() {
		subApis = new List<SubApi>();
		name = "";
		pattern = "";
	}

	public string Name {
		get { return name; }
		set { name = value; }
	}
	public string Pattern {
		get { return pattern; }
		set { pattern = value; }
	}
	public List<SubApi> SubApis {
		get { return subApis; }
	}

	public void Add(string name, string url) {
		subApis.Add(
			new SubApi(this) {
				SubApiName = name,
				Url = url
			}
		);
	}
}
