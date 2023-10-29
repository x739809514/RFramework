# RFramework

这个框架主要用于给刚开始学做游戏的初学者，里面提供了简单的事件分发，存储，场景切换等系统。初学者可以在这个框架的基础上构建自己的2D游戏。同时还可以学习相关设计模式的知识。本篇说明也会简单讲解框架中的设计模式和部分系统的使用方法。
大致讲一下该框架的使用方法：

1.  游戏的入口的场景为`Stable`, 在 Scenes 文件夹中
2.  游戏的所有脚本都在 Scripts 中，关卡数据和玩家数据放在 Data 中
3.  代码文件：Character 文件夹中是角色相关的逻辑， Factory中是工厂模式相关代码，GamePlay中都是游戏逻辑相关代码，其中包括事件等系统相关代码和场景切换逻辑代码，还有简单的gameobject上的逻辑，如子弹等。
4.  UI文件夹中是UI框架。

## 事件分发

框架中的事件分发是观察者模式的体现，游戏中的观察者模式主要通过delegate实现，通过在delegate中绑定对应的事件链，然后调用delegate的invoke()函数执行对应的一连串事件函数。在”Events” 文件夹中，`EventSender` 这个类是事件系统的主体函数，在其中我设置了各种具体发送事件的函数，而`EventSystem` 是对`EventSender` 的一层封装，整个类是一个静态类，因为在整个项目中只需要有一个事件系统。`EventEnumType` 则是具体事件的枚举类。

### 使用方法

首先在`EventEnumType`中注册你所需要的事件枚举，然后在你所需要的观察者脚本中添加该事件的监听（也就是在被调用函数所在的脚本中添加），最后在被观察者脚本（也就是事件触发的地方）调用`EventSystem.Dispatch`.
```c#
//注册
public enum EventEnumType
{
    Null = 0,
    PlayerAttackDamageEvent = 1, //玩家攻击
    EnemyGetHitEvent = 2, //敌人受到伤害
    BattleSettlementEvent = 3, //滴人死亡
    GameStartEvent = 4, //游戏开始事件
    GameEndEvent = 5, //游戏结束事件
}
//绑定
private void AddListener()
{
    EventSystem.AddListener(EventEnumType.PlayerAttackDamageEvent, CommonDamage);
}
//调用
EventSystem.Dispatch(EventEnumType.PlayerAttackDamageEvent,go.transform);
```

## 单例模式

在游戏中单例模式是一个会经常被用到的设计模式，一般当我们想要保持某一个系统只需要一个实例的时候就可以使用单例模式，但是切记单例模式不能滥用，否则会造成严重的代码耦合，造成后期维护困难。
单例模式的使用也非常简单，主要有通过static实现。在该框架中，我实现了两种泛型单例模式，一个是关于mono的，另一个是带构造函数的。但是我所实现的是懒汉式写法，感兴趣的同学可以自己优化。
```c#
//mono
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;

    public static T Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = (T)this;
    }
}
```
```C#
public class Singleton<T> where T : new()
{
    private T instance;

    public T Instance => instance ??= new T();
}
```

## 对象池

在游戏中，我们常常会需要大量的创建对象，但是new 和 destroy一个对象会涉及内存的分配和回收，一两个没什么问题，但是如果需要大量的创建，则会造成非常大的内存消耗，比如射击时的子弹，UI面板等。所以这个时候我们就需要一个对象池来存放所创建的对象，循环利用这些对象，来达到优化性能的效果。
对象池实现起来也非常的简单，我们使用一个队列来存储所创建的对象，并开放两个函数让外部获取对象，同时我们还需要让这个对象池是一个泛型类，以便针对不同类型的对象。
```C#
private Queue<T> objectPool; //使用队列（先进先出）

public T Spawn()
{
    if (objectPool.Count == 0)
    {
        CreatePool(RETAIL_COUNT);
    }
    var t = objectPool.Dequeue();
    // do something
    return t;
}

public void Recycle(T t)
{
    t.gameObject.SetActive(false);
    objectPool.Enqueue(t);
}
```

### 使用方法

```C#
//初始化
ObjectPool<PlayerBullets>.Instance.InitPool(player.bulletPool, player.bullet);
//取出
var bullet = ObjectPool<PlayerBullets>.Instance.Spawn();
//回收
ObjectPool<PlayerBullets>.Instance.Recycle(playerBullet);
```

