using WebAPI.Models;

namespace Tests.Fixtures
{
    public class StreetFixture
    {
        public static List<Street> GetTestStreets()
        {
            return new List<Street>()
            {
                new Street()
                {
                    Id = 1,
                    Name = "Viamonte",
                    City = "Rosario"
                },
                new Street()
                {
                    Id = 2,
                    Name = "Rivadavia",
                    City = "Victoria"
                },
                new Street()
                {
                    Id = 3,
                    Name = "Laprida",
                    City = "Rosario"
                }
            };
        }

        public static Street GetTestStreetById()
        {
            return new Street()
            {
                Id = 5,
                Name = "Sarmiento",
                City = "Victoria"
            };
        }
    }
}

