```C#
static void Main(string[] args)
{
     List<string> source = new List<string> { "H", "Q", "T", "S" };
     List<string> target = new List<string> { "Q", "S" };
     var query = source.Select((num, index) => target.Contains(num) ? index : -1).Where(index => index != -1);
     foreach (var indexInSource in query)
     {
          Console.WriteLine(indexInSource);
     }

     Console.ReadKey();
}
```
輸出結果 
![](https://i.imgur.com/qNpJGP5.png)
