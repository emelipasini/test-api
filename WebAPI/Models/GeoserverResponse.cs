namespace WebAPI.Models
{
    public class GeoserverResponse<T>
    {
        public List<Feature<T>> features { get; set; }
    }

    public class Feature<T>
    {
        public object geometry { get; set; }
        public T properties { get; set; }
    }
}

