using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 target;

    public void Init()
    {
        target.y = 1;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        transform.LookAt(target);
    }

    void Update()
    {
        transform.position += transform.forward * 15f * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}
