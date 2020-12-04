using Roommates43.Models;
using Roommates43.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Roommates43
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roomieRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        ShowAllRooms(roomRepo);
                        break;
                    case ("Search for room"):
                        ShowOneRoom(roomRepo);
                        break;
                    case ("Add a room"):
                        AddOneRoom(roomRepo);
                        break;
                    case ("Show all chores"):
                        ShowAllChores(choreRepo);
                        break;
                    case ("Search for chore"):
                        ShowOneChore(choreRepo);
                        break;
                    case ("Add a chore"):
                        AddOneChore(choreRepo);
                        break;
                    case ("Search for roomate"):
                        ShowRoommate(roomieRepo);
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }
        static void ShowAllRooms(RoomRepository roomRepo)
        {
            List<Room> rooms = roomRepo.GetAll();
            foreach (Room r in rooms)
            {
                Console.WriteLine($"[{r.Id}] {r.Name} Max Occ({r.MaxOccupancy})");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void ShowOneRoom(RoomRepository roomRepo)
        {
            Console.Write("Room Id: ");
            int id = int.Parse(Console.ReadLine());

            Room room = roomRepo.GetById(id);

            Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void AddOneRoom(RoomRepository roomRepo)
        {
            Console.Write("Room name: ");
            string name = Console.ReadLine();

            Console.Write("Max occupancy: ");
            int max = int.Parse(Console.ReadLine());

            Room roomToAdd = new Room()
            {
                Name = name,
                MaxOccupancy = max
            };

            roomRepo.Insert(roomToAdd);

            Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void ShowAllChores(ChoreRepository choreRepo)
        {
            List<Chore> chores = choreRepo.GetAll();
            foreach (Chore c in chores)
            {
                Console.WriteLine($"[{c.Id}] {c.Name}");
            }
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void ShowOneChore(ChoreRepository choreRepo)
        {
            Console.Write("Chore Id: ");
            int id = int.Parse(Console.ReadLine());

            Chore chore = choreRepo.GetById(id);

            Console.WriteLine($"{chore.Id} - {chore.Name}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }
        static void AddOneChore(ChoreRepository choreRepo)
        {
            Console.Write("Chore name: ");
            string name = Console.ReadLine();

            Chore choreToAdd = new Chore()
            {
                Name = name,
            };
        }
        static void ShowRoommate(RoommateRepository roomieRepo)
        {
            Console.Write("Roommate Id: ");
            int id = int.Parse(Console.ReadLine());

            Roommate roommate = roommateRepo.GetById(id);

            Console.WriteLine($"{roommate.Id} - {roommate.Name}");
            Console.Write("Press any key to continue");
            Console.ReadKey();
        }

        static string GetMenuSelection()
            {
                Console.Clear();

                List<string> options = new List<string>()
        {
            "Show all rooms",
            "Search for room",
            "Add a room",
            "Show all chores",
            "Search for a chore",
            "Add a chore",
            "Exit"
        };

                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Select an option > ");

                        string input = Console.ReadLine();
                        int index = int.Parse(input) - 1;
                        return options[index];
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }

            }
        }
    }
