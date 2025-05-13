using System.Xml;

namespace Core.Picture;

public class PictureNameGenerator : IPictureNameGenerator {
	protected string path = "picture\\";
	protected int index = 0;

	public PictureNameGenerator() { }

	public string Path {
		get { return path; }
		set { path = value ?? throw new ArgumentNullException(nameof(value)); }
	}
	public int Index {
		get { return index; }
	}

	public string CreatePictureName(in string pictureExtension) {
		string name = path + index.ToString() + pictureExtension;
		index++;
		return name;
	}
	public XmlNode CreateXMLNode(in XmlDocument xmlDocument) {
		XmlNode pictureNameGeneratorNode = xmlDocument.CreateElement("PictureNameGenerator");

		XmlNode pathNode = xmlDocument.CreateElement("Path");
		pathNode.InnerText = path;
		pictureNameGeneratorNode.AppendChild(pathNode);

		XmlNode indexNode = xmlDocument.CreateElement("Index");
		indexNode.InnerText = index.ToString();
		pictureNameGeneratorNode.AppendChild(indexNode);

		return pictureNameGeneratorNode;
	}

	public void LoadDataFromXmlNode(in XmlNode node) {
		path = node["Path"].InnerText;
		index = Convert.ToInt32(node["Index"].InnerText);
	}
}
