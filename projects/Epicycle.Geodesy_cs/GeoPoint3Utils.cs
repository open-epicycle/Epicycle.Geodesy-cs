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

using Epicycle.Commons;
using Epicycle.Math.Geometry;

namespace Epicycle.Geodesy
{
    using System;

    public static class GeoPoint3Utils
    {
        public static Vector3 ToEcef(this GeoPoint3 point, GeoDatum datum)
        {
            var a = datum.Semimajor;
            var e = datum.Eccentricity;

            var theta = point.Latitude;
            var phi = point.Longitude;
            var h = point.Altitude;

            var sinTheta = Math.Sin(theta);
            var cosTheta = Math.Cos(theta);
            var sinPhi = Math.Sin(phi);
            var cosPhi = Math.Cos(phi);

            var n = a / Math.Sqrt(1 - BasicMath.Sqr(e * sinTheta)); // distance from ellipsoid point to z-axis along ellipsoid normal

            var xy = (n + h) * cosTheta;

            var x = xy * cosPhi;
            var y = xy * sinPhi;
            var z = (n * (1 - e * e) + h) * sinTheta;

            return new Vector3(x, y, z);
        }

        public static Vector3 EcefUp(this GeoPoint3 point)
        {
            var theta = point.Latitude;
            var phi = point.Longitude;

            var sinTheta = Math.Sin(theta);
            var cosTheta = Math.Cos(theta);
            var sinPhi = Math.Sin(phi);
            var cosPhi = Math.Cos(phi);

            return new Vector3(cosTheta * cosPhi, cosTheta * sinPhi, sinTheta);
        }

        public static RotoTranslation3 Enu(this GeoPoint3 origin, GeoDatum datum)
        {
            var ecefOrigin = origin.ToEcef(datum);

            var z = origin.EcefUp();
            var y = Vector3.UnitZ - z.Z * z;
            var x = y.Cross(z);

            return new RotoTranslation3(Rotation3Utils.GramSchmidt(x, y), ecefOrigin);
        }

        public static GeoPoint3 EcefToGeodetic(Vector3 ecef, GeoDatum datum)
        {
            const double precision2 = 1e-6;
            const int maxIterations = 16;

            var norm = ecef.Norm;

            var phi = Math.Atan2(ecef.Y, ecef.X);
            var theta0 = BasicMath.Asin(ecef.Z / ecef.Norm); // first approximation            
            var h0 = norm - datum.Semiminor; // first approximation

            var theta = theta0;
            var h = h0;

            var error2 = double.PositiveInfinity;

            for (var i = 0; i < maxIterations && error2 > precision2; i++)
            {
                var ecefPrime = new GeoPoint3(theta, phi, h).ToEcef(datum);

                var normPrime = ecefPrime.Norm;
                var thetaSphere = BasicMath.Asin(ecefPrime.Z / normPrime);
                var hSphere = normPrime - datum.Semiminor;

                theta += theta0 - thetaSphere;
                h += h0 - hSphere;

                error2 = Vector3.Distance2(ecef, ecefPrime);
            }

            return new GeoPoint3(theta, phi, h);
        }

        public sealed class YamlSerialization
        {
            public double Lat { get; set; }
            public double Long { get; set; }
            public double Alt { get; set; }

            public YamlSerialization() { }

            public YamlSerialization(GeoPoint3 geoPoint3)
            {
                Lat = geoPoint3.Latitude_deg;
                Long = geoPoint3.Longtitude_deg;
                Alt = geoPoint3.Altitude;
            }

            public GeoPoint3 Deserialize()
            {
                return GeoPoint3.InDegrees(Lat, Long, Alt);
            }
        }
    }
}
