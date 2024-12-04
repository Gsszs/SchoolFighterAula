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

        // Se a colis�o foi com um inimigo
        if (enemy != null )
        {
            enemy.TakeTamage(damage);
        }
    }
}
