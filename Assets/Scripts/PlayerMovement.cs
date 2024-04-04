using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;
    public float speed = 5;
    [SerializeField] Rigidbody rb;

    float horizontalInput;

    public float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;
    public float speedIncreasePerPoint = 0.05f;

    // Aggiungi una variabile per la sensibilità del giroscopio
    [SerializeField] float gyroSensitivity = 2f;

    // Variabile per l'integrazione del giroscopio
    private float gyroPosition = 0f;

    // Aggiungi variabili per i limiti di movimento laterale
    [SerializeField] float minXLimit = -5f;
    [SerializeField] float maxXLimit = 5f;

    private void FixedUpdate()
    {
        if (!alive) return;

        // Calcola il movimento orizzontale combinando l'input della tastiera e il giroscopio
        float horizontalMove = horizontalInput * speed * Time.fixedDeltaTime;
        float gyroMove = gyroPosition * gyroSensitivity * 10 * Time.fixedDeltaTime;
        float totalMove = horizontalMove + gyroMove;

        // Aggiungi limiti di movimento laterale
        float newXPosition = Mathf.Clamp(rb.position.x + totalMove, minXLimit, maxXLimit);

        // Applica il movimento laterale al rigidbody del giocatore
        rb.MovePosition(new Vector3(newXPosition, rb.position.y, rb.position.z));

        // Muovi il giocatore in avanti automaticamente
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMove);
    }

    private void Update()
    {
        // Leggi l'input orizzontale della tastiera
        horizontalInput = Input.GetAxis("Horizontal");

        // Leggi l'input del giroscopio solo per il movimento laterale
        gyroPosition = Input.acceleration.x;

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }*/

        if (transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        alive = false;
        //restart the game
        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        if (isGrounded) // Impedisce al player di saltare se non è a terra
        {

            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}