namespace Core.Picture;

public class ChangePictureEventArgs : EventArgs {
	protected string newUrl;
	protected string newPath;

	public ChangePictureEventArgs(string newUrl, string newPath) {
		this.newUrl = newUrl;
		this.newPath = newPath;
	}

	public string NewUrl {
		get { return newUrl; }
	}

	public string NewPath {
		get { return newPath; }
	}
}
