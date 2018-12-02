using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework03
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task 1
            Console.WriteLine("Task1:");
            string inputString = "Davis, Clyne, Fonte, Hooiveld, Shaw, Davis, Schneiderlin, Cork, Lallana, Rodriguez, Lambert";

            string[] splitedString = inputString.Split(new[] { ", " }, StringSplitOptions.None);
            var indexes = Enumerable.Range(1, splitedString.Length);

            var resultArray = splitedString.Zip(indexes, (n1, n2) => n2.ToString() + ". " + n1);
            string resultString = resultArray.Aggregate((current, next) => current + ", " + next);

            Console.WriteLine(resultString);
            Console.WriteLine();


            // Task 2
            Console.WriteLine("Task2:");
            string inputString2 = "Jason Puncheon, 26/06/1986; Jos Hooiveld, 22/04/1983; Kelvin Davis, 29/09/1976; Luke Shaw, 12/07/1995; Gaston Ramirez, 02/12/1990; Adam Lallana, 10/05/1988";
            //var result = inputString2.Split(';')
            //    .Select(playeyWithYear => playeyWithYear.Split(','))
            //    .Select(s => new { Player = s[0], Year = DateTime.Parse(s[1]) })
            //    .OrderBy(element => element.Year)
            //    .Select(p => p.Player);
            string[] splitedString2 = inputString2.Split(new[] { "; " }, StringSplitOptions.None);

            var players = splitedString2.Select(n =>
                                                    {
                                                        string[] playerInfo = n.Split(new[] { ", " }, StringSplitOptions.None);
                                                        string name = playerInfo[0];
                                                        string[] birthdayInfo = playerInfo[1].Split('/');
                                                        int day = Convert.ToInt32(birthdayInfo[0]);
                                                        int mounth = Convert.ToInt32(birthdayInfo[1]);
                                                        int year = Convert.ToInt32(birthdayInfo[2]);
                                                        DateTime birthday = new DateTime(year, mounth, day);

                                                        return new { name, birthday };
                                                    }
                                                )
                                        .OrderByDescending(n => n.birthday.Year);

            DateTime currentTime = DateTime.Now;
            foreach (var player in players)
            {
                TimeSpan diff = currentTime.Subtract(player.birthday);
                int playerAge = (new DateTime() + diff).Year;
                Console.WriteLine($"{player.name} - {playerAge}");
                //Console.WriteLine(player.birthday);
            }
            Console.WriteLine();


            // Task 3
            Console.WriteLine("Task3:");
            string inputString3 = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27";
            string[] splitedString3 = inputString3.Split(',');

            TimeSpan duration = splitedString3.Select(n =>
                                                    {
                                                        string[] timeInfo = n.Split(':');
                                                        int minutes = Convert.ToInt32(timeInfo[0]);
                                                        int seconds = Convert.ToInt32(timeInfo[1]);
                                                        return new TimeSpan(0, minutes, seconds);
                                                    }
                                                )
                                            .Aggregate((current, next) => current + next);
            Console.WriteLine($"total sounds duration: {duration}");

            Console.ReadKey();
        }
    }
}
