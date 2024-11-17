using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        // Find the Cinemachine Virtual Camera in the scene
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("No Cinemachine Virtual Camera found in the scene!");
            return;
        }

        // Look for the player after instantiation
        Invoke(nameof(AssignPlayerToCamera), 0.1f); 
    }

    private void AssignPlayerToCamera()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            virtualCamera.Follow = player.transform;
            virtualCamera.LookAt = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the player prefab has the 'Player' tag.");
        }
    }
}
