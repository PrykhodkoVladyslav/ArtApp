namespace Core.Api;

public class SubApi {
	protected string name;
	protected Api parent;
	protected string url;

	public SubApi(Api parent) {
		name = "";
		this.parent = parent;
		url = "";
	}

	public string SubApiName {
		get { return name; }
		set { name = value; }
	}

	public string ApiName {
		get { return parent.Name; }
	}

	public string Pattern {
		get { return parent.Pattern; }
	}

	public string Url {
		get { return url; }
		set { url = value; }
	}
}
