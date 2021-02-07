﻿using System;
using System.IO;

namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            // initial input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            string resp = Console.ReadLine();

            if (resp == "1")
            {
                // create data file
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine());

                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                
                // random number generator
                Random rnd = new Random();

                // create file
                StreamWriter sw = new StreamWriter("data.txt");

                // loop for the desired # of weeks
                while (dataDate < dataEndDate)
                {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++)
                    {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }
                    // M/d/yyyy,#|#|#|#|#|#|#
                    //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                sw.Close();

            }
            else if (resp == "2")
            {
                // TODO: parse data file
                string line;
                using(StreamReader sw = new StreamReader("data.txt"))
                {
                    while((line = sw.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        string date = line.Substring(0, line.IndexOf(","));
                        var parsedDate = DateTime.Parse(date);
                        Console.WriteLine($"Week of {parsedDate:MMM}, {parsedDate:dd}, {parsedDate:yyyy}");
                        string[] sleepTimes1 = line.Split(",");
                        string[] sleepTimes2 = sleepTimes1[1].Split("|");
                        Console.WriteLine("Mo Tu We Th Fr Sa Su\n-- -- -- -- -- -- --");
                        Console.WriteLine($"{sleepTimes2[0]} {sleepTimes2[1]} {sleepTimes2[2]} {sleepTimes2[3]} {sleepTimes2[4]} {sleepTimes2[5]} {sleepTimes2[6]}\n");
                        //Console.WriteLine($"{sleepTimes1[1]}");
                    }
                    sw.Close();
                }
                    
            }
        }
    }
}
