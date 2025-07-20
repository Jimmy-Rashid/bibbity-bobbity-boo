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

        transform.LookAt(new Vector3(playerPosition.x, 0, playerPosition.z));
    }
}