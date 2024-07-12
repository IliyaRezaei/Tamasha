using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamasha.Repositories;
using Tamasha.ViewModel;

namespace Tamasha.Application
{
    internal class Menu
    {
        UserRepository userRepository;
        VideoRepository videoRepository;
        CommentRepository commentRepository;
        public Menu() 
        { 
            userRepository = new UserRepository();
            videoRepository = new VideoRepository();
            commentRepository = new CommentRepository();
        }
        public string loggedinUsername = "";
        public void Start()
        {
            Console.WriteLine("" +
                "1.Register\n" +
                "2.Login\n" +
                "3.Make New Video(You must be logged in)\n" +
                "4.Update Username\n" +
                "5.Delete Username\n" +
                "6.Print All Users\n" +
                "7.Make New Comment\n" +
                "8.Logout\n" +
                "9.Exit");
            var choice = Convert.ToInt32(Console.ReadLine());
            Decision(choice);
        }


        public void Decision(int choice)
        {
            switch (choice)
            {
                case 1:
                    {
                        Register();
                    }
                break;
                case 2:
                    {
                        Login();
                    }
                break;
                case 3:
                    {
                        MakeNewVideo();
                    }
                break;
                case 4:
                    {
                        UpdateUsername();
                    }
                break;
                case 5:
                    {
                        DeleteUserByUsername();
                    }
                break;
                case 6:
                    {
                        PrintAllUsers();
                    }
                break;
                case 7:
                    {
                        makeNewComment();
                    }
                break;
                    
                default:
                    {
                        Console.WriteLine("Wrong input");
                    }
                break;
            }
            Start();
        }

        public void Register()
        {
            
            Console.WriteLine("Enter your Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter your Username: ");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your Password: ");
            string password = Console.ReadLine();
            userRepository.Register(email,username,password,DateTime.Now);
            Console.WriteLine("You successfully Registered!");
        }

        public void Login()
        {
            Console.WriteLine("Enter your Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter you Password");
            string password = Console.ReadLine();
            var result = userRepository.Login(email, password);
            if (result.Count>0) 
            { 
                loggedinUsername = result[0].Replace(";", ""); 
            }
            else
            {
                Console.WriteLine("Wrong username or password");
                Start();
            }
            Console.WriteLine("You successfully Logged in");
        }

        public void MakeNewVideo()
        {
            Random next = new Random();
            char[] chars = new char[50];
            for (int i = 0; i < 50; i++)
            {
                chars[i] = (char)next.Next(65, 90);
            }
            if (userRepository.FindUserByUsername(loggedinUsername))
            {
                Console.WriteLine("Enter Video's Title: ");
                string title = Console.ReadLine();
                Console.WriteLine("Enter Video's Description: ");
                string description = Console.ReadLine();
                Console.WriteLine("Is Video Age restricted?: 1.True or 0.False");
                string ageRestrict = Console.ReadLine();
                string url = "WWW." + new string(chars) + ".COM";
                bool isAgeRestricted = false;
                if (ageRestrict == "1")
                {
                    isAgeRestricted = true;
                }
                videoRepository.MakeNewVideo(title, description, url, isAgeRestricted, loggedinUsername);
            }
        }

        public void makeNewComment()
        {
            if(loggedinUsername != "")
            {
                Console.WriteLine("What is your comment on the video");
                var comment = Console.ReadLine();
                Console.WriteLine("Enter Video's Url: ");
                var url = Console.ReadLine(); //"HERE I WOULD JUST COPY THE URL FROM CLIENT BROWSER AND INSERT IT INTO DATABASE"
                commentRepository.MakeNewComment(comment, url, loggedinUsername);
                Console.WriteLine("Commend Successfully added to the video");
            }
            Start();
        }

        public void DeleteUserByUsername()
        {
            userRepository.DeleteUserByUsername(loggedinUsername);
            Console.WriteLine("Your account got deleted and you are logged out now");
            loggedinUsername = "";
            Start();
        }

        public void UpdateUsername()
        {
            if(loggedinUsername != "")
            {
                Console.WriteLine("Enter your new Username: ");
                string newUsername = Console.ReadLine();
                userRepository.UpdateUsername(newUsername, loggedinUsername);
                loggedinUsername = newUsername;
                Console.WriteLine($"You successfully changed your username to {newUsername}");
            }
            Start();
        }

        public void PrintAllUsers()
        {
            List<User> users = userRepository.GetAllUsers();
            foreach (var item in users)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
