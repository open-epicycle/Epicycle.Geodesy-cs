using System.Xml.Linq;

namespace Epicycle.Geodesy.Kml
{
    internal interface IKmlElement
    {
        XElement ToXml();
    }
}
