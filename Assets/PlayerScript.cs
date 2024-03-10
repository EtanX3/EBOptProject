using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Gun gun;
    private Camera mainCamera;

    Vector2 moveDirection;
    Vector2 mousePos;

    public bool dead = false;

    private void Awake()
    {
        // Cacheing frequently used componenets
        rb = GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<Gun>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (Input.GetMouseButtonUp(0))
        {
            gun.Fire();
        }

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;

        // only calling this code when the mouse position and rb position are different enough
        if (!Mathf.Approximately(mousePos.x, rb.position.x) || !Mathf.Approximately(mousePos.y, rb.position.y))
        {
            Vector2 aimDirection = mousePos - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            dead = true;
        }
    }
}
