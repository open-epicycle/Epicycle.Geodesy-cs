using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Epicycle.Graphics;

namespace Epicycle.Geodesy.Kml
{
    internal sealed class SegmentKmlElement : IKmlElement
    {
        public SegmentKmlElement(GeoPoint3 start, GeoPoint3 end, Color4b? colour = null)
        {
            _start = start;
            _end = end;
            _colour = colour;
        }

        private readonly GeoPoint3 _start;
        private readonly GeoPoint3 _end;
        private readonly Color4b? _colour;

        public XElement ToXml()
        {
            var coordText = string.Format("{0},{1},{2} {3},{4},{5}",
                _start.Longtitude_deg, _start.Latitude_deg, _start.Altitude,
                _end.Longtitude_deg, _end.Latitude_deg, _end.Altitude);

            var xml = 
                new XElement("Placemark", 
                    new XElement("LineString",
                        new XElement("coordinates", coordText)
                    )
                );

            if (_colour != null)
            {
                var colourText = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", _colour.Value.A, _colour.Value.B, _colour.Value.G, _colour.Value.R);

                xml.Add(
                    new XElement("Style",
                        new XElement("LineStyle",
                            new XElement("color", colourText)
                            )
                        )
                    );
            }

            return xml;
        }
    }
}
