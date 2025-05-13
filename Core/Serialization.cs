using System.Xml;

namespace Core;

public interface IWithSerialization {
	XmlNode CreateXMLNode(in XmlDocument xmlDocument);
	void LoadDataFromXmlNode(in XmlNode node);
}
