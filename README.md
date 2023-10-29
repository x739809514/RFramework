# RFramework

This framework is mainly used for beginners who are just learning how to make games. It provides a simple system for event distribution, storage, scene switching, etc. The framework allows beginners to build their own 2D games based on this framework. Beginners can build their own 2D games based on this framework. At the same time, you can also learn the knowledge of related design patterns. This note will also briefly explain the design patterns in the framework and the use of some systems.
Roughly speaking, how to use the framework:

1. the entry scene of the game is `Stable`, in the Scenes folder
2. all scripts of the game are in Scripts, level data and player data are in Data.
3. code files: Character folder is character related logic, Factory is factory mode related code, GamePlay is game logic related code, including events and other system related code and scene switching logic code, and simple gameobject logic, such as bullets. 4. UI folder is UI folder.
4. UI folder is UI framework.

## Event distribution

The event distribution in the framework is the embodiment of the observer pattern, which is mainly realized through the delegate, by binding the corresponding event chain in the delegate, and then calling the invoke() function of the delegate to execute the corresponding chain of event functions. In the "Events" folder, the class `EventSender` is the main function of the event system, in which I set up various specific functions to send events, while `EventSystem` is a layer of encapsulation of `EventSender`, the whole class is a static class. The whole class is a static class because there is only one event system needed in the whole project. The `EventEnumType` is an enumeration class for specific events.

### Usage

First register the event enumeration you need in `EventEnumType`, then add a listener for the event in the observer script you need (i.e., in the script where the called function is located), and finally call `EventSystem.Dispatch` in the observed script (where the event is triggered).
```c#
//Register
public enum EventEnumType
{
    Null = 0,
    PlayerAttackDamageEvent = 1, //Player attacks
    EnemyGetHitEvent = 2, //Enemy takes damage
    BattleSettlementEvent = 3, //Dropper dies
    GameStartEvent = 4, //Game start event
    GameEndEvent = 5, //game end event
}
//Binding
private void AddListener()
{
    EventSystem.AddListener(EventEnumType.PlayerAttackDamageEvent, CommonDamage);
}
// Call
EventSystem.Dispatch(EventEnumType.PlayerAttackDamageEvent, go.transform);
```

## Singleton pattern

The singleton pattern is a design pattern that is often used in games, generally when we want to keep a system with only one instance we can use the singleton pattern, but remember that the singleton pattern should not be abused, otherwise it will cause serious code coupling, resulting in later maintenance difficulties.
The use of the singleton pattern is also very simple, mainly through the static realization. In the framework, I realized two generalized singleton pattern, one is about mono, the other is with constructor. But what I have implemented is written in a lazy way, interested students can optimize it by themselves.
```c#
//mono
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance.

    public static T Instance => instance; public static T Instance => instance.

    private void Awake()
    {
        if (instance ! instance => instance; private void Awake() { if (instance !
        {
            Destroy(instance); }
        }

        instance = (T)this; }
    }
}
```
```C#
public class Singleton<T> where T : new()
{
    private T instance.

    public T Instance => instance ? = new T();
}
```

## Object pooling

In games, we often need to create a large number of objects, but new and destroy an object will involve memory allocation and recycling, one or two is not a problem, but if you need to create a large number of objects, it will cause a very large memory consumption, such as bullets, UI panels and so on. So this time we need an object pool to store the created objects, recycling these objects to achieve the effect of optimizing performance.
Object pooling is very simple to implement, we use a queue to store the created objects, and open two functions to allow external access to the objects, and we also need to make the object pool a generic class to target different types of objects.
```C#
private Queue<T> objectPool; //Use the queue (FIFO)

public T Spawn()
{
    if (objectPool.Count == 0)
    {
        CreatePool(RETAIL_COUNT);
    }
    var t = objectPool.Dequeue(); // do something.
    // do something
    return t; } var t = objectPool.Dequeue(); // do something
}

public void Recycle(T t)
{
    t.gameObject.SetActive(false); objectPool.Enqueue(t); } public void Recycle(t) { t.gameObject.
    objectPool.Enqueue(t); }
}
```

### Usage

```C#
// Initialization
ObjectPool<PlayerBullets>.Instance.InitPool(player.bulletPool, player.bullet);
//Remove
var bullet = ObjectPool<PlayerBullets>.Instance.Spawn();
//Recycle
ObjectPool<PlayerBullets>.Instance.Recycle(playerBullet);
```

## Factory Pattern

