using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallScript : MonoBehaviour
{
    public InputAction keyPress;
    private bool _isPressed;
    public float launchForce = 10f, min = 10f, max = 10f, score = 0f;
    private Rigidbody2D _rb;
    public GameObject padel, gameOverFrame;
    public Vector3 offset;
    public AudioSource breakSound;
    public TextMeshProUGUI scoreText, finalScoreText;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float space = keyPress.ReadValue<float>();
        Vector3 padelPosition = padel.transform.position;
        
        if (!_isPressed)
            transform.position = padelPosition + offset;

        
        if (!_isPressed && space > 0)
        {
            _isPressed = true;
            _rb.AddForceY(launchForce, ForceMode2D.Impulse);
            
        }
        
        

        if (_rb.linearVelocity.magnitude < min)
            _rb.linearVelocity = _rb.linearVelocity.normalized * min;
        
        if (_rb.linearVelocity.magnitude > max)
            _rb.linearVelocity = _rb.linearVelocity.normalized * max;

    }


    private void OnEnable()
    {
        keyPress.Enable();
    }

    private void OnDisable()
    {
        keyPress.Disable();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {
            finalScoreText.text = $"Your Score: {score}";
            gameOverFrame.SetActive(true);
            Time.timeScale = 0;
        }
            

        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            breakSound.Play();
            score++;

            scoreText.text = $"Score: {score}";
        }
            
    }
}
