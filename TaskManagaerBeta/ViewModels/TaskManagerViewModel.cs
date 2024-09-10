using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagaerBeta.Messages;
using TaskManagaerBeta.Models;
using TaskManagaerBeta.Services;

namespace TaskManagaerBeta.ViewModels;

public partial class TaskManagerViewModel : ObservableObject
{
    private const string ClownSound = @"Sounds\circus-clown-music.wav";
    
    [ObservableProperty] private string _stopUpdateButtonImage = ButtonImageService.GetButonImage("StopUpdate");
    [ObservableProperty] private string _startButtonImage = ButtonImageService.GetButonImage("Start");
    [ObservableProperty] private string _searchButtonImage = ButtonImageService.GetButonImage("Search");
    [ObservableProperty] private string _newProcessButtonImage = ButtonImageService.GetButonImage("NewProcess");
    [ObservableProperty] private string _clownModeButtonImage = ButtonImageService.GetButonImage("ClownMode");
    
    [ObservableProperty] private ObservableCollection<ProcessModel> _processes = new();
    private bool _updateFlag = true;
    private System.Timers.Timer _timer;

    [ObservableProperty] private string _search = "";

    public TaskManagerViewModel()
    {
        UpdateProcesses();
        _timer = new System.Timers.Timer(200);
        _timer.Elapsed += (_, _) => UpdateProcesses();
        _timer.Start();
    }

    private void UpdateProcesses()
    {
        
       if (_updateFlag)
       {
           Application.Current.Dispatcher.Invoke(() =>
           {
               foreach (var newProcess in Process.GetProcesses())
               {
                   var process = Processes.FirstOrDefault(p => p.Id == newProcess.Id);
                   if (process == null)
                   {
                       Processes.Add(new ProcessModel(newProcess));
                   }
                   else
                   {
                       process.Update(newProcess);
                   }
               }
           });
       }
    }
    
    [RelayCommand]
    private void StopUpdate()
    {
        if (StopUpdateButtonImage == ButtonImageService.GetButonImage("StopUpdate"))
        {
            StopUpdateButtonImage = ButtonImageService.GetButonImage("Update");
            _updateFlag = false;
        }
        UpdateProcesses();
    }
    
    [RelayCommand]
    private void StartClownMode()
    {
        StopUpdateButtonImage = ButtonImageService.GetButonImage("ClownMode");
        StartButtonImage = ButtonImageService.GetButonImage("ClownMode");
        SearchButtonImage = ButtonImageService.GetButonImage("ClownMode");
        NewProcessButtonImage = ButtonImageService.GetButonImage("ClownMode");
        var playerTask = new Task(() => {
            var player = new SoundPlayer(ClownSound);
            player.PlayLooping();
        });
        playerTask.Start();
    }

    [RelayCommand]
    private void Start()
    {
        StopUpdateButtonImage = ButtonImageService.GetButonImage("StopUpdate");
        _updateFlag = true;
    }

    private void SearchUpdate(object _, SearchChanged message)
    {
    }
    
    [RelayCommand]
    private void Run()
    {
        const int keyeventfExtendedkey = 0x1;
        const int keyeventfKeyup = 0x2;
        const int vkLwin = 0x5B; 
        const int vkR = 0x52; 
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        
        keybd_event(vkLwin, 0, keyeventfExtendedkey, UIntPtr.Zero);
        keybd_event(vkR, 0, keyeventfExtendedkey, UIntPtr.Zero);
        keybd_event(vkR, 0, keyeventfKeyup, UIntPtr.Zero);
        keybd_event(vkLwin, 0, keyeventfKeyup, UIntPtr.Zero);
    }
}