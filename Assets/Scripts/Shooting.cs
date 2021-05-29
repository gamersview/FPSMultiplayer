using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject bulletImpact;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            Instantiate(bulletImpact, hit.point + (hit.normal *  .002f), Quaternion.LookRotation(hit.normal, Vector3.up)) ;
        }
    }
}
