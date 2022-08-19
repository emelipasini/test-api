namespace Models
{
    public class Street
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public Street(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
    }
}
