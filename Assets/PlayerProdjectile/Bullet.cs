using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PlayerProdjectile
{
    [SerializeField] int damage;
    Vector2 dir;
    [SerializeField] float speed;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    public override void InstanceProdjectile(PlayerMovement player, Vector2 aimTo)
    {
        Vector2 dir = (Vector3)aimTo - player.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();

        this.dir = dir;

        transform.position = player.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
         transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            FishBehaviour enemy = collision.GetComponent<FishBehaviour>();
            enemy.TakeDamage(GetDamage());
            Destroy(gameObject);
        }
    }
}
