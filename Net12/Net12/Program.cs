﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Net12
{
    class Program
    {
        static void Main(string[] args)
        {
            var users = new List<User>();

            for (int i = 0; i < 3; i++)
            {
                var user = new User();

                user.FirstName = AskUser("Enter your first name");
                user.LastName = AskUser("Enter your last name");
                user.Pet = AskUser("Fav pet?");

                users.Add(user);
            }

            foreach (var user in users.OrderBy(x => x.Pet))
            {
                Console.WriteLine($"Name:{user.FirstName}. Pet: {user.Pet}");
            }
        }

        private static string AskUser(string userMessage)
        {
            string pet;
            do
            {
                Console.WriteLine(userMessage);
                pet = Console.ReadLine();
            } while (pet.Length <= 3);

            return pet;
        }
    }
}
