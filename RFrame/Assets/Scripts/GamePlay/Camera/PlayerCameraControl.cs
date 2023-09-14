using UnityEngine;
using Cinemachine;

public class PlayerCameraControl : MonoBehaviour
{
    private CinemachineVirtualCamera virCamera;
    public Transform playerModule;

    private void Start()
    {
        virCamera = transform.GetComponentInChildren<CinemachineVirtualCamera>();
        Debug.Log("Find Player");
        playerModule = GameObject.FindWithTag("Player").transform;
        
        InitVirtualCamera();
        //DontDestroyOnLoad(gameObject);
    }

    private void InitVirtualCamera()
    {
        virCamera.Follow = playerModule;
        virCamera.LookAt = playerModule;
    }
}
