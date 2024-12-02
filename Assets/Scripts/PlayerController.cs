using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Setando variáveis

    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 0.6f;
    private float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalking;
    private bool playerFacingRigth = true;
    private Animator playerAnimator;

    private int punchCount;
    private float timeCross = 1f;
    private bool comboControll;

    private bool isDead;

    void Start()
    {
        // Iniciando variaveis junto ao game com componentes para sua função.
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        currentSpeed = playerSpeed;
    }

    void Update()
    {
        // Iniciando a função de PlayerMove a todo momento.
        PlayerMove();
        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (punchCount < 2)
            {
                PlayerJab();
                punchCount++;
                if (!comboControll)
                {

                    StartCoroutine(CrossController());
                }
            }

            else if (punchCount >= 2)
            {
                PlayerCross();
                punchCount = 0;
            }
            StopCoroutine(CrossController());
        }
    }

    private void FixedUpdate()
    {
        // Alterar valor de isWalking dependendo se o player estiver se movendo ou não
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;
        } else
        {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        // Colocando para playerDirection receber 2 vetores, horizontal e vertical.
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerDirection.x < 0 && playerFacingRigth)
        {
            Flip();
        }
        else if (playerDirection.x > 0  && !playerFacingRigth)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        // Mudar o parametro do animator de acordo com a váriavel isWalking
        playerAnimator.SetBool("isWalking", isWalking);
    }

    void Flip()
    {
        playerFacingRigth = !playerFacingRigth;

        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        playerAnimator.SetTrigger("isJab");
    }
    
    void PlayerCross()
    {
        playerAnimator.SetTrigger("isCross");
    }

    IEnumerator CrossController()
    {
        comboControll = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControll = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }
}
