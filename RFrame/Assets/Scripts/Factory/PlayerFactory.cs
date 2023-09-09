using Tool;
using UnityEngine;

public class PlayerFactory : MonoBehaviour,IPlayerFactory
{
    public GameObject GeneratePlayer()
    {
        var prefab = Resources.Load("Prefab/Character/Player");
        var player = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
        player.SetActive(true);
        player.name = "player_only_one"; 
        
        return player;
    }
}