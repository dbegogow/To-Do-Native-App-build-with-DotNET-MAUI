using System.ComponentModel;

namespace ToDoMauiClient.Models;

public class ToDo : INotifyPropertyChanged
{
    private int _id;

    private string _todoname;

    public int Id
    {
        get => this._id;
        set
        {
            if (this._id == value)
            {
                return;
            }

            this._id = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Id)));
        }
    }

    public string ToDoName
    {
        get => this._todoname;
        set
        {
            if (this._todoname == value)
            {
                return;
            }

            this._todoname = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.ToDoName)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