The factory pattern is generally used to create objects, and is a pattern structure that provides an interface for an accessing class to create a set of related or interdependent objects, and the accessing class does not have to specify the specific class of the desired product in order to get a different class of products of the same family. In this framework I have two enemies, `Goblin`,`Orc`, which belong to the same character family, and one `player`, which belongs to another character family, so I defined a character factory and in turn defined an enemy factory, `IEnemyFactory`, and a player factory, `IPlayerFactory`, to create respectively the enemies and monsters respectively. The convenience of this pattern is that it can be easily extended later. Suppose I want to add an elf class later, I only need to implement an elf factory to create all kinds of elves.
! [image](https://github.com/x739809514/RFramework/assets/53636082/8367c8ba-1246-418e-b884-996f01811bfb)
```C#
// First implement a specific factory interface
public interface IPlayerFactory : IFactory
{
    public GameObject GeneratePlayer();
}
//Implement the concrete factory logic
public class PlayerFactory : MonoBehaviour,IPlayerFactory
public class PlayerFactory : MonoBehavior,IPlayerFactory {
    public GameObject GeneratePlayer()
    var GameObject GeneratePlayer() {
        var prefab = Resources.Load("Prefab/Character/Player"); // do something.
        // do something 
        return player; }
    }
}
```

### Usage

```C#
var factory = gameObject.AddComponent<PlayerFactory>(); // first add the scripts
var player = factory.GeneratePlayer(); //create the player
```

## Storage system

The storage system mainly uses MVC architecture and uses NewtonJson for json storage. In the framework, I defined an `ISaveHandler`, which implements the registration function, and defined two functions `GenerateSaveData`, `LoadSaveData`, in order to create the storage data object, and read the data object respectively. In SaveManager, it is the store logic and load logic.

### Usage

Just implement the `ISaveHandler` interface in the class that needs to store the data and register it.
```C#
//Define a save data structure
public partial class SaveData
{
    public string levelName; }
}
// register
ISaveHandler handler = this; handler.
handler.DoRegister(this); //Interface implementation; }
//Interface implementation
public SaveData GenerateSaveData()
{
    var data = new SaveData(); data.levelName = curScene; }
    data.levelName = curScene; return data; var data = new SaveData(); data.levelName = curScene
    data = new SaveData(); data.levelName = curScene; return data; }
}

public void LoadSaveData(SaveData saveData)
} public void LoadSaveData(SaveData saveData)
    curScene = saveData.levelName; } public void LoadSaveData(SaveData saveData) { curScene = saveData.
    Debug.Log("Load Scuess"); }
}
```

## Scene switching and scene loading

The scene loader is also implemented through MVC, by hooking the `ChangeScene` component to the scene, I set up the from and to fields as scene variables, and then selecting in and out of the scene will enable scene switching.

### How to use

! [image](https://github.com/x739809514/RFramework/assets/53636082/8841f2a3-8a64-4ca5-a4a5-3f8a6f807098)

### Scene initialization

In this framework, I use scriptobjectable to store the scene initialization information, in the `LevelData` you can see the specific scene setting data, such as the initial position of the monster, the initial position of the character, the collision of the camera and so on, you can customize your own scene by setting different LevelData.

! [image](https://github.com/x739809514/RFramework/assets/53636082/8e6229bb-e367-4e82-b296-d97464de1ebf)

## UI framework

There is no hotshift framework used in this framework, if you want to know about hotshift you can check out my other ILRuntime project, so the UI framework here is implemented in the main project. In `UIMoudle`, there is a specific UI switching logic, in `PanelBase`, there are several functions that will be used in the UI panel, and in `BaseContent`, there is a `UIType` object, and in `UIType`, there are properties such as path etc. Meanwhile, in `UIMoudle`, I have defined a UI panel that uses UIType as the key, PanelBase as the key, and `UIMoudle` as the panel. bitKey, PanelBase as value to easily index the panels. Finally the UI panel script is pre-hosted on the panel prefab (this is not really a good way to do it, readers can check out that ILRuntime project and use the UI framework there).

### Usage

```C#
// Register the panel
public static readonly UIType Panel_Hud = new UIType("Panel_Hud");
// Realize the content of the specific panel
public class Panel_Hud_Content : BaseContent
{
    public Panel_Hud_Content() : base(UIType.Panel_Hud)
    {
    }
}
// Realize the specific panel
public class Panel_Hud : PanelBase
{
}
```
### ILR_PROJ LINK

ILR_Project: https://github.com/x739809514/UHotFixProject
