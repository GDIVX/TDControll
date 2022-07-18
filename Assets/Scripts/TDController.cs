using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TDController : MonoBehaviour
{
    public float speed = 1;
    public float maxVelocity;
    [Range(0f, 1f)]
    public float sensitivity;
    [Range(0, .5f)]
    public float coefficientOfFriction = 0.0125f;

    public UnityEvent onMove;
    public UnityEvent onStop;
    public bool hasStoped = true;

    Vector2 direction;
    Vector2 velocity;
    Vector2 previousDirection;

    void Update()
    {
        previousDirection = direction;
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        var acceleration = direction * speed * Time.deltaTime;

        velocity = Vector2.Lerp(velocity, acceleration + velocity, Time.deltaTime);
        var friction = -1 * coefficientOfFriction * velocity.normalized;
        velocity = Vector2.Lerp(velocity, velocity + friction, Time.deltaTime);
        velocity = Vector2.ClampMagnitude(velocity, maxVelocity);

        if (velocity.magnitude > sensitivity)
        {
            hasStoped = false;
            transform.Translate(velocity);
            onMove.Invoke();
        }
        else if (!hasStoped)
        {
            onStop.Invoke();
            hasStoped = true;
        }
    }

    public void debug()
    {
        Debug.Log("!");
    }

}
