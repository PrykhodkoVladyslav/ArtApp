using System.Net;
using System.Xml;

namespace Core.Picture;

public class PictureLibrary : IPictureLibrary {
	protected Dictionary<string, string> pictures = new Dictionary<string, string>();
	protected PictureNameGenerator nameGenerator = new PictureNameGenerator();

	public PictureLibrary() { }

	public string GetLocalPathByUrl(string url) {
		if (!Contains(url)) {
			Download(url);
		}

		if (!IsValidPicturePath(pictures[url])) {
			Redownload(url);
		}

		return pictures[url];
	}

	protected bool Contains(string url) {
		return pictures.ContainsKey(url);
	}

	protected bool IsValidPicturePath(string url) {
		return File.Exists(url);
	}

	protected void Redownload(in string url) {
		pictures.Remove(url);
		Download(url);
	}

	protected void Download(in string url) {
		CreatePictureDirectoryIfNotFound();

		byte[] picture = new WebClient().DownloadData(url);
		string name = nameGenerator.CreatePictureName(GetImageFormatFromUrl(url));
		SaveByteArrayToFile(picture, name);

		pictures.Add(url, name);
	}

	protected void SaveByteArrayToFile(byte[] picture, string name) {
		using (FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write)) {
			fs.Write(picture, 0, picture.Length);
		}
	}

	protected string GetImageFormatFromUrl(in string url) {
		int index = url.LastIndexOf('.');
		return url.Substring(index, url.Length - index);
	}

	protected void CreatePictureDirectoryIfNotFound() {
		DirectoryInfo directory = new DirectoryInfo(Environment.CurrentDirectory + "\\" + nameGenerator.Path);
		if (!directory.Exists)
			directory.Create();
	}

	public XmlNode CreateXMLNode(in XmlDocument xmlDocument) {
		XmlNode pictureLibraryNode = xmlDocument.CreateElement("PictureLibrary");

		XmlNode picturesNode = xmlDocument.CreateElement("Pictures");
		foreach (KeyValuePair<string, string> picturePair in pictures) {
			XmlNode pictureNode = xmlDocument.CreateElement("Picture");

			XmlNode urlNode = xmlDocument.CreateElement("URL");
			urlNode.InnerText = picturePair.Key;
			pictureNode.AppendChild(urlNode);

			XmlNode pathNode = xmlDocument.CreateElement("Path");
			pathNode.InnerText = picturePair.Value;
			pictureNode.AppendChild(pathNode);

			picturesNode.AppendChild(pictureNode);
		}
		pictureLibraryNode.AppendChild(picturesNode);

		pictureLibraryNode.AppendChild(nameGenerator.CreateXMLNode(xmlDocument));

		return pictureLibraryNode;
	}

	public void LoadDataFromXmlNode(in XmlNode node) {
		pictures.Clear();
		foreach (XmlNode picturesChild in node["Pictures"].ChildNodes) {
			pictures.Add(picturesChild["URL"].InnerText, picturesChild["Path"].InnerText);
		}

		nameGenerator.LoadDataFromXmlNode(node["PictureNameGenerator"]);
	}
}
