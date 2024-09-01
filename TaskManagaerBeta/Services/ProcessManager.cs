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
    
    public static double UpdateTotalDiskUsage()
    {
        var processes = Process.GetProcesses();
        double totalDiskUsage = 0;

        foreach (var processInfo in processes)
        {
            try
            {
                var process = Process.GetProcessById(processInfo.Id);

                using (var diskReadCounter = new PerformanceCounter("Process", "IO Read Bytes/sec", process.ProcessName))
                using (var diskWriteCounter = new PerformanceCounter("Process", "IO Write Bytes/sec", process.ProcessName))
                {
                    var diskReadBytes = diskReadCounter.NextValue();
                    var diskWriteBytes = diskWriteCounter.NextValue();

                    var totalMBps = (diskReadBytes + diskWriteBytes) / (1024.0 * 1024.0);

                    totalDiskUsage += totalMBps;
                }
            }
            catch
            {
                return 0f;
            }
        }

        return Math.Round(totalDiskUsage, 2);
    }
    
}