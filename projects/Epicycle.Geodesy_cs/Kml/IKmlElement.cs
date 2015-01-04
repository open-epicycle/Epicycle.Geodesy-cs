using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Epicycle.Geodesy.Kml
{
    internal interface IKmlElement
    {
        XElement ToXml();
    }
}
