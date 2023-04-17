using System.Diagnostics;
using ToDoMauiClient.DataServices;
using ToDoMauiClient.Models;

namespace ToDoMauiClient.Pages;

[QueryProperty(nameof(ToDo), "ToDo")]
public partial class ManageToDoPage : ContentPage
{
    private readonly IRestDataService _dataService;

    private ToDo _toDo;

    private bool _isNew;

    public ManageToDoPage(IRestDataService dataService)
    {
        InitializeComponent();

        this._dataService = dataService;

        BindingContext = this;
    }

    public ToDo ToDo
    {
        get => this._toDo;
        set
        {
            this._isNew = this.IsNew(value);
            this._toDo = value;

            OnPropertyChanged();
        }
    }

    bool IsNew(ToDo todo)
       => todo.Id == 0
           ? true
           : false;

    async void OnSaveButtonclicked(object sender, EventArgs e)
    {
        if (this._isNew)
        {
            Debug.WriteLine("---> Add new Item");

            await this._dataService
                .AddToDoAsync(this.ToDo);
        }
        else
        {
            Debug.WriteLine("---> Update an Item");

            await this._dataService
                .UpdateToDoAsync(this.ToDo);
        }

        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        await this._dataService
            .DeleteToDoAsync(this.ToDo.Id);

        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}