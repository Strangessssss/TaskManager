using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using TaskManagaerBeta.Services;

namespace TaskManagaerBeta.Models;

public class ProcessModel: ObservableObject
{
    public ProcessModel(Process process)
    {
        Process = process;
    }

    public Process Process;

    public int Id => Process.Id;
    public string ProcessName => Process.ProcessName;
    public string Priority => Process.PriorityClass.ToString();
    public string Status => Process.Responding ? "Running" : "Stopped";
    public string CpuUsage => $"{ProcessManager.UpdateCpuUsage()}%";
    public string MemoryUsage => $"{Process.WorkingSet64 / (1024.0 * 1024.0)}MB";
    public string Disk =>$"{ProcessManager.UpdateTotalDiskUsage()}MB/s";
}