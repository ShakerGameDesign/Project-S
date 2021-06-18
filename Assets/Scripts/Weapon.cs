using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) Shoot(hit.point);
        }
    }

    void Shoot(Vector3 target)
    {
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;

        bullet.GetComponent<Bullet>().target = target;
        bullet.GetComponent<Bullet>().Init();
    }
}
