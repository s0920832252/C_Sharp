# 計算星期幾的日期

### 描述

* 使用延伸方法擴充DateTime類別 .
* 當你指定一個星期幾 , 此方法會幫你找出`距離指定日期最近的下一個星期幾`

### 可能適用情境

* 當你需要做Schedule的功能的時候
  - 比如說 , 使用者在禮拜一的時候指定禮拜四要執行某件事情. 或者使用者在禮拜四的時候指定禮拜一要執行某件事情.
    就會需要這個函式去計算出相對應的日期.

### 範例
取出距離今天最近的下禮拜一.
```C#
  DateTime dateTime = DateTime.Now;
  DateTime nextWeekDay = dateTime.NextWeekDay(DayOfWeek.Monday);
```
