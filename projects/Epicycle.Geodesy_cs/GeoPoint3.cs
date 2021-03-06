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

namespace Epicycle.Geodesy
{
    using System;

    public sealed class GeoPoint3
    {
        public GeoPoint3(GeoPoint2 ll, double altitude = 0)
        {
            _latitude = ll.Latitude;
            _longitude = ll.Longitude;
            _altitude = altitude;
        }

        public GeoPoint3(double latitude_rad, double longitude_rad, double altitude)
        {
            _latitude = latitude_rad;
            _longitude = longitude_rad;
            _altitude = altitude;
        }

        public GeoPoint3(SexagesimalAngle latitude, SexagesimalAngle longitude, double altitude)
        {
            _latitude = latitude.InRadians;
            _longitude = longitude.InRadians;
            _altitude = altitude;
        }

        private readonly double _latitude; // in radians
        private readonly double _longitude; // in radians
        private readonly double _altitude;

        public static implicit operator GeoPoint3(GeoPoint2 point2)
        {
            return new GeoPoint3(point2);
        }

        public double Latitude // in radians
        {
            get { return _latitude; }
        }

        public double Latitude_deg
        {
            get { return BasicMath.RadToDeg(_latitude); }
        }

        public double Longitude // in radians
        {
            get { return _longitude; }
        }

        public double Longtitude_deg
        {
            get { return BasicMath.RadToDeg(_longitude); }
        }

        public double Altitude
        {
            get { return _altitude; }
        }

        public GeoPoint2 LatLon
        {
            get { return new GeoPoint2(Latitude, Longitude); }
        }

        public override string ToString()
        {
            return string.Format("({0}N, {1}E, {2})", Latitude_deg, Longtitude_deg, Altitude);
        }

        public static GeoPoint3 InDegrees(double latitude_deg, double longitude_deg, double altitude)
        {
            return new GeoPoint3(BasicMath.DegToRad(latitude_deg), BasicMath.DegToRad(longitude_deg), altitude);
        }

        public static GeoPoint3 NorthPole
        {
            get { return new GeoPoint3(latitude_rad: Math.PI / 2, longitude_rad: 0, altitude: 0); }
        }

        public static GeoPoint3 SouthPole
        {
            get { return new GeoPoint3(latitude_rad: -Math.PI / 2, longitude_rad: 0, altitude: 0); }
        }
    }
}
