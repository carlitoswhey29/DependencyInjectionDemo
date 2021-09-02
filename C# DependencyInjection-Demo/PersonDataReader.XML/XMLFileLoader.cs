using System.Xml.Linq;

namespace PersonDataReader.XML
{
    public interface IXMLFileLoader
    {
        XElement LoadFile();
    }

    public class XMLFileLoader : IXMLFileLoader
    {
        private string _filePath;

        public XMLFileLoader(string filePath)
        {
            _filePath = filePath;
        }

        public XElement LoadFile()
        {
            return XElement.Load(_filePath);
        }
    }
}
