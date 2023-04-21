using System.Text;
using System.Text.Json;
using System.Diagnostics;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.DataServices;

public class RestDataService : IRestDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public RestDataService(HttpClient httpClient)
    {
        this._httpClient = httpClient;

        this._baseAddress = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5126"
            : "https://localhost:7291";

        this._url = $"{this._baseAddress}/api";

        this._jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<IEnumerable<ToDo>> GetAllToDosAsync()
    {
        var todos = new List<ToDo>();

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");

            return todos;
        }

        try
        {
            var response = await this._httpClient
                .GetAsync($"{this._url}/todo");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content
                    .ReadAsStringAsync();

                todos = JsonSerializer
                    .Deserialize<IEnumerable<ToDo>>(content, this._jsonSerializerOptions)
                    .ToList();
            }
            else
            {
                Debug.WriteLine("---> Non 2xx response");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Whoops exception: {ex.Message}");
        }


        return todos;
    }

    public async Task AddToDoAsync(ToDo todo)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");

            return;
        }

        try
        {
            var jsonToDo = JsonSerializer
                .Serialize(todo, this._jsonSerializerOptions);

            var content = new StringContent(
                jsonToDo, Encoding.UTF8,
                "application/json");

            var response = await this._httpClient
                .PostAsync($"{this._url}/todo", content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successfully created ToDo");
            }
            else
            {
                Debug.WriteLine("---> Non 2xx response");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Whoops exception: {ex.Message}");
        }

        return;
    }

    public async Task UpdateToDoAsync(ToDo todo)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");

            return;
        }

        try
        {
            var jsonToDo = JsonSerializer
                .Serialize(todo, this._jsonSerializerOptions);

            var content = new StringContent(
                jsonToDo, Encoding.UTF8,
                "application/json");

            var response = await this._httpClient
                .PutAsync($"{this._url}/todo/{todo.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successfully created ToDo");
            }
            else
            {
                Debug.WriteLine("---> Non 2xx response");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Whoops exception: {ex.Message}");
        }

        return;
    }

    public async Task DeleteToDoAsync(int id)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Debug.WriteLine("---> No internet access...");

            return;
        }

        try
        {
            var response = await this._httpClient
                .DeleteAsync($"{this._url}/todo/{id}");

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Successfully created ToDo");
            }
            else
            {
                Debug.WriteLine("---> Non 2xx response");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Whoops exception: {ex.Message}");
        }

        return;
    }
}
