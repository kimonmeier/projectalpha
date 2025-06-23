using System.Diagnostics;

namespace ProjectAlpha.Client;

public class BenchmarkHelper
{
    private readonly Dictionary<string, Stopwatch> _benchmarks = new();
    private readonly Dictionary<string, int> _executionTimes = new();
    private readonly Dictionary<string, List<int>> _executionRuns = new();
    
    public void Start(string name)
    {
        _benchmarks[name] = Stopwatch.StartNew();

        _executionRuns.TryAdd(name, new List<int>());
        _executionTimes.TryAdd(name, 0);
        _executionTimes[name] += 1;
    }
    
    public void Stop(string name)
    {
        var stopwatch = _benchmarks[name];
        stopwatch.Stop();
        _executionRuns[name].Add((int) stopwatch.Elapsed.TotalMilliseconds);
        
        _benchmarks.Remove(name);
    }

    public void Print()
    {
        Console.WriteLine("Benchmark Results:");
        Console.WriteLine("==================");
        Console.WriteLine("Name: Times: Average: Total");
        foreach (var (name, times) in _executionTimes)
        {
            var average = _executionRuns[name].Average();
            Console.WriteLine($"{name}: {times}: {TimeSpan.FromMilliseconds(average):g} {TimeSpan.FromMilliseconds(average * times):g}");
        }
        Console.WriteLine("==================");
    }

    public void Clear()
    {
        _benchmarks.Clear();
        _executionTimes.Clear();
        _executionRuns.Clear();
    }
    
    
    
}