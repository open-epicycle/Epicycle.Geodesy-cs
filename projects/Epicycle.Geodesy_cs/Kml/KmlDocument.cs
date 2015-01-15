using Epicycle.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Epicycle.Geodesy.Kml
{
    public sealed class KmlDocument
    {
        public KmlDocument()
        {
            _elements = new List<IKmlElement>();
        }

        private readonly IList<IKmlElement> _elements;

        public void AddSegment(GeoPoint3 start, GeoPoint3 end, Color4b? colour = null)
        {
            _elements.Add(new SegmentKmlElement(start, end, colour));
        }

        public string Serialize()
        {
            var document = new XElement("Document");

            foreach (var element in _elements)
            {
                document.Add(element.ToXml());
            }

            var kmlRoot = new XElement("kml", document);

            var stringWriter = new StringWriter();
            kmlRoot.Save(stringWriter);

            return stringWriter.ToString();
        }
    }
}
