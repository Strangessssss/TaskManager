using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TaskManagaerBeta.Models;

namespace TaskManagaerBeta.ViewModels;

public partial class TaskManagerViewModel : ObservableObject
{
    private const string PathToImages = "../Images/";
    [ObservableProperty] private string _stopUpdateButtonImage = PathToImages + "black_square_for_stop.png";
    [ObservableProperty] private string _startButtonImage = PathToImages + "../Images/arrow_forward.png";
    [ObservableProperty] private string _searchButtonImage = PathToImages + "../Images/mag_right.png";
    [ObservableProperty] private string _newProccessButtonImage = PathToImages + "../Images/new.png";

    [ObservableProperty] private ObservableCollection<ProcessModel> _processes = new();
    private bool _updateFlag = true;
    private System.Timers.Timer _timer;

    public TaskManagerViewModel()
    {
        UpdateProcesses();
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += (sender, e) => UpdateProcesses();
        _timer.Start();
    }

    private void UpdateProcesses()
    {
        Processes = new ObservableCollection<ProcessModel>(Process.GetProcesses().Select(p => new ProcessModel(p)));
    }
    
    [RelayCommand]
    private void StopUpdate()
    {
        if (StopUpdateButtonImage == PathToImages + "black_square_for_stop.png")
        {
            StopUpdateButtonImage = PathToImages + "arrows_counterclockwise.png";
            _updateFlag = false;
        }
        UpdateProcesses();
    }

    [RelayCommand]
    private void Start()
    {
        StopUpdateButtonImage = PathToImages + "black_square_for_stop.png";
        _updateFlag = true;
    }
}