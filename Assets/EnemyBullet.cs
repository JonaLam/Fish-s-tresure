using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [HideInInspector] public Vector2 dir;
    [SerializeField] float speed;
    [SerializeField] int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * (Vector3)dir * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        GameManager.instance.PlayerDamage(damage);


        Destroy(gameObject);
    }
}
