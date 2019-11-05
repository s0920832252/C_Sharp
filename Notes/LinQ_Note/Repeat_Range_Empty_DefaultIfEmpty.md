---
tags: LinQ , C# , Generation Operators
---

# Repeat  & Range & Empty & DefaultIfEmpty
Generation Operators 可以幫助我們快速產生常見樣式的資料集合. 也就是說這些方法不需要提供類似型式的資料集合作為方法的輸入參數 , 並且回傳會 IEnumerable<T> 型態的資料集合. 例如 : 產生一個陣列後不需要手動地透過迴圈去塞值 , 而是可以透過一個方法產生這樣的陣列.

隸屬於 Generation Operators 的方法有
1. Repet ( Enumerable 類別內的**靜態**方法 )
1. Range ( Enumerable 類別內的**靜態**方法 )
1. Empty ( Enumerable 類別內的**靜態**方法 )
1. DefaultIfEmpty ( Enumerable 類別內的擴充方法 )

### Enumerable.Repeat 
Enumerable.Repeat 方法可以產生指定數量重複值的序列.
```C#
public static IEnumerable<TResult> Repeat(TResult element, int count);
```
- element : 需要重複的值
- count : 重複的數量
#### 使用時機
- 需要快速產生具有重複值的序列.
    - 例如 : 快速初始化一個裡面全部塞某個值的陣列.
        - var array = new int[]{1,1,1,1,1} 
           雖然我們可以透過上述語法初始化一個陣列 , 但卻必須自己手動的key 入內容值. 但使用 Repeat 方法卻僅需要填入兩個參數即可 (雖然型態是 Enumerable , 之後需要再使用 toArray() ) , 並且陣列大小將是動態決定的 , 由第二個參數 count 來決定.
#### Repeat 的用法

##### 輸出結果
#### 簡單實作自己的 Repeat

### Range
### Empty
### DefaultIfEmpty
### Summary
1.  Initialize an array 







---

### Thank you! 

You can find me on

- [GitHub](https://github.com/s0920832252)
- [Facebook](https://www.facebook.com/fourtune.chen)

若有謬誤 , 煩請告知 , 新手發帖請多包涵

# :100: :muscle: :tada: :sheep: 
