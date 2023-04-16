using ToDoMauiClient.Models;

namespace ToDoMauiClient.DataServices;

public interface IRestDataService
{
    Task<IEnumerable<ToDo>> GetAllToDosAsync();

    Task AddToDoAsync(ToDo todo);

    Task UpdateToDoAsync(ToDo todo);

    Task DeleteToDoAsync(int id);
}
