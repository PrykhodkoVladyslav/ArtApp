using Core.Web;
using System.Net;
using System.Xml;

namespace Core.Picture;

public class PictureController : IPictureController {
	protected ILinkHistory linkHistory = new LinkHistory();
	protected IPictureLibrary library = new PictureLibrary();

	protected string source = "";
	protected string regExPattern = "";

	protected int threadsCounter = 0;

	public event EventHandler<ChangePictureEventArgs>? PictureChanged;

	public event EventHandler<EventArgs>? OnWebException;

	protected delegate void MoveMethod();

	public PictureController() { }

	public string Source {
		get { return source; }
		set { source = value ?? throw new ArgumentNullException(nameof(value)); }
	}

	public string RegExPattern {
		get { return regExPattern; }
		set { regExPattern = value ?? throw new ArgumentNullException(nameof(value)); }
	}

	public bool IsProcessingInAnotherThreads {
		get { return threadsCounter != 0; }
	}


	protected void ChangePicture(string url) {
		PictureChanged?.Invoke(this, new ChangePictureEventArgs(url, library.GetLocalPathByUrl(url)));
	}

	protected void LoadPictureFromApi() {
		string url = WebLoad.GetPictureUrlFromApi(source, regExPattern);
		ChangePicture(url);
		linkHistory.AddToEnd(url);
	}

	public void LoadNext() => LoadPicture(LoadNextPictureMethod);
	public void LoadPrev() => LoadPicture(LoadPrevPictureMethod);

	protected void LoadPicture(MoveMethod moveMethod) {
		Task.Run(() => LoadPictureUsingAnotherThread(moveMethod));
	}

	protected void LoadPictureUsingAnotherThread(MoveMethod moveMethod) {
		Interlocked.Increment(ref threadsCounter);
		try {
			lock (this) { LoadPictureWithExceptionHandling(moveMethod); }
		}
		finally {
			Interlocked.Decrement(ref threadsCounter);
		}
	}

	protected void LoadPictureWithExceptionHandling(MoveMethod moveMethod) {
		try {
			moveMethod();
		}
		catch (WebException) {
			OnWebException?.Invoke(this, EventArgs.Empty);
		}
	}

	protected void LoadNextPictureMethod() {
		if (linkHistory.CanNext()) {
			ChangePicture(linkHistory.Next());
		}
		else {
			LoadPictureFromApi();
		}
	}

	protected void LoadPrevPictureMethod() {
		if (linkHistory.CanPrev()) {
			ChangePicture(linkHistory.Prev());
		}
	}

	public XmlNode CreateXMLNode(in XmlDocument xmlDocument) {
		if (IsProcessingInAnotherThreads)
			throw new TasksWorkIsNotCompletedException();

		XmlNode pictureControllerNode = xmlDocument.CreateElement("PictureController");

		pictureControllerNode.AppendChild(linkHistory.CreateXMLNode(xmlDocument));
		pictureControllerNode.AppendChild(library.CreateXMLNode(xmlDocument));

		XmlNode sourceNode = xmlDocument.CreateElement("Source");
		sourceNode.InnerText = source;
		pictureControllerNode.AppendChild(sourceNode);

		XmlNode regExPatternNode = xmlDocument.CreateElement("RegExPattern");
		regExPatternNode.InnerText = regExPattern;
		pictureControllerNode.AppendChild(regExPatternNode);

		return pictureControllerNode;
	}

	public void LoadDataFromXmlNode(in XmlNode node) {
		linkHistory.LoadDataFromXmlNode(node["LinkHistory"]);
		library.LoadDataFromXmlNode(node["PictureLibrary"]);
		source = node["Source"].InnerText;
		regExPattern = node["RegExPattern"].InnerText;
	}
}
