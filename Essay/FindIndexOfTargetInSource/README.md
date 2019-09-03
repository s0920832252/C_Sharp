### 找出子集合存在於母集合中的索引

### 需求
例如我們有一個星期陣列 , 一到七 . (母集合)
但是使用者可能只選擇一星期中的幾天 (子集合)
這時候 , 我們就可以使用這個方式找到的索引集合去存取母集合的值.

### 範例
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
