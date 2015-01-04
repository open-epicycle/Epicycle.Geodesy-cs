using Epicycle.Commons;

namespace Epicycle.Geodesy
{
    using System;

    public class GeoPoint2
    {
        public GeoPoint2(double latitude_rad, double longitude_rad)
        {
            _latitude = latitude_rad;
            _longitude = longitude_rad;
        }

        public GeoPoint2(SexagesimalAngle latitude, SexagesimalAngle longitude)
        {
            _latitude = latitude.InRadians;
            _longitude = longitude.InRadians;
        }

        private readonly double _latitude; // in radians
        private readonly double _longitude; // in radians

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

        public override string ToString()
        {
            return string.Format("({0}N, {1}E)", Latitude_deg, Longtitude_deg);
        }

        public static GeoPoint2 InDegrees(double latitude_deg, double longitude_deg)
        {
            return new GeoPoint2(BasicMath.DegToRad(latitude_deg), BasicMath.DegToRad(longitude_deg));
        }

        public static GeoPoint2 NorthPole
        {
            get { return new GeoPoint2(latitude_rad: Math.PI / 2, longitude_rad: 0); }
        }

        public static GeoPoint2 SouthPole
        {
            get { return new GeoPoint2(latitude_rad: -Math.PI / 2, longitude_rad: 0); }
        }
    }
}

