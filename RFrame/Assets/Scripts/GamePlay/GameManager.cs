using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class GameManager : SingletonMono<GameManager>
{
    //Test
    public Button button;
    
    
    private void Start()
    {
        EventSystem.Dispatch(EventEnumType.GameStartEvent);
        button.onClick.AddListener(CloseGame);
    }

    private void CloseGame()
    {
        EventSystem.Dispatch(EventEnumType.GameEndEvent);
        Application.Quit();
    }
}