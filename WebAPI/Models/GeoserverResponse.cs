namespace WebAPI.Models
{
    public class GeoserverResponse<T>
    {
        public Feature<T>[] features { get; set; }

        public GeoserverResponse(Feature<T>[] features)
        {
            this.features = features;
        }
    }

    public class Feature<T>
    {
        public object geometry { get; set; }
        public T properties { get; set; }

        public Feature(object geometry, T properties)
        {
            this.geometry = geometry;
            this.properties = properties;
        }
    }
}
