using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        // Se a colisão foi com um inimigo
        if (enemy != null )
        {
            enemy.TakeTamage(damage);
        }
    }
}
