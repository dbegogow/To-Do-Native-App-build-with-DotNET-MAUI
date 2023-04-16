using System.Diagnostics;
using ToDoMauiClient.DataServices;
using ToDoMauiClient.Models;
using ToDoMauiClient.Pages;

namespace ToDoMauiClient;

public partial class MainPage : ContentPage
{
    private IRestDataService _dataService;

    public MainPage(IRestDataService dataService)
    {
        InitializeComponent();

        this._dataService = dataService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        collectionView.ItemsSource = await this._dataService
            .GetAllToDosAsync();
    }

    async void OnAddToDoClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("---> Add button clicked!");

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(ToDo), new ToDo() }
        };

        await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
    }

    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("---> Item changed clicked!");

        var navigationParameter = new Dictionary<string, object>
        {
            {nameof(ToDo), e.CurrentSelection.FirstOrDefault() as ToDo }
        };

        await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
    }
}

