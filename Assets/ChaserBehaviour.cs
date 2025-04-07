using UnityEngine;

public class ChaserBehaviour : FishBehaviour
{
    Transform target;
    [SerializeField] LayerMask scanning;

    [SerializeField] float scanSize;

    private void Update()
    {
        if (target == null)
            BasicMovement();
        else
            Chase();
    }

    void Chase() 
    {
        Vector2 dir = (Vector3)target.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();

        transform.position += (Vector3)dir * speed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void FixedUpdate()
    {
        ScanForPlayer();
    }

    void ScanForPlayer() 
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, scanSize, scanning);

        if (col != null)
            target = col.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, scanSize);
    }
}
