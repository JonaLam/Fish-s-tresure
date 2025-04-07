using UnityEngine;

public class PufferFish : FishBehaviour
{
    [SerializeField] EnemyBullet enemyBullet;
    [SerializeField] int amount;

    public override void Die()
    {
        Shoot();

        base.Die();
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
    
}
