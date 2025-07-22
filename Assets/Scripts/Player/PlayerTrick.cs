using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTrick : MonoBehaviour
{
    private PlayerInputActions playerControls;
    private PlayerGrind playerGrind;
    private PlayerScore playerScore;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        playerControls.Enable();

        playerGrind = GetComponent<PlayerGrind>();

        playerScore = GetComponent<PlayerScore>();
    }

    private void OnLeftClickTrick(InputValue val)
    {
        if (playerGrind.onRail && val.isPressed) {
            doLeftClickTrick();
        }
    }

    private void OnRightClickTrick(InputValue val)
    {
        if (playerGrind.onRail && val.isPressed) {
            doRightClickTrick();
        }
    }

    void doLeftClickTrick()
    {
        Debug.Log("Left click trick performed on rail!");
        transform.Rotate(0, 180, 0, Space.Self);
        playerScore.PerformTrick(300, "Left Click Trick");
    }

    void doRightClickTrick()
    {
        Debug.Log("Right click trick performed on rail!");
        transform.Rotate(0, -180, 0, Space.Self);
        playerScore.PerformTrick(300, "Right Click Trick");
    }
}
