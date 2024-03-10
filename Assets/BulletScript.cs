using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Gun gun;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>().Damage();
        }

        Timeout();
    }

    private void OnEnable()
    {
        Invoke("Timeout", 2);
    }

    private void Timeout()
    {
        CancelInvoke("Timeout");
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gun.ReturnBulletToPool(gameObject);
    }
}
