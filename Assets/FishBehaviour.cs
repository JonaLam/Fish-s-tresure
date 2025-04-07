using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    [SerializeField] int startHealth;
    [HideInInspector] public int currentHealth;

    [SerializeField] protected float speed;

    bool moveRight = false;

    [HideInInspector] public RoomManager roomManager;

    public int value;

    [SerializeField] int contactDamage;

    protected SpriteRenderer sr;

    protected void Start()
    {
        currentHealth = startHealth;
        sr = gameObject.GetComponent<SpriteRenderer>();
        moveRight = Random.Range(0f, 1f) > 0.5f;
    }

    private void Update()
    {
        BasicMovement();
    }

    protected void BasicMovement() 
    {
        sr.flipX = moveRight;

        if (moveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x > 18)
            {
                moveRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x < -18)
            {
                moveRight = true;
            }
        }
    }

    public void TakeDamage(int amount) 
    {
        int displayNum;

        if (amount > currentHealth)
            displayNum = currentHealth;
        else
            displayNum = amount;

        GameManager.instance.DrawTextOnScreen(transform.position, displayNum.ToString());


        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die() 
    {
        GameManager.instance.DrawTextOnScreen(transform.position, "+" + value + "NOK", Color.green);
        GameManager.instance.ChangeMoney(value);

        roomManager.RemoveFish(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            GameManager.instance.PlayerDamage( contactDamage);
        }
    }
}
