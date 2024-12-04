using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    public bool facingRigth;
    private bool previousDirectionRigth;

    public bool isDead;
    private bool isWalking;

    private Transform target;

    private float enemySpeed = 0.3f;
    private float currentSpeed;
    private float walkTimer;

    private float horizontalForce;
    private float verticalForce;

    private float attackRate = 1f;
    private float nextAttack;

    public int maxHealth;
    public int currentHealth;

    public float staggerTime = 0.5f;
    public float damageTimer;
    public bool isTakingDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = FindAnyObjectByType<PlayerController>().transform;

        currentSpeed = enemySpeed;
    }

    void Update()
    {
        if (target.position.x < transform.position.x)
        {
            facingRigth = false;
        } else
        {
            facingRigth = true;
        }

        if (facingRigth && !previousDirectionRigth)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRigth = true;
        }

        if (!facingRigth && previousDirectionRigth)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRigth = false;
        }

        walkTimer += Time.deltaTime;

        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        if (damageTimer >= staggerTime)
        {
            isTakingDamage = false;
            damageTimer = 0;

            ResetSpeed();
        }

        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        Vector3 targetDistance = target.position - this.transform.position;

        horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);

        if (walkTimer >= Random.Range(1f, 2f))
        {
            verticalForce = Random.Range(-1, 2);
            walkTimer = 0;
        }

        if (Mathf.Abs(targetDistance.x) < 0.2f)
        {
            horizontalForce = 0;
        }

        rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);

        if (Mathf.Abs(targetDistance.x) < 0.2f && Mathf.Abs(targetDistance.y) < 0.05f && Time.time > nextAttack)
        {
            animator.SetTrigger("Attack");
            ZeroSpeed();
            nextAttack = Time.time + attackRate;
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void TakeTamage (int damage)
    {
        if (!isDead)
        {
            isTakingDamage = true;

            currentHealth -= damage;

            animator.SetTrigger("hitDamage");
        }
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = enemySpeed;
    }
}
