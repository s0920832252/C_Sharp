---
tags: LinQ, LinQ基礎 , C#
---

# LINQ基礎 - IEnumerable & IEnumerator

### 前言
我們在寫程式的時候 , 常會使用到許多資料結構 , 像是List、HashTable、Stack、Tree等等資料結構 , 儘管這些資料結構在本質以及實作上並不相同 , 但我們有時候仍然會希望它們都具有走訪(Traverse)資料的能力

所謂的[走訪(Traverse)](https://www.quora.com/What-is-traversing)就是逐一存取此資料集合下內含的所有資料元素(Element)或節點 (Node) , 不過依據實作的演算法不同 , 走訪的方式也可能會不同. 例如樹狀資料就有前序、中序、後序三種演算法.

--- 

### Enumerator & Enumerable
- Enumerator : a read-only, forward-only cursor over a sequence of values
    * 實作介面
        * System.Collections.IEnumerator
        * System.Collections.Generic.IEnumerator<T>
    * Enumerator是實作MoveNext()、Reset()以及具有屬性Current的物件
    
- Enumerable : the logical representation of a sequence. It is not itself a cursor, but an object that produces cursors over itself
    * 實作介面
        * IEnumerable 
        * IEnumerable<T> 
    * Enumerable必實作一個回傳Enumerator的GetEnumerator() 
    
---

### IEnumerable & IEnumerator

在.Net中 , 若是希望資料集合類別具有資料走訪能力 , 需要實作IEnumerable以及IEnumerator兩個介面(或是它們的泛型版本)

- IEnumerable
    ```C#
    public interface IEnumerable
    {
        IEnumerator GetEnumerator();
    }
    ```
    由IEnumerable這個介面可以知道.Net對於資料集合是否可被列舉(走訪)的定義是其是否具備取得Enumerator(列舉器)的能力 , 換句話說實作介面IEnumerable代表此資料集合中的成員可以被列舉.
    
- IEnumerator
    ```C#
    public interface IEnumerator
    {
        bool MoveNext();
        object Current { get; }
        void Reset();
    }
    ```
    依據介面IEnumerator可以知道Enumerator負責將其所屬的資料集合中的成員 , 逐一取出並回傳. 因此其將實作MoveNext() & Reset()以及具有屬性Current. 請參考下圖.
    
    ![](https://i.imgur.com/ze6pKBH.gif)
    
    - Current屬性 : 回傳**目前**走訪到的成員內容值.
    - MoveNext() : 走訪到下一個成員 , 並回傳bool值來告知向下移動是否成功. 
    - Reset : 重置走訪的位置.
    
---

### 範例

走訪金木水火土五行.

```C#
public class FiveElements : IEnumerable
{
     private string[] fiveElements = { "金", "木", "水", "火", "土" };
     public IEnumerator GetEnumerator() => new FiveElementsEnumerator(fiveElements);
}
```
```C#
public class FiveElementsEnumerator : IEnumerator
{
     private string[] fiveElements;
     private int index = -1;
     public FiveElementsEnumerator(string[] elements) => fiveElements = elements;
     public bool MoveNext() => ++index < fiveElements.Length;
     public object Current => fiveElements[index];
     public void Reset() => index = -1;
}
```
##### 測試程式
![](https://i.imgur.com/9BtSkTR.png)

走訪所有元素的方式有兩種
* 採用 foreach
* 採用 GetEnumerator() 的方式

不過由上面範例可知 , Foreach語法其實就是採用GetEnumerator的方式實作 . 也就是說 foreach 其實是做了這四個步驟
1. 呼叫 fiveelements.GetEnumerator() , 得到一個 IEnumerator.
2. 呼叫 iterator.MoveNext() 以判斷走訪是否結束
    - 如果 IEnumerable 已經走訪完畢，則會回傳 false. 
    - 如果尚未拜訪完 , 則會回傳 true , 並將 Current 指標移到下一個元素上.    
3. 回傳 Current 屬性 .
4. 重複動作 2 & 3

所以下列這一行 , 可以這麼解讀
```C#
foreach (var item in fiveelements)
```
- foreach : 代表要走訪的進入點.
- fiveelements : 呼叫GetEnumerator()
- in : 呼叫MoveNext()
- item :     
    - 若回傳false則代表走訪結束 , 結束迴圈
    - 若回傳true , 走訪尚未結束並回傳Current屬性

---

### .Net繼承關西

>  book - C# 4.0 in Nutshell 
    ![](https://i.imgur.com/TDmJg3G.png)

根據上圖 , 我們可以知道.Net常用的集合介面(ICollection、IList、IDictionary)都有繼承IEnumerable或是IEnumerable<T> , 也因此實作這些集合界面的類別 , 也都具備資料走訪能力.

---

### 總結
1. 自定義的資料集合類別要具備**可被列舉**的能力 , 需實作IEnumerable或是IEnumerable<T>
2. 實作IEnumerable與IEnumerable<T>界面 , 需要實作GetEnumerator() 用來回傳列舉器(Enumerator)物件 - 實作IEnumerator
3. 列舉器(Enumerator)是實作走訪各個資料元素的類別的物件
4. > 引用91大的blog
 ![](https://i.imgur.com/ZepI1wi.png)









---

### Thank you! 

You can find me on

- [GitHub](https://github.com/s0920832252)
- [Facebook](https://www.facebook.com/fourtune.chen)

若有謬誤 , 煩請告知 , 新手發帖請多包涵

# :100: :muscle: :tada: :sheep: 
