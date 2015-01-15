﻿// AUTOGENERATED, DO NOT MODIFY

// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Geodesy-cs
// ]]]]

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
