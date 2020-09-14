using System;
using System.IO;
using NLog.Web;
namespace SleepData
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            logger.Info("Program started");
            string[] daysOfWeek = {"Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"  };
            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            if (resp == "1")
            {
              // create data file

                 // ask a question
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
                     sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }
                   sw.Close();
            }
            else if (resp == "2")
            {
               StreamReader sr = new StreamReader("data.txt");
                  while(!sr.EndOfStream){
                      string lineOfCode = sr.ReadLine();
                     string[] parsedLineOfCode = lineOfCode.Split(',');
                     string[] totalParsed = parsedLineOfCode[1].Split('|');
                  string decoded = string.Join("  ", totalParsed);
                  string daysOfWeek2 = string.Join(" ", daysOfWeek);
                //   Console.WriteLine(parsedLineOfCode[0]);
              DateTime test = Convert.ToDateTime(parsedLineOfCode[0]);
                Console.WriteLine($"Week of {test:MMM}, {parsedLineOfCode[0]}");
                Console.WriteLine($"{"Su",3}{"Mo",3}{"Tu",3}{"We",3}{"Th",3}{"Fr",3}{"Sa",3}"); // {"Tot",4}{"Avg",4}");
                        Console.WriteLine($"{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}{"--",3}"); // {"---",4}{"---",4}");
                   Console.WriteLine($"{totalParsed[0],3}{totalParsed[1],3}{totalParsed[2],3}{totalParsed[3],3}{totalParsed[4],3}{totalParsed[5],3}{totalParsed[6],3}"); 
                     Console.WriteLine();// {hours.Sum(),4}{Math.Round(hours.Sum() / 7.0, 1),4:n1}");

                  }
            }
              logger.Info("Program ended");
        }
    }
}
