using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        playerPosition.y = transform.position.y;

        transform.LookAt(playerPosition);
    }
}