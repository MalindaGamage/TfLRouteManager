using System;
using TfLRouteManager.Services;

namespace TfLRouteManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = new TfLNetwork();
            network.InitializeNetwork();
            DelayManager delayManager = new DelayManager(network);
            TrackManager trackManager = new TrackManager(network);
            Printer printer = new Printer(network);
            var routeFinder = new RouteFinder(network);

            while (true)
            {
                Console.WriteLine("Select Mode:");
                Console.WriteLine("1. Engineer Operations");
                Console.WriteLine("2. Customer Operations");
                Console.WriteLine("3. Exit");

                if (!int.TryParse(Console.ReadLine(), out int mode) || mode < 1 || mode > 3)
                {
                    Console.WriteLine("Invalid mode. Please enter 1, 2, or 3.");
                    continue;
                }

                if (mode == 3)
                {
                    break;
                }
                else if (mode == 1)
                {
                    HandleEngineerOperations(delayManager, trackManager, printer);
                }
                else if (mode == 2)
                {
                    HandleCustomerOperations(routeFinder);
                }
            }
        }

        private static void HandleEngineerOperations(DelayManager delayManager, TrackManager trackManager, Printer printer)
        {
            while (true)
            {
                Console.WriteLine("Engineer Operations:");
                Console.WriteLine("1. Add Delay");
                Console.WriteLine("2. Remove Delay");
                Console.WriteLine("3. Close Track");
                Console.WriteLine("4. Open Track");
                Console.WriteLine("5. Print Closed Tracks");
                Console.WriteLine("6. Print Delayed Tracks");
                Console.WriteLine("7. Back to Main Menu");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        HandleAddDelay(delayManager);
                        break;
                    case 2:
                        HandleRemoveDelay(delayManager);
                        break;
                    case 3:
                        HandleCloseTrack(trackManager);
                        break;
                    case 4:
                        HandleOpenTrack(trackManager);
                        break;
                    case 5:
                        HandlePrintClosedTracks(printer);
                        break;
                    case 6:
                        HandlePrintDelayedTracks(printer);
                        break;
                    case 7:
                        return;
                }
            }
        }

        private static void HandleCustomerOperations(RouteFinder routeFinder)
        {
            while (true)
            {
                Console.WriteLine("Customer Operations:");
                Console.WriteLine("1. Find Fastest Route");
                Console.WriteLine("2. Back to Main Menu");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 2)
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        HandleFindFastestRoute(routeFinder);
                        break;
                    case 2:
                        return;
                }
            }
        }

        private static void HandleAddDelay(DelayManager delayManager)
        {
            Console.WriteLine("Enter from station:");
            string fromStation = Console.ReadLine();
            Console.WriteLine("Enter to station:");
            string toStation = Console.ReadLine();
            Console.WriteLine("Enter delay (in minutes):");

            if (!double.TryParse(Console.ReadLine(), out double delay) || delay <= 0)
            {
                Console.WriteLine("Invalid delay. Please enter a positive number.");
                return;
            }

            delayManager.AddDelay(fromStation, toStation, delay);
        }

        private static void HandleRemoveDelay(DelayManager delayManager)
        {
            Console.WriteLine("Enter from station:");
            string fromStation = Console.ReadLine();
            Console.WriteLine("Enter to station:");
            string toStation = Console.ReadLine();

            delayManager.RemoveDelay(fromStation, toStation);
        }

        private static void HandleCloseTrack(TrackManager trackManager)
        {
            Console.WriteLine("Enter from station:");
            string fromStation = Console.ReadLine();
            Console.WriteLine("Enter to station:");
            string toStation = Console.ReadLine();

            trackManager.CloseTrack(fromStation, toStation, true);
        }

        private static void HandleOpenTrack(TrackManager trackManager)
        {
            Console.WriteLine("Enter from station:");
            string fromStation = Console.ReadLine();
            Console.WriteLine("Enter to station:");
            string toStation = Console.ReadLine();

            trackManager.OpenTrack(fromStation, toStation, true);
        }

        private static void HandlePrintClosedTracks(Printer printer)
        {
            printer.PrintClosedTracks();
        }

        private static void HandlePrintDelayedTracks(Printer printer)
        {
            printer.PrintDelayedTracks();
        }

        private static void HandleFindFastestRoute(RouteFinder routeFinder)
        {
            Console.WriteLine("Enter start station:");
            string startStation = Console.ReadLine();
            Console.WriteLine("Enter end station:");
            string endStation = Console.ReadLine();

            var (route, directions) = routeFinder.FindFastestRoute(startStation, endStation);
            string routeDescription = routeFinder.GenerateRouteDescription(route, directions);
            Console.WriteLine(routeDescription);
        }
    }
}
