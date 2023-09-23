public class GameManager : SingletonMono<GameManager>
{
    private void Start()
    {
        EventSystem.Dispatch(EventEnumType.GameStartEvent);
        UIModule.Instance.OpenPanel(new Panel_Hud_Content());
    }
}