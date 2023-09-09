using Tool;
using UnityEngine;

public interface IPlayerFactory : IFactory
{
    public GameObject GeneratePlayer();
}