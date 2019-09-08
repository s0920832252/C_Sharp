## 擴充 LinQ 的 Min 方法.

### 需求
有時候 , 我們不是需要得到集合中指定屬性的最小值 , 我們是需要取得指定屬性的最小值的物件.

例如一群學生 , 我們希望取得這次數學成績最小的學生**物件**.

### 原始碼
```C#
public static TSource MyMin<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
{
     if (source == null) throw new Exception("null source");

     var enumerator = source.GetEnumerator();
     if (enumerator.MoveNext())
     {
          var comparer = Comparer<TKey>.Default;
          var minKey = selector(enumerator.Current);
          var value = enumerator.Current;
          do
          {
               var key = selector(enumerator.Current);
               if (comparer.Compare(minKey, key) > 0)
               {
                    minKey = key;
                    value = enumerator.Current;
               }
          } while (enumerator.MoveNext());
          return value;
     }
     throw new Exception("no item");
}
```
---
#### 補充
雖然你也可以取得最小的物件 , 其實也可以透過 LinQ Aggregate 函數做到啦.