﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLO24_SecondTurnInNiklasH
{
    using Factories;
    using Interfaces;

    public static class VehicleInitializer
    {
        // Method to encapsulate the entire program logic, with a try-catch block to handle exceptions
        internal static void RunFactory()
        {
            try // Try to run the program logic, else catch any exceptions that occur
            {
                // Create a List of vehicles and display their details
                List<IVehicle> vehicles = CreateVehicleList();

                // Shuffle the vehicle list and then display the modified vehicles
                ShuffleList(vehicles);
                DisplayOriginalAndModifiedVehicles(vehicles);
            }
            catch (Exception exRun)
            {
                Console.WriteLine($"An error occurred: {exRun.Message}");
            }
            finally
            {
                Program.FactoryShutdown();
            }
        }

        // Method to create a list of vehicles
        private static List<IVehicle> CreateVehicleList()
        {
            // Creating vehicle instances of the vehicle classes to supply our factory classes
            CarFactory carFactory = new CarFactory();
            MotorcycleFactory motorcycleFactory = new MotorcycleFactory();
            TractorFactory tractorFactory = new TractorFactory();

            // Creating a list to hold all the vehicles
            List<IVehicle> vehicles = new List<IVehicle>();

            try // Try to add vehicles to the list, else catch any exceptions that occur
            {
                // Adding vehicles to the list
                vehicles.Add(CreateVehicle(carFactory, "Toyota", "Corolla", 2020, 15000, 4));
                vehicles.Add(CreateVehicle(motorcycleFactory, "Harley Davidson", "Sportster", 2019, 5000, "V-Twin"));
                vehicles.Add(CreateVehicle(tractorFactory, "John Deere", "X9 Combine", 2021, 1200, "Harvester", 12));
                vehicles.Add(CreateVehicle(carFactory, "Ford", "Focus", 2018, 30000, 5));
                vehicles.Add(CreateVehicle(motorcycleFactory, "Yamaha", "MT-07", 2020, 8000, "Parallel Twin"));
                vehicles.Add(CreateVehicle(tractorFactory, "Massey Ferguson", "6713", 2020, 500, "Loader", 7));
                vehicles.Add(CreateVehicle(carFactory, "Honda", "Civic", 2021, 10000, 4));
                vehicles.Add(CreateVehicle(motorcycleFactory, "Ducati", "Monster", 2022, 2000, "V-Twin"));
                vehicles.Add(CreateVehicle(tractorFactory, "Kubota", "M7", 2020, 600, "Forklift", 11));
            }
            catch (ArgumentException exArgumentExceptionsAddingVehicles) // Catch specific argument exceptions and print the error message
            {
                Console.WriteLine($"Vehicle creation error: {exArgumentExceptionsAddingVehicles.Message}");
            }
            catch (Exception exAddingVehicles) // Catch any other exceptions that occur during vehicle creation and print the error message
            {
                Console.WriteLine($"An error occurred while adding vehicles: {exAddingVehicles.Message}");
            }

            return vehicles;
        }

        // Method to shuffle the list of vehicles, Fisher-Yates shuffle algorithm
        private static void ShuffleList<T>(List<T> list)
        {
            Random shuffleList = new Random();      // Initializes a new random number
            int n = list.Count;                     // Get the total number of items in the list
            while (n > 1)                           // Continue until all items are shuffled
            {
                n--;                                // Decrease n (the range of unshuffled items) by 1
                int k = shuffleList.Next(n + 1);    // Generate a random index between 0 and n
                T value = list[k];                  // Store the value at the random index
                list[k] = list[n];                  // Swap the value at the random index with the last unshuffled item
                list[n] = value;                    // Place the stored value in the last unshuffled position
            }
        }

        // Method to display original and modified vehicle details using a switch statement
        private static void DisplayOriginalAndModifiedVehicles(List<IVehicle> vehicles)
        {
            // Define lists of possible modifications
            List<int> carDoorModifications = new List<int> { 2, 3, 4, 5 }; // Possible door counts for cars
            List<string> motorcycleEngineModifications = new List<string> { "V-Twin", "Inline-4", "Parallel Twin", "Single Cylinder" }; // Possible engine types for motorcycles
            List<string> tractorUtilityModifications = new List<string> { "Plow", "Harvester", "Forklift", "Seeder" }; // Possible utility types for tractors
            List<double> tractorWeightModifications = new List<double> { 6, 8, 10, 12 }; // Possible Weight modifications for Tractor

            // Single Random instance for all modifications
            Random tempRandom = new Random();

            foreach (var vehicle in vehicles)
            {
                DisplayVehicleDetails(vehicle);

                try
                {
                    // Apply modifications based on vehicle type using a switch type pattern
                    switch (vehicle)
                    {
                        case ICar car:
                            // Apply a random door modification from the list
                            int newDoors = carDoorModifications[tempRandom.Next(carDoorModifications.Count)];
                            Console.WriteLine($"Modified Car Doors: {newDoors}");
                            car.Doors = newDoors;
                            break;

                        case IMotorcycle motorcycle:
                            // Apply a random engine modification from the list
                            string newEngineType = motorcycleEngineModifications[tempRandom.Next(motorcycleEngineModifications.Count)];
                            Console.WriteLine($"Modified Motorcycle Engine Type: {newEngineType}");
                            motorcycle.EngineType = newEngineType;
                            break;

                        case ITractor tractor:
                            // Apply a random utility modification from the list
                            string newUtilityTool = tractorUtilityModifications[tempRandom.Next(tractorUtilityModifications.Count)];
                            Console.WriteLine($"Modified Tractor Utility: {newUtilityTool}");
                            tractor.UtilityTool = newUtilityTool;

                            // Apply a random weight modification from the list
                            double newWeight = tractorWeightModifications[tempRandom.Next(tractorWeightModifications.Count)];
                            Console.WriteLine($"Modified Tractor Weight: {newWeight} tons");
                            tractor.Weight = newWeight;
                            break;

                        default:
                            Console.WriteLine("Unknown vehicle type.");
                            break;
                    }
                }
                catch (Exception exModification) // Catch any other exceptions that occur during vehicle modification and print the error message
                {
                    Console.WriteLine($"An error occurred while modifying the vehicle: {exModification.Message}");
                }

                Console.WriteLine(); // Add a line break between vehicles
            }
        }

        // This below section is the "Factory". Utilizing the factory pattern to create vehicles, this can be expanded to include more vehicle types
        private static IVehicle CreateVehicle(CarFactory factory, string brand, string model, int year, double mileage, int doors) // Note: doors
        {
            return factory.CreateCar(brand, model, year, mileage, doors);
        }

        private static IVehicle CreateVehicle(MotorcycleFactory factory, string brand, string model, int year, double mileage, string engineType) // Note: engineType
        {
            return factory.CreateMotorcycle(brand, model, year, mileage, engineType);
        }

        private static IVehicle CreateVehicle(TractorFactory factory, string brand, string model, int year, double mileage, string utilityTool, double weight) // Note: utilityTool, weight
        {
            return factory.CreateTractor(brand, model, year, mileage, utilityTool, weight);
        }

        // Method to display vehicle details - here we also check for car, motorcycle, tractor specific properties
        private static void DisplayVehicleDetails(IVehicle vehicle)
        {
            Console.WriteLine(vehicle.ToString());
            vehicle.StartEngine();
            Console.WriteLine("Vehicle engine status: " + (vehicle.IsEngineOn() ? "On" : "Off"));

            // Check if the vehicle is driveable, then call the Drive method
            if (vehicle is IDriveable driveableVehicle)
            {
                Console.WriteLine(driveableVehicle.Drive());
            }
            else
            {
                Console.WriteLine("This vehicle cannot be driven.");
            }

            vehicle.StopEngine();
            Console.WriteLine("Vehicle engine status: " + (vehicle.IsEngineOn() ? "On" : "Off"));

            // Check for specific properties based on the vehicle type using a switch statement
            switch (vehicle)
            {
                case ICar car:
                    Console.WriteLine($"Car doors: {car.Doors}"); // Display the modified number of doors
                    break;

                case IMotorcycle motorcycle:
                    Console.WriteLine($"Motorcycle engine type: {motorcycle.EngineType}"); // Display the modified engine type
                    break;

                case ITractor tractor:
                    Console.WriteLine($"Tractor utility tool: {tractor.UtilityTool}"); // Display the utility tool
                    Console.WriteLine($"Tractor Weight: {tractor.Weight} tons"); // Display the weight
                    break;
            }
        }
    }
}
