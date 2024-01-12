using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    private Rigidbody2D rig;
    private bool IsJumping;
    private bool facingRight = true; // Adicionado para controlar a direção do personagem

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Speed = 4;
        JumpForce = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Controls();
    }

    void Controls()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (horizontalMovement > 0 && IsJumping == false)
        {
            rig.velocity = new Vector2(Speed, rig.velocity.y);
        }
        else if (horizontalMovement < 0 && IsJumping == false)
        {
            rig.velocity = new Vector2(-Speed, rig.velocity.y);
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && IsJumping == false)
        {
            rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            IsJumping = true;
        }

        // Inverter a escala do objeto se estiver indo para a esquerda
        if (horizontalMovement > 0 && !facingRight || horizontalMovement < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        // Inverter a escala no eixo X
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 8)
        {
            IsJumping = false;
        }
    }
}