## 工厂模式

工厂模式一般用来创建对象，是一种为访问类提供一个创建一组相关或相互依赖对象的接口，且访问类无须指定所要产品的具体类就能得到同族的不同等级的产品的模式结构。在该框架中我有两个敌人，`Goblin`,`Orc`, 这两个属于同一个角色族，另外还有一个`player`, 这属于另一个角色族，所以我定义了一个角色工厂，并又分别定义了敌人工厂`IEnemyFactory`和玩家工厂`IPlayerFactory`来分别创建敌人和怪物。这种模式的方便之处在于可以为日后的扩展带来方便，假设我如后想要添加一个精灵族类，则只需要实现一个精灵工厂就能创建各种各样的精灵。
![image](https://github.com/x739809514/RFramework/assets/53636082/8367c8ba-1246-418e-b884-996f01811bfb)
```C#
//首先实现一个具体的工厂接口
public interface IPlayerFactory : IFactory
{
    public GameObject GeneratePlayer();
}
//实现具体的工厂逻辑
public class PlayerFactory : MonoBehaviour,IPlayerFactory
{
    public GameObject GeneratePlayer()
    {
        var prefab = Resources.Load("Prefab/Character/Player");
        // do something 
        return player;
    }
}
```

### 使用方法

```C#
var factory = gameObject.AddComponent<PlayerFactory>(); //首先添加脚本
var player = factory.GeneratePlayer(); //创建角色
```

## 存储系统

存储系统主要使用了MVC架构，并使用NewtonJson来实现json的存储。在框架中，我定义了一个`ISaveHandler`,在里面实现了注册函数，并定义了两个函数`GenerateSaveData`, `LoadSaveData`, 分别为了创建存储数据对象，和读取数据对象。在SaveManager中是时存储逻辑和load逻辑。

### 使用方法

只要在需要存储数据的类中实现`ISaveHandler`接口，并注册即可
```C#
//定义一个存储数据结构
public partial class SaveData
{
    public string levelName;
}
//注册
ISaveHandler handler = this;
handler.DoRegister(this);
//接口实现
public SaveData GenerateSaveData()
{
    var data = new SaveData();
    data.levelName = curScene;
    return data;
}

public void LoadSaveData(SaveData saveData)
{
    curScene = saveData.levelName;
    Debug.Log("Load Scuess");
}
```

## 场景切换及场景加载

场景加载器也是通过MVC实现的，通过给场景挂`ChangeScene`组件，我设立了from和to两个字段作为场景变量，然后选定进出场景就可以实现场景切换。

### 使用方式

![image](https://github.com/x739809514/RFramework/assets/53636082/8841f2a3-8a64-4ca5-a4a5-3f8a6f807098)

### 场景初始化

在该框架中我是用scriptobjectable来存储场景初始化信息，在`LevelData`中你可以看到具体的场景设定数据，比如怪物的初始位置，角色的初始位置，相机碰撞等，你可以通过设定不同的LevelData来自己定制不同的场景。

![image](https://github.com/x739809514/RFramework/assets/53636082/8e6229bb-e367-4e82-b296-d97464de1ebf)

## UI框架

在本框架中没有使用热更框架，如果想了解热更相关可以查看我的另一个ILRuntime项目, 所以这里的UI框架是在主工程实现的。`UIMoudle`中是具体的UI切换逻辑，`PanelBase`中开放了几个UI面板中会用到的函数，另外`BaseContent`中存放了`UIType`对象，在`UIType`中存放了path等属性，同时在`UIMoudle`中，我定义了一个以UIType位Key, PanelBase为value的字典，方便索引面板。最后UI面板的脚本是事先挂在面板prefab上的（这种方式其实并不好，读者可以查看ILRuntime的那个项目，使用那其中的UI框架）。

### 使用方法

```C#
//注册面板
public static readonly UIType Panel_Hud = new UIType("Panel_Hud");
//实现具体面板的content
public class Panel_Hud_Content : BaseContent
{
    public Panel_Hud_Content() : base(UIType.Panel_Hud)
    {
    }
}
//实现具体面板
public class Panel_Hud : PanelBase
{
}
```
### ILR_PROJ LINK

ILR_Project: https://github.com/x739809514/UHotFixProject
