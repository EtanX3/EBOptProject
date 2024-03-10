using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float force = 20f;

    private Queue<GameObject> bulletPool = new Queue<GameObject>();
    public int poolSize = 20;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.GetComponent<BulletScript>().gun = this;
            bulletPool.Enqueue(bullet);
        }
    }

    public void Fire()
    {
        GameObject bullet = GetBulletFromPool();
        bullet.transform.position = firepoint.position;
        bullet.transform.rotation = firepoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * force, ForceMode2D.Impulse);
    }

    private GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            return bulletPool.Dequeue();
        }
        else
        {
            // If pool is empty, create a new bullet
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullet.GetComponent<BulletScript>().gun = this;
            return bullet;
        }
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
