using System.Linq;
using System.Threading.Tasks;

namespace async.Classes
{
    public class Work
    {
        public int NumAggregation(int num)
        {
            int[] intArray = new int[num];
            return Enumerable.Range(1, num)
                            .Select((elem, index) => 
                                intArray[index] = elem)
                            .Aggregate((acc, elem) =>
                                acc + elem);            
        }

        public Task<int> AsyncNumAggregation(int num)
        {
            int[] intArray = new int[num];
            return Task.Run(() => NumAggregation(num));
        }

        public async Task<int> AsyncLongTask(int num)
        {
            await Task.Delay(num);
            return 10;
        }
    }
}