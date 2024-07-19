using System;
using TfLRouteManager.Services;

namespace TfLRouteManager
{
    class Program
    {
        static void Main(string[] args)
        {
            TfLNetwork network = new TfLNetwork();
            network.InitializeNetwork();
            DelayManager delayManager = new DelayManager(network);
            TrackManager trackManager = new TrackManager(network);
            Printer printer = new Printer(network);
            RouteFinder routeFinder = new RouteFinder(network);

            while (true)
            {
                Console.WriteLine("1. Add Delay");
                Console.WriteLine("2. Remove Delay");
                Console.WriteLine("3. Close Track");
                Console.WriteLine("4. Open Track");
                Console.WriteLine("5. Print Closed Tracks");
                Console.WriteLine("6. Print Delayed Tracks");
                Console.WriteLine("7. Find Fastest Route");
                Console.WriteLine("8. Exit");

                int choice = int.Parse(Console.ReadLine());

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
                        HandleFindFastestRoute(routeFinder);
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                        break;
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
            double delay = double.Parse(Console.ReadLine());

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
