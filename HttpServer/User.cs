namespace HttpServer
{
    internal class User
    {
        private static int _id = 0;
        public User(string name, string surname)
        {
            Id = _id++;
            Name = name;
            Surname = surname;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}

