using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float speed;
    [SerializeField] int camFollowDist;
    public GameObject cameraAnchor;

    private Rigidbody rb;

    // Start is called before the first frame update
    public void PlayerStart()
    {
        rb = GetComponent<Rigidbody>();
        WorldInfo.player = this;
        transform.position = transform.position.SZ(PathGenerator.pathGen.GetPathCenterAtPos(WorldInfo.camSideViewDistance));
        WorldInfo.setWorldLoc(transform.position.x + camFollowDist);
        //WorldInfo.setWorldLoc(transform.position.x + camFollowDist);
    }

    // Update is called once per frame
    public void PlayerUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speed);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }

        if (transform.position.x > WorldInfo.i.worldLocation - camFollowDist)
        {
            WorldInfo.setWorldLoc(transform.position.x + camFollowDist);
        }
    }
}
