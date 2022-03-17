using System.Net;
using System.Text.Json;

namespace HttpServer;

internal static class Program
{
    private static List<User>? _users;
    private static void Main()
    {
        FillUsers();
        var listener = new HttpListener();
        listener.Prefixes.Add("http://localhost:45678/");
        listener.Start();

        while (true)
        {
            var context = listener.GetContext();
            var request = context.Request;
            var response = context.Response;

            if (request.HttpMethod == HttpMethod.Get.Method)
            {
                var sw = new StreamWriter(response.OutputStream);


                var option = new JsonSerializerOptions { WriteIndented = true };
                var data = JsonSerializer.Serialize(_users!, option);

                sw.WriteLine(data);
                sw.Close();
            }
            else if(request.HttpMethod == HttpMethod.Post.Method)
            {
                var user = JsonSerializer.Deserialize<User>(request.InputStream);
                _users!.Add(new User(user!.Name, user.Surname));
            }
        }
    }

    private static void FillUsers()
    {
        _users = new List<User>()
        {
            new User("Nicat","Qaraxanov"),
            new User("Murad","Seyidov"),
            new User("Metin","Abaszade"),
            new User("Elesker","Memmedov"),
            new User("Ilqar","Novruzov"),
            new User("Nezrin","Mustafazade"),
            new User("Ali","Eleskerov"),
            new User("Mehemmed","Memmedov"),
            new User("Elesger","Elizade")
        };
    }
}
