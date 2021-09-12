---
tags: 筆記範本
---

# 控制反轉(Inversion of Control)與依賴注入(Dependency Inversion)
## 前言
話講在前頭 , 每個人對於控制反轉和依賴注入的見解不同 , 所以這也只是我自己閱讀相關的網路文章和書籍後所得到的認知 , 當然我都會盡可能附上我參考的出處 (如果忘記 , 還請作者提醒我一下.)
總之 , 有一百個人那就有一百種巴哈姆特. 所以如果你認為我有哪裡寫錯 , 歡迎跟我討論.
謝謝.

### 當我們使用控制反轉與依賴注入能對我們的程式碼帶來哪些好處呢!?
我認為可能會有以下的好處.
- 低耦合 (Loose Coupling)
    - **實例依賴物件的『控制流程 (Control Flow)』由主動轉為被動** , 意即高位模組不再需要主動去建立低位模組 , 而是透過 IOC 容器給予. 
    - **很可能**可以達成依賴反轉原則(Dependency Inversion Principle)對於程式碼的要求
- 可維護性提高 (Maintainability)
    - 使用 IOC & DI , 你通常可以很好的達成開放封閉原則(Open-Closed Principle)對我們程式碼的要求. 意即對於**功能的擴充**是相對容易的.
- 可測試性 (Testability)
    - 使用 IOC & DI 時通常會順便遵守依賴反轉原則(Dependency Inversion Principle). 而一個達成 DIP 要求的程式 , 可測試性通常是不用擔心的.
- 可替換性 (pluggability)
    - 理由同上

## 控制反轉(Inversion of Control)
IOC 是一種設計程式的概念 , 用來降低

#### [IOC](https://en.wikipedia.org/wiki/Inversion_of_control)


>In traditional programming, the flow of the business logic is determined by objects that are statically bound to one another. With inversion of control, the flow depends on the object graph that is built up during program execution.Such a dynamic flow is made possible by object interactions that are defined through abstractions.

翻成白話文是說**反轉控制流程的關鍵在於高位模組取得低位模組時是使用靜態綁定(Statically binding)和動態綁定(Dynamic binding)哪一個**

#### 靜態封裝(Statically Bound or Early Binding)
```C#
// 程式碼在編譯時 , 就知道其物件的真實型別.
var dog = new Dog();
```
#### 動態綁定(Dynamic Binding or Late Binding)
```C#
// 程式碼在執行時 , 才知道其物件的真實型別.
var dog = CreateDogFactory(typeof(哈士奇));

public IDog CreateDogFactory<T>(Type dogType)
    => Activator.CreateInstance(dogType) as IDog
```




---

---


- 簡單IOC 程式碼
- [IOC 的批評](https://www.theserverside.com/definition/inversion-of-control-IoC)
> The IoC design principle does not specify a problem domain within which it is meant to operate. It is not precisely clear what is meant by "control," and IoC does not specify where an application's control is best inverted to.
Further, IoC makes no guarantees about the benefits its implementation will have. The IoC design principle simply asserts that benefits may accrue if you invert the flow of an application.






---

###### Thank you! 

You can find me on

- [GitHub](https://github.com/s0920832252)
- [Facebook](https://www.facebook.com/fourtune.chen)

若有謬誤 , 煩請告知 , 新手發帖請多包涵

# :100: :muscle: :tada: :sheep: 
