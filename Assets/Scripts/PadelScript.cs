using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadelScript : MonoBehaviour
{
    public float speed, maxX;
    public InputAction movement;
    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var x = movement.ReadValue<float>();
        
        position.x += speed * x * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -maxX, maxX);
        
        transform.position = position;

    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }
}
