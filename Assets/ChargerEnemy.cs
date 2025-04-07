using UnityEngine;

public class ChargerEnemy : FishBehaviour
{

    Transform target;
    [SerializeField] LayerMask scanning;

    [SerializeField] float scanSize;

    float chargeTime;
    Vector2 dir;

    [SerializeField] float chargeSpeed;

    private void Update()
    {
        if (target == null)
            BasicMovement();
        else
            Charge();
    }

    void Charge() 
    {
        sr.flipX = true;

        chargeTime += Time.deltaTime;

        if (chargeTime < 0.5f)
            return;

        transform.position += (Vector3)dir * chargeSpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -18, 18), Mathf.Clamp(transform.position.y, -9, 9));

        if(chargeTime > 1.5f) 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            target = null;
        }
    }

    private void FixedUpdate()
    {
        if(target == null)
            ScanForPlayer();
    }

    void ScanForPlayer()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, scanSize, scanning);



        if (col == null)
            return;

        target = col.transform;
        chargeTime = 0;
        dir = (Vector3)target.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
