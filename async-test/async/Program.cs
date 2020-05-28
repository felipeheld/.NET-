using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using async.Classes;
using System.Linq;

namespace async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Work work = new Work();  
            List<int> intList = Enumerable.Range(2, 100).ToList();
            List<int> resultList1;
            List<int> resultList2;
            
            var sw = Stopwatch.StartNew();

            resultList1 = intList.Select(x => {
                Task.Delay(TimeSpan.FromMilliseconds(1000)).GetAwaiter();
                return work.NumAggregation(x);
            }).ToList();
            Console.WriteLine($"sequential - { sw.ElapsedTicks }");

            sw.Restart();
            resultList2 = intList.AsParallel().Select(x => {
                Task.Delay(TimeSpan.FromMilliseconds(1000)).GetAwaiter();
                return work.NumAggregation(x);
            }).ToList();

            Console.WriteLine($"parallel - { sw.ElapsedTicks }");
            resultList2.ForEach(x => Console.WriteLine(x));
            

            /**
             *  Async in parallel vs sequential
             */   
            
            
            // parallel
            var longTask  = work.AsyncLongTask(500);
            var longTask2 = work.AsyncLongTask(500);
            var longTask3 = work.AsyncLongTask(500);
            var longTask4 = work.AsyncLongTask(500);
            var longTask5 = work.AsyncLongTask(500);
            var longTask6 = work.AsyncLongTask(500);
            var longTask7 = work.AsyncLongTask(500);

            Stopwatch stopwatch = Stopwatch.StartNew();

            Console.WriteLine(await longTask);
            Console.WriteLine(await longTask2);
            Console.WriteLine(await longTask3);
            Console.WriteLine(await longTask4);
            Console.WriteLine(await longTask5);
            Console.WriteLine(await longTask6);
            Console.WriteLine(await longTask7);

            Console.WriteLine($"async timestamp - { stopwatch.ElapsedMilliseconds }");

            stopwatch.Restart();

            // sequential
            Console.WriteLine(await work.AsyncLongTask(500));
            Console.WriteLine(await work.AsyncLongTask(500));
            Console.WriteLine(await work.AsyncLongTask(500));
            Console.WriteLine(await work.AsyncLongTask(500));
            Console.WriteLine(await work.AsyncLongTask(500));
            Console.WriteLine(await work.AsyncLongTask(500));
            Console.WriteLine(await work.AsyncLongTask(500));

            Console.WriteLine($"sync timestamp - { stopwatch.ElapsedMilliseconds }");
            
            /**
             *  Sync wrapped in task
             */

            // Sync
            stopwatch.Restart();

            Console.WriteLine(work.NumAggregation(1000000));
            Console.WriteLine(work.NumAggregation(1000000));
            Console.WriteLine(work.NumAggregation(1000000));
            Console.WriteLine(work.NumAggregation(1000000));

            Console.WriteLine($"Sync - { stopwatch.ElapsedMilliseconds }");

            stopwatch.Restart();

            // Async
            var asyncAggregation  = work.AsyncNumAggregation(1000000);
            var asyncAggregation1 = work.AsyncNumAggregation(1000000);
            var asyncAggregation2 = work.AsyncNumAggregation(1000000);
            var asyncAggregation3 = work.AsyncNumAggregation(1000000);

            Console.WriteLine(asyncAggregation.GetAwaiter().GetResult());
            Console.WriteLine(asyncAggregation1.GetAwaiter().GetResult());
            Console.WriteLine(asyncAggregation2.GetAwaiter().GetResult());
            Console.WriteLine(asyncAggregation3.GetAwaiter().GetResult());

            Console.WriteLine($"Task based async -  { stopwatch.ElapsedMilliseconds }");
        }
        
    }
}
