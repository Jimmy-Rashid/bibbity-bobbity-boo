using System.IO;
using UnityEngine;

public class FeetCollision : MonoBehaviour
{
    float lengthOfRay = 1f;
    float distanceBetweenRays;
    float margin = 0;
    int numRays = 2;
    Ray ray;
    Vector3 startPoint;
    Vector3 rayOrigin;
    RaycastHit hitInfo;
    float rayOffset;
    public bool isGrounded;
    public bool isGroundedCycle;
    void Start()
    {
        distanceBetweenRays = (GetComponent<Collider>().bounds.size.x - 2 * margin) / (numRays - 1);
        rayOffset = ((numRays - 1) * distanceBetweenRays) / 2;
        //rayOffset = 0;
        isGrounded = true;
        isGroundedCycle = false;
    }

    void FixedUpdate()
    {
        startPoint = new Vector3(transform.position.x - rayOffset, transform.position.y, transform.position.z - rayOffset);
        rayOrigin = startPoint;

        isGroundedCycle = false;

        for (int i = 0; i < numRays; i++)
        {
            for (int j = 0; j < numRays; j++)
            {
                ray = new Ray(rayOrigin, Vector3.down);
                Debug.DrawRay(rayOrigin, Vector3.down, Color.yellow);

                if (Physics.Raycast(ray, out hitInfo, lengthOfRay))
                {
                    if (!hitInfo.transform.gameObject.CompareTag("Rail"))
                    {
                        Debug.Log("Hit ground:" + Time.time);
                        isGroundedCycle = true;
                        if (GetComponent<PlayerGrind>().onRail)
                        {
                            GetComponent<PlayerGrind>().FeetCollisionOnRail();
                        }
                    }
                }
                else
                {
                    Debug.Log("No hit:" + Time.time);
                }
                rayOrigin += new Vector3(distanceBetweenRays, 0, 0);
            }

            startPoint += new Vector3(0, 0, distanceBetweenRays);
            rayOrigin = startPoint;
        }
        if (isGroundedCycle)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        isGroundedCycle = false;
    } 
}
