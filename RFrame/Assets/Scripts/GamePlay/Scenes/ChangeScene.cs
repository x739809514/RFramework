using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SceneName] public string from;
    [SceneName] public string to;

    public void Teleport()
    {
        ChangeSceneSystem.Instance.OnTeleport(from, to);
    }
}