using UnityEngine;

/// <summary>
/// Script para controlar o personagem principal no jogo.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Configuracoes de Movimento")]
    [SerializeField] private float speed = 5f; // Velocidade de movimento
    [SerializeField] private float jumpForce = 10f; // Forca do pulo

    private Rigidbody2D rb; // Referencia ao Rigidbody2D do player
    //private Animator animator; // Referencia ao Animator do player
    private bool isGrounded = true; // Indica se o jogador esta no chao

    private void Awake()
    {
        // Inicializa as referencias
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    /// <summary>
    /// Lida com o movimento horizontal do player.
    /// </summary>
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);
        rb.linearVelocity = velocity;

        // Atualiza a direcao do sprite
        if (horizontalInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    /// <summary>
    /// Lida com o pulo do jogador.
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; // O jogador saiu do chao
        }
    }

    /// <summary>
    /// Atualiza as animacoes do personagem.
    /// </summary>
    private void UpdateAnimations()
    {
        //animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        //animator.SetBool("IsGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o player voltou ao chao
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
