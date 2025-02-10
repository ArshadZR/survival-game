using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float finalSpeed;
    private Rigidbody2D rb2d;
    List<Buff> buffs = new List<Buff>();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        buffs.Add(new Buff(1.1f, 30));
        buffs.Add(new Buff(1.1f, 30));
    }

    public void SetDirection(Vector2 direction)
    {
        finalSpeed = speed * GetAllBuffModifier();
        rb2d.velocity = direction * finalSpeed;
    }

    private float GetAllBuffModifier()
    {
        float modifier = 1;
        foreach(Buff b in buffs)
        {
            modifier *= b.modifier;
        }
        return modifier;
    }

    public void SetDirection(CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed 
            || ctx.phase == InputActionPhase.Canceled)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            //print($"Set Direction: {direction}");
            SetDirection(direction);
        }
    }
}

public class Buff
{
    public float modifier;
    public float duration;

    public Buff(float modifier, float duration)
    {
        this.modifier = modifier;
        this.duration = duration;
    }
}