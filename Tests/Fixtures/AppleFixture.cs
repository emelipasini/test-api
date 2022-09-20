using WebAPI.Models;

namespace Tests.Fixtures
{
    public class AppleFixture
    {
        public static GeoserverResponse<Apple> GetTestApples()
        {
            var features = new List<Feature<Apple>>()
            {
                new Feature<Apple>()
                {
                    geometry = new object(),
                    properties = new Apple(1, "2022-09-14", (float)3.4, (float)4.5)
                },
                new Feature<Apple>()
                {
                    geometry = new object(),
                    properties = new Apple(2, "2022-09-14", (float)3.4, (float)4.5)
                },
                new Feature<Apple>()
                {
                    geometry = new object(),
                    properties = new Apple(3, "2022-09-15", (float)3.4, (float)4.5)
                }
            };

            return new GeoserverResponse<Apple>() { features = features };
        }

        public static GeoserverResponse<Apple> GetTestAppleById()
        {
            var feature = new List<Feature<Apple>>()
            {
                new Feature<Apple>()
                {
                    geometry = new object(),
                    properties = new Apple(5, "2022-09-20", (float)5.9, (float)7.4)
                }
            };

            return new GeoserverResponse<Apple> { features = feature };
        }
    }
}

