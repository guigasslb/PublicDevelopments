using System;
using System.Diagnostics;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please specify the process Name: ");
        string processName = Console.ReadLine();

        Console.WriteLine("What is the maximum lifetime? (in minutes): ");
        int maxLifetime = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("What is the monitoring frequency? (in minutes): ");
        int monitoringFrequency = Convert.ToInt32(Console.ReadLine());

        Process[] processes = Process.GetProcesses();


        foreach (var item in processes)
        {
            if (item.ProcessName.Contains(processName))
            {
                processName = item.ProcessName;
            }

        }
        Process[] processSelected = Process.GetProcessesByName(processName);
        Console.WriteLine($"Monitoring process {processSelected[0].ProcessName} with max lifetime {maxLifetime} minutes and monitoring frequency {monitoringFrequency} minutes.");


        while (true)
        {


            if (processes.Length == 0)
            {
                Console.WriteLine($"No {processName} processes found. Waiting for {monitoringFrequency} minutes before checking again.");
            }
            else
            {

                TimeSpan processLifetime = DateTime.Now - processSelected[0].StartTime;
                if (processLifetime.TotalMinutes > maxLifetime)
                {
                    Console.WriteLine($"Killing process {processName} with PID {processSelected[0].Id} because it has exceeded its maximum lifetime of {maxLifetime} minutes.");
                    processSelected[0].Kill();
                }
                else
                {
                    Console.WriteLine($"Process {processName} with PID {processSelected[0].Id} is running within its lifetime of {maxLifetime} minutes.");
                }

            }

            Console.WriteLine("Press 'q' to leave or any other key to continue monitoring.");
            if (Console.ReadKey(true).KeyChar == 'q')
            {
                Console.WriteLine("Exiting program.");
                return;
            }

            Console.WriteLine($"Waiting for {monitoringFrequency} minutes before checking again.");
            System.Threading.Thread.Sleep(monitoringFrequency * 60 * 1000);
        }
    }
}