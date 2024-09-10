using System.Collections;
using System.Diagnostics;
using TaskManagaerBeta.Models;

namespace TaskManagaerBeta.Services;

public static class ProcessManager
{
    public static double UpdateCpuUsage()
    {
        var processes = Process.GetProcesses();
        foreach (var processInfo in processes)
        {
            try
            {
                var process = Process.GetProcessById(processInfo.Id);

                var currentCpuTime = process.TotalProcessorTime.TotalMilliseconds;
                var timeSinceLastCheck = 1000;

                var cpuUsage = (currentCpuTime / timeSinceLastCheck) / Environment.ProcessorCount * 100;

                return double.Parse($"{cpuUsage:F2}");
            }
            catch
            {
                return 0d;
            }
        }

        return 0f;
    }
    
}