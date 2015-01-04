using Epicycle.Commons;

namespace Epicycle.Geodesy
{
    using System;

    public sealed class GeoDatum
    {
        public GeoDatum(double semimajor, double eccentricity)
        {
            _a = semimajor;
            _e = eccentricity;
        }

        private readonly double _a; // half the size of the major axis
        private readonly double _e; // eccentricity

        // half the size of the major axis
        public double Semimajor
        {
            get { return _a; }
        }

        // half the size of the minor axis
        public double Semiminor
        {
            get { return _a * Math.Sqrt(1 - _e * _e); }
        }

        public double Eccentricity
        {
            get { return _e; }
        }

        public static GeoDatum Wgs84
        {
            get { return _wgs84; }
        }

        private static GeoDatum _wgs84;

        static GeoDatum()
        {
            var wgs84a = 6378137.0;
            var wgs84g = 1 / 298.257223563;

            var wgs84e = Math.Sqrt(1 - BasicMath.Sqr(1 - wgs84g));

            _wgs84 = new GeoDatum(wgs84a, wgs84e);
        }
    }
}
