---
tags: LinQ, LinQ基礎 , C#
---

# LINQ基礎 - Iterator(疊代器模式)

### 閒話543
最近開始寫LinQ的筆記後才發現LinQ與foreach在設計模式的實作上 , 原來都有採用Iterator(疊代器模式)呀 , 也難怪我好像很久沒使用foreach了QAQ. 忘記在哪本書裡面看到的 , 它說: Iterator由於幾乎被大多數程式語言內化 , 固此模式的學習價值遠高於實戰價值 , Gof甚至曾經提出要刪除此模式...

---

### 定義
> 提供一種方法可以讓使用者能夠走訪集合對象中的各個元素 , 但又不必暴露該對象的內部實現.

> 單一設計原則(SRP) - 一個類別最好只負責一個責任 , 只有一個修改它的原因.

我們固然能夠在集合對象中實作走訪的操作 , 但這樣集合對象可能就承擔過多的責任(?). 也就違反設計模式中的單一設計原則(SRP) , 因此我們要盡可能的分離職責 , 讓不同的類別承擔不同的責任. 而Iterator模式就是用Iterator類別來承擔順序走訪的責任.

---

### 結構
![](https://i.imgur.com/67DuZJI.png)
- Iterator
  負責定義走訪方法的介面 - 通常會有MoveNext、CurrentItem、Fisrt、IsDone、Reset之類的方法或是屬性讓子類實現.
- Aggregate
  將Iterator封裝給Client使用的介面
- ConcreteIteraror
  具體實現Iterator.
- ConcreteAggregate 
  具體實現Aggregate.
---
### 範例 - 走訪整數

- Aggregate
    ```C# 
    public interface IAggregate
    {
        IntIterator GetIterator();
    }
    ```
- ConcreteAggregate
    ```C# 
    public class Integers : IAggregate
    {
        private readonly int _integers;
        public Integers(int integers)
        {
            _integers = integers;
        }
        public IntIterator GetIterator() => new IntIterator(_integers);
    }
    ```
- Iterator
    ```C#
    public interface INtegerIterator
    {
        object Current { get; }
        bool MoveNext();
        void Reset();
    }
    ```
- ConcreteIteraror
    ```C#    
    public class IntIterator : INtegerIterator
    {
        private readonly int _integers;
        private readonly int _maxDigit;
        private int _currentFilter;
        public object Current => (_integers / _currentFilter) % 10;
        public IntIterator(int integers)
        {
            _integers = integers;
            _maxDigit = (int)Math.Log10(integers) + 1;
            Reset();
        }

        public void Reset() => _currentFilter = (int)Math.Pow(10, _maxDigit);

        public bool MoveNext()
        {
            _currentFilter /= 10;
            bool hasNext = _currentFilter > 0;
            if (!hasNext) Reset();
            return hasNext;
        }
    }
    ```
- Client
    ```C#
    static void Main(string[] args)
    {
         Integers integers = new Integers(543838626);
         var iterator = integers.GetIterator();
         while (iterator.MoveNext())
         {
              Console.Write($"{ iterator.Current} ");
         }
         Console.WriteLine();
         while (iterator.MoveNext())
          {
               Console.Write($"{ iterator.Current} ");
          }
          Console.ReadKey();
     }
    ```
- 輸出結果
    ![](https://i.imgur.com/4Q6R9nx.png)

---
### .Net的Iterator
在.Net的System.Collections命名空間內 , 早就具有Iterator模式的實現 . 也就是Aggregate以及Iterator這兩個介面的存在.
- IEnumerator(扮演Iterator的角色) - 定義如何走訪
    ```C#
    public interface IEnumerator
    {
         object Current { get; }
         bool MoveNext();
         void Reset();
    }
    ```
- IEnumerable(扮演Aggregate的角色) - 若需要資料集合具有走訪功能 , 就必須實現此介面.
    ```C#
    public interface IEnumerable
    {
        IEumerator GetEnumerator();
    }
    ```
    C#大部分的資料結構都有實作IEnumerable介面 , 像是List
    ![](https://i.imgur.com/bUIHiwl.png)

    
所以上述範例 - 走訪整數 , 更改下列動作後 , 就可以使用foreach關鍵字.
> public class IntIterator : **IEnumerator**

> public class Integers : **IEnumerable**

> public **IEnumerator GetEnumerator()** => new IntIterator(_integers);

#### 測試端程式以及輸出結果(我用了foreach)

![](https://i.imgur.com/lAAFKCo.png)

---
### 總結

- Iterator模式使得走訪一個資料集合的時候 , 無須暴露它的內部細節.
- Iterator模式為走訪不同的資料結構提供一個統一的接口. 因此同樣的演算法可在不同的資料結構上操作.
- Iterator模式在走訪的時候更改Iterator所在的資料集合會拋出例外 , 所以foreach只能用在走訪 , 而不能在走訪的時候同時修改資料集合內的元素.

Iterator模式是一個**抽象Iterator類來分離集合的走訪行為** , 為了可以做到不暴露集合內部結構同時又可讓外部使用者走訪集合內部的資料.

---
### Thank you! 

You can find me on

- [GitHub](https://github.com/s0920832252)
- [Facebook](https://www.facebook.com/fourtune.chen)

若有謬誤 , 煩請告知 , 新手發帖請多包涵

# :100: :muscle: :tada: :sheep: 
