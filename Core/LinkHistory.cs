using System.Xml;

namespace Core;

public class LinkHistory : ILinkHistory {
	protected List<string> urlList = new List<string>();
	protected int index = -1;

	public LinkHistory() { }

	public void AddToEnd(string url) {
		if (index != urlList.Count - 1)
			throw new LinkHistoryIndexIsNotAtTheEndOfHistoryException();

		urlList.Add(url);
		index++;
	}

	public string Next() {
		if (!CanNext())
			throw new ErrorLinkHistoryMoveException();

		return urlList[++index];
	}

	public string Prev() {
		if (!CanPrev())
			throw new ErrorLinkHistoryMoveException();

		return urlList[--index];
	}

	public bool CanNext() {
		return index + 1 < urlList.Count;
	}

	public bool CanPrev() {
		return index - 1 >= 0;
	}

	public bool IsCurentExist() {
		return 0 <= index && index < urlList.Count;
	}

	public string Curent() {
		return urlList[index];
	}

	public bool Contains(string url) {
		return urlList.Contains(url);
	}

	public XmlNode CreateXMLNode(in XmlDocument xmlDocument) {
		XmlNode linkHistoryNode = xmlDocument.CreateElement("LinkHistory");

		XmlNode urlListNode = xmlDocument.CreateElement("URLList");
		foreach (string url in urlList) {
			XmlNode urlNode = xmlDocument.CreateElement("URL");
			urlNode.InnerText = url;
			urlListNode.AppendChild(urlNode);
		}
		linkHistoryNode.AppendChild(urlListNode);

		XmlNode indexNode = xmlDocument.CreateElement("Index");
		indexNode.InnerText = index.ToString();
		linkHistoryNode.AppendChild(indexNode);

		return linkHistoryNode;
	}

	public void LoadDataFromXmlNode(in XmlNode node) {
		urlList.Clear();
		foreach (XmlNode urlListChild in node["URLList"].ChildNodes) {
			urlList.Add(urlListChild.InnerText);
		}

		index = Convert.ToInt32(node["Index"].InnerText);
	}
}
