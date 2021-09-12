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
    - 設定低位模組的責任將不再由高位模組負責. 意即高位模組所需要背負的責任減少.
- 可維護性提高 (Maintainability)
    - 使用 IOC & DI , 你通常可以很好的達成開放封閉原則(Open-Closed Principle)對我們程式碼的要求. 意即對於**功能的擴充**是相對容易的.
- 可測試性 (Testability)
    - 使用 IOC & DI 時通常會順便遵守依賴反轉原則(Dependency Inversion Principle). 而一個達成 DIP 要求的程式 , 可測試性通常是不用擔心的.
- 可替換性 (pluggability)
    - 理由同上

## 控制反轉(Inversion of Control)

#### [維基 IOC 定義](https://en.wikipedia.org/wiki/Inversion_of_control)
> In software engineering, inversion of control (IoC) is a programming principle. IoC inverts the flow of control as compared to traditional control flow. In IoC, custom-written portions of a computer program receive the flow of control from a generic framework. A software architecture with this design inverts control as compared to traditional procedural programming: in traditional programming, the custom code that expresses the purpose of the program calls into reusable libraries to take care of generic tasks, but with inversion of control, it is the framework that calls into the custom, or task-specific, code.

#### IOC 是一種設計程式的概念 , 目標是反轉高位模組對於依賴低位模組的『**控制流程 (Control Flow)**』.
舉個例子 : A 物件裡有使用 B 物件 , 為 A 依賴 B 的關係 , 在傳統的作法上 , A 直接對 B 做流程控制才能使用 B , 這會讓 A 和 B 有一定程度的耦合 , 所以 IOC 希望透過某種方式反轉這樣的控制關係. 讓 A 不需要對 B 做流程控制. 但又可以使用 B.

> In traditional programming, the flow of the business logic is determined by objects that are statically bound to one another. With inversion of control, the flow depends on the object graph that is built up during program execution. Such a dynamic flow is made possible by object interactions that are defined through abstractions. This run-time binding is achieved by mechanisms such as dependency injection or a service locator. In IoC, the code could also be linked statically during compilation, but finding the code to execute by reading its description from external configuration instead of with a direct reference in the code itself.

#### 反轉控制流程的關鍵在於高位模組取得低位模組是使用靜態綁定(Statically binding)還是動態綁定(Dynamic binding) 哪一種方式!?

##### 靜態綁定(Statically Bound or Early Binding)
```C#
// 程式碼在編譯時 , 就知道其物件的真實型別.
public class 高位模組 {
    public 高位模組(){
        IDog dog = new 哈士奇();
    }
}
```
##### 動態綁定(Dynamic Binding or Late Binding)
```C#
// 程式碼在執行時 , 才知道其物件的真實型別.
public class 高位模組 {
    public 高位模組(){
        IDog dog = new CreateDogFactory().CreateDog(typeof(哈士奇);
    }
}

public class CreateDogFactory {
    public IDog CreateDog(Type dogType) 
        => (IDog)Activator.CreateInstance(dogType);
}
```

#### IOC 只是一個設計程式的概念
IOC 只是一個設計程式的概念 , 代表實現它的方式非常多 , 像是 [Service Locator Pattern](https://en.wikipedia.org/wiki/Service_locator_pattern) 或是 [Factory Pattern](https://en.wikipedia.org/wiki/Factory_method_pattern) 以及 [Dependency injection](https://en.wikipedia.org/wiki/Dependency_injection) 等等方式都可能實現它 , 只要這種實現方式可以讓高位模組不需要對低位模組作流程控制 , 就可以稱這是一種 IOC. 目前比較流行的實現方式是將流程的控制交給第三方 (通常是 IOC 容器) 專門負責 (我覺得這幾乎已經流行到可以直接當作 IOC 的定義).
- 舉個例子 : A 物件裡有使用 B 物件 , 為 A 依賴 B 的關係. 但 A 不需要自己設定(new) B , 而是透過 IOC 容器 C 設定 B 後 , 在丟給 A.

#### 所以為什麼要用 IOC ?!


#### 簡單 IOC 容器範例
##### IOC 容器實作方式
```C#
public class MyContainer
{
    private readonly Dictionary<Type, Type> _types = new Dictionary<Type, Type>();

    public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        => _types[typeof(TInterface)] = typeof(TImplementation);

    public object Create(Type type)
    {
        var defaultConstructor = _types[type].GetConstructors()[0]; //Find a default constructor using reflection
        var defaultParams = defaultConstructor.GetParameters(); //Verify if the default constructor requires params
        var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray(); //Instantiate all constructor parameters using recursion
        return defaultConstructor.Invoke(parameters);
    }
}
```
##### IOC 容器呼叫方式
```C#
public interface IMyApp
{
    void OutputString(string name);
}

public class MyApp : IMyApp
{
    private readonly IWriter _writer;
    public MyApp(IWriter writer) => _writer = writer;
    public void OutputString(string name) => _writer.Write($"Hello {name}!");
}

public interface IWriter
{
    void Write(string s);
}

public class ConsoleWriter : IWriter
{
    public void Write(string s) => Console.WriteLine(s);
}

internal static class Program
{
    private static void Main(string[] args)
    {
        // Create IOC Container
        var container = new MyContainer();
        // Register Type
        container.Register<IWriter, ConsoleWriter>();
        container.Register<IMyApp, MyApp>();
        // Get MyApp Instance from IOC Container
        // 高位模組不需要自己 new MyApp 以及 ConsoleWriter 以及設定它們. 就可以使用它們
        var myApp = container.Create(typeof(IMyApp)) as IMyApp;
        myApp.OutputString("QQQ");
        Console.ReadKey();
    }
}
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
