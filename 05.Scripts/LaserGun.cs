using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform FirePos;
    //public GameObject bulletPrefab;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        FirePos = transform.GetChild(3).transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(FirePos.position, transform.forward, out hit, 100f))
        {
            Debug.Log(hit.collider.name + "맞음");
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("발사");
                //GameObject bullet = Instantiate(bulletPrefab, FirePos);
                //bullet.GetComponent<Rigidbody>().AddForce(hit.transform.forward);
                //Destroy(bullet, 3f);
                
            }
            StartCoroutine(ShowLaser(hit.point));
        }
    }

    IEnumerator ShowLaser(Vector3 hitPoint)
    {
        lineRenderer.SetPosition(0, FirePos.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;

        yield return new WaitForSeconds(1f);

        lineRenderer.enabled = false;
    }
}
