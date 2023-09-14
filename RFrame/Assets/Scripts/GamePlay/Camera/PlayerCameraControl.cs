using UnityEngine;
using Cinemachine;

public class PlayerCameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera virCamera;
    public Transform playerModule;

    private void Start()
    {
        virCamera = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        playerModule = GameObject.Find("player_only_one").transform;
        
        InitVirtualCamera();
        //DontDestroyOnLoad(gameObject);
    }

    private void InitVirtualCamera()
    {
        virCamera.Follow = playerModule;
        virCamera.LookAt = playerModule;
    }
}
