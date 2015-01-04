using Epicycle.Math.Geometry;

namespace Epicycle.Geodesy
{

    public static class GeoPoint2Utils
    {
        public static Vector3 ToEcef(this GeoPoint2 point, GeoDatum datum)
        {
            return GeoPoint3Utils.ToEcef((GeoPoint3)point, datum);
        }

        public static RotoTranslation3 Enu(this GeoPoint2 origin, GeoDatum datum)
        {
            return GeoPoint3Utils.Enu((GeoPoint3)origin, datum);
        }

        public sealed class YamlSerialization
        {
            public double Lat { get; set; }
            public double Long { get; set; }

            public YamlSerialization() { }

            public YamlSerialization(GeoPoint3 geoPoint3)
            {
                Lat = geoPoint3.Latitude_deg;
                Long = geoPoint3.Longtitude_deg;
            }

            public GeoPoint3 Deserialize()
            {
                return GeoPoint2.InDegrees(Lat, Long);
            }
        }
    }
}
