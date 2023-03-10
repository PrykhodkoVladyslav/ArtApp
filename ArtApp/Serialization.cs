using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace ArtApp {
	public interface IWithSerialization {
		XmlNode CreateXMLNode(in XmlDocument xmlDocument);
		void LoadDataFromXmlNode(in XmlNode node);
	}

	public partial class PictureController {
		public XmlNode CreateXMLNode(in XmlDocument xmlDocument) {
			if (IsProcessingInAnotherThreads)
				throw new TreadsWordIsNotCompletedException();

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

	public partial class LinkHistory {
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

	public partial class PictureLibrary {
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

	public partial class PictureNameGenerator {
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
}
