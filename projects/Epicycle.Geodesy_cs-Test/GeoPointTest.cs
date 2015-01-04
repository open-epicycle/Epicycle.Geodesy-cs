using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Epicycle.Math.Geometry;

namespace Epicycle.Geodesy
{
    [TestFixture]
    public sealed class GeoPointTest : AssertionHelper
    {
        [Test]
        public void ToEcef_returns_correct_result()
        {
            // example computed using http://www.oc.nps.edu/oc2902w/coord/llhxyz.htm with 1m precision at each axis
            var geodetic = GeoPoint3.InDegrees(latitude_deg: 32.067, longitude_deg: 54.783, altitude: 3.141);
            var ecef = new Vector3(3119880, 4419927, 3366731);

            var actualEcef = geodetic.ToEcef(GeoDatum.Wgs84);

            Expect(Vector3.Distance(actualEcef, ecef), Is.LessThan(2.0));
        }

        [Test]
        public void EcefToGeodetic_composed_with_ToEcef_yields_original_point()
        {
            var datum = GeoDatum.Wgs84;
            var ecef = new Vector3(3119880, 4419927, 3366731);

            var geodetic = GeoPoint3Utils.EcefToGeodetic(ecef, datum);
            var roundTrip = geodetic.ToEcef(datum);

            Expect(Vector3.Distance(roundTrip, ecef), Is.LessThan(1e-3));
        }

        [Test]
        public void Enu_is_compatible_with_numerical_differential_of_GeodeticToEcef()
        {
            var datum = new GeoDatum(semimajor: 6, eccentricity: 0.314);

            var geodetic = GeoPoint3.InDegrees(latitude_deg: 32.067, longitude_deg: 54.783, altitude: 3.141);
            var ecef = geodetic.ToEcef(datum);

            var enu = geodetic.Enu(datum);

            Expect(Vector3.Distance(enu.Translation, ecef), Is.LessThan(1e-10));

            var eps = 1e-10;
            var tolerance = 1e-5;

            var geodeticN = new GeoPoint3(geodetic.Latitude + eps, geodetic.Longitude, geodetic.Altitude);
            var ecefN = geodeticN.ToEcef(datum);
            var enuN = enu.ApplyInv(ecefN);

            Expect(Vector3.Distance(enuN.Normalized, Vector3.UnitY), Is.LessThan(tolerance));

            var geodeticE = new GeoPoint3(geodetic.Latitude, geodetic.Longitude + eps, geodetic.Altitude);
            var ecefE = geodeticE.ToEcef(datum);
            var enuE = enu.ApplyInv(ecefE);

            Expect(Vector3.Distance(enuE.Normalized, Vector3.UnitX), Is.LessThan(tolerance));

            var geodeticU = new GeoPoint3(geodetic.Latitude, geodetic.Longitude, geodetic.Altitude + eps);
            var ecefU = geodeticU.ToEcef(datum);
            var enuU = enu.ApplyInv(ecefU);

            Expect(Vector3.Distance(enuU, eps * Vector3.UnitZ), Is.LessThan(eps * 1e-4));
        }
    }
}
