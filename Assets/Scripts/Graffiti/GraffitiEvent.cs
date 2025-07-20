using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class GraffitiEvent : MonoBehaviour
{
  public GameObject mainCamera;
  public GameObject graffitiCam;
  public GameObject mainPlayer;

  public void StartEvent()
  {
    Debug.Log("GraffitiEvent: Locking player");
    PlayerMovement playerMovement = mainPlayer.GetComponent<PlayerMovement>();
    float playerSpeed = playerMovement.movementSpeed;
    playerMovement.movementSpeed = 0;
    Debug.Log("GraffitiEvent: Changing camera position");
    CinemachineCamera freeCamera = graffitiCam.GetComponent<CinemachineCamera>();
    freeCamera.Priority = 2;
  }
}
