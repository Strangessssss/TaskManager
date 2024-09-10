using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using TaskManagaerBeta.Services;

namespace TaskManagaerBeta.Models;

public partial class ProcessModel: ObservableObject
{
    public ProcessModel(Process process)
    {
        _process = process;
        
        Update(process);
    }

    private readonly Process _process;

    public int Id { get; set; }
    public string? ProcessName { get; set; }
    public string? Status { get; set; }
    [ObservableProperty] private string? _memoryUsage;

    public void Update(Process process)
    {
        Id = process.Id;
        ProcessName = process.ProcessName;
        Status = process.Responding ? "Running" : "Stopped";
        MemoryUsage = $"{Math.Round(process.WorkingSet64 / (1024.0 * 1024.0), 2)}MB";
    }
}