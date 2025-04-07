using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : PlayerProdjectile
{
    int maxDamage;
    int dealtDamage;
    Vector2 dir;
    [SerializeField] float speed;
    PlayerMovement player;

    int combo;

    bool moveing = true;

    public override void InstanceProdjectile(PlayerMovement player, Vector2 aimTo)
    {
        Vector2 dir = (Vector3)aimTo - player.transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();

        this.dir = dir;

        transform.position = player.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Start()
    {
        dealtDamage = 0;
        combo = 0;
        maxDamage = GetDamage();

        Destroy(gameObject, 2f);
    }

    void Update()
    {
        if(moveing)
            transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!moveing)
            return;
        if (collision.CompareTag("Enemy")) 
        {
            FishBehaviour enemy = collision.GetComponent<FishBehaviour>();
            int damageToDo = maxDamage - dealtDamage;
            dealtDamage += enemy.currentHealth;

            if (damageToDo >= enemy.currentHealth)
            {

                combo++;
                if (combo > 1)
                {
                    GameManager.instance.DrawTextOnScreen(transform.position, combo + "x", Color.red);
                }

                enemy.value = Mathf.RoundToInt( enemy.value *  (1 + (0.2f * (combo - 1))));
            }

            enemy.TakeDamage(damageToDo);

            if (dealtDamage >= maxDamage)
                moveing = false;
        }
    }
}
