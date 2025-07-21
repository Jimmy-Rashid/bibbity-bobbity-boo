using System.IO;
using UnityEngine;

public class FeetCollision : MonoBehaviour
{
    float lengthOfRay = 0.1f;
    Ray ray;
    Vector3 startPoint;
    Vector3 rayOrigin;
    RaycastHit hitInfo;
    float rayOffset;
    public bool isGrounded;
    public bool isGroundedCycle;
    void Start()
    {
        isGrounded = true;
    }

    void FixedUpdate()
    {
        rayOrigin = transform.position;

        ray = new Ray(rayOrigin, Vector3.down);
        Debug.DrawRay(rayOrigin, Vector3.down, Color.yellow);

        if (Physics.Raycast(ray, out hitInfo, lengthOfRay)) // Check if player touches other Rigidbody
        {
            if (!hitInfo.transform.gameObject.CompareTag("Rail")) // Check if touched Rigidbody is ground (not rail)
            {
                // Debug.Log("Hit ground:" + Time.time);
                isGrounded = true;
                if (GetComponent<PlayerGrind>().onRail) // Check if player is on rail
                {
                    GetComponent<PlayerGrind>().FeetCollisionOnRail();
                }
            }
        }
        else
        {
            isGrounded = false;
            // Debug.Log("No hit:" + Time.time);
        }
    } 
}
