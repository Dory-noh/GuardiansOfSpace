using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public AudioClip shootingSoundClip;
    public AudioSource audioSource;
    public Transform FirePos;
    //public GameObject bulletPrefab;
    private float power = 15f;

    int enemyLayerMask;
    void Start()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        audioSource = GetComponent<AudioSource>();
        power = 20f;
        if(lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
        if(FirePos == null)
            FirePos = transform.GetChild(3).transform;
    }

    public void Shoot()
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 30f)) //FirePos.position, transform.forward
        {
            Debug.Log($"{hit.transform.parent.gameObject.name} 감지 성공");
            LivingEntity enemy = hit.transform.GetComponentInParent<LivingEntity>();

            if(enemy != null)
            {
                Debug.Log($"{hit.transform.parent.gameObject.name} 공격 성공");
                enemy.OnDamage(power);
            }
            else
            {
                Debug.Log($"총알 공격 실패");
            }
            FirePos.forward = (hit.point - FirePos.position).normalized;
            StartCoroutine(ShowLaser(hit.point));
        }
        else
        {
            Vector3 missPoint = ray.origin + ray.direction * 30f;
            FirePos.forward = (missPoint - FirePos.position).normalized;
            StartCoroutine(ShowLaser(missPoint));
        }
        
    }

    IEnumerator ShowLaser(Vector3 hitPoint)
    {
        
        lineRenderer.SetPosition(0, FirePos.position);
        lineRenderer.SetPosition(1, hitPoint);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(shootingSoundClip);
        yield return new WaitForSeconds(0.2f);

        lineRenderer.enabled = false;
    }
}
