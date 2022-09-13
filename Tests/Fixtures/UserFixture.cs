using WebAPI.Models;

namespace Tests.Fixtures
{
    internal class UserFixture
    {
        public static List<User> GetTestUsers()
        {
            return new List<User>()
            {
                new(1, "testing", "test.123"),
                new(2, "tests", "password"),
                new(3, "username", "12345")
            };
        }
    }
}
