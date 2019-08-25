using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDateTimeExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.NextWeekDay(DayOfWeek.Sunday));
            Console.ReadKey();
        }
    }

    public static class CityDateTimeExtension
    {
        public static DateTime NextWeekDay(this DateTime from, DayOfWeek end)
        {
            int addDays = (int)end - (int)from.DayOfWeek;
            return from.AddDays(addDays <= 0 ? addDays + 7 : addDays);
        }
    }
}
