using Epicycle.Commons;

namespace Epicycle.Geodesy
{
    using System;

    public sealed class SexagesimalAngle
    {
        public SexagesimalAngle(double degrees)
        {
            double arcminutesFraction;

            ToSexagesimalFraction(degrees, out _degrees, out arcminutesFraction);

            ToSexagesimalFraction(arcminutesFraction, out _arcminutes, out _arcseconds);
        }

        public SexagesimalAngle(int degrees, double arcminutes)
        {
            _degrees = degrees;

            ToSexagesimalFraction(arcminutes, out _arcminutes, out _arcseconds);
        }

        private static void ToSexagesimalFraction(double real, out int integralPart, out double fractionalPart)
        {
            integralPart = (int)Math.Floor(real);

            fractionalPart = 60 * (real - integralPart);
        }

        public SexagesimalAngle(int degrees, int arcminutes, double arcseconds)
        {
            _degrees = degrees;
            _arcminutes = arcminutes;
            _arcseconds = arcseconds;
        }

        private readonly int _degrees;
        private readonly int _arcminutes;
        private readonly double _arcseconds;

        public int Degrees
        {
            get { return _degrees; }
        }

        public int Arcminutes
        {
            get { return _arcminutes; }
        }

        public double Arcseconds
        {
            get { return _arcseconds; }
        }

        public double ArcminutesFraction
        {
            get { return Arcminutes + Arcseconds / 60; }
        }

        public double InDegrees
        {
            get { return Degrees + ArcminutesFraction / 60; }
        }

        public double InRadians
        {
            get { return BasicMath.DegToRad(InDegrees); }
        }

        public override string ToString()
        {
            return string.Format("{0}° {1}' {2}''", Degrees, Arcminutes, Arcseconds);
        }

        public static SexagesimalAngle FromRadians(double rad)
        {
            return new SexagesimalAngle(BasicMath.RadToDeg(rad));
        }
    }
}
