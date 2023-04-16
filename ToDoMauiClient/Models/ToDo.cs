using System.ComponentModel;

namespace ToDoMauiClient.Models;

public class ToDo : INotifyPropertyChanged
{
    private int _id;

    private string _todoname;

    public int Id { get; set; }

    public int ToDoName { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
}
