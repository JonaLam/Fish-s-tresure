using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestFish : FishBehaviour
{
    float ShootCD;

    [SerializeField] EnemyBullet enemyBullet;
    [SerializeField] int amount;

    private new void Start()
    {
        base.Start();
        transform.position = new Vector3(transform.position.x, 0);
    }

    public override void Die()
    {
        SceneManager.LoadScene("WinScreen");
    }

    void Shoot()
    {
        float angleStep = 360f / amount;
        float angle = 0f;

        for (int i = 0; i < amount; i++)
        {
            float proxPos = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float proyPos = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 prodjectileVector = new Vector3(proxPos, proyPos);
            Vector3 moveVector = (prodjectileVector - transform.position).normalized;

            EnemyBullet b = Instantiate(enemyBullet, transform.position, Quaternion.identity);
            b.dir = moveVector;
            angle += angleStep;
        }
    }
    void Update()
    {
        BasicMovement();

        ShootCD -= Time.deltaTime;

        if (ShootCD < 0)
        {
            ShootCD = 3;
            Shoot();
        }
    }
}
