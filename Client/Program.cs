using System.Text.Json;

namespace Client;

internal static class Program
{
    private static readonly HttpClient Client = new HttpClient();
    private const string Url = "http://localhost:45678/";

    private static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.Write($"Enter (1) to get users\nEnter (2) to add new user\n>>");
            switch (Console.ReadLine())
            {
                case "1":
                    Get();
                    break;
                case "2":
                    Post();
                    break;
                default:
                    Console.WriteLine("Invalid Command!!!");
                    continue;
            }

            Console.ReadLine();
        }
    }

    private static async Task Get()
    {
        var response = await Client.GetAsync(Url);
        var users = JsonSerializer.Deserialize<List<User>>(await response.Content.ReadAsStringAsync());

        if (users != null)
            foreach (var user in users)
            {
                Console.WriteLine($"Id:{user.Id}\nName:{user.Name}\nSurname:{user.Surname}");
            }
    }

    private static async Task Post()
    {
        var user = new User();

        Console.Write("Enter the user name: ");
        user.Name = Console.ReadLine();
        Console.Write("Enter the user surname: ");
        user.Surname = Console.ReadLine();

        var content = new StringContent(JsonSerializer.Serialize(user));
        await Client.PostAsync(Url,content);
    }

}