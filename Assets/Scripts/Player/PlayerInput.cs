using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Look { UP, DOWN, LEFT, RIGHT };
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    bool DEBUG_INPUT = false;

    public float Speed = 1.0F;
    public float AttackDuration = 1.0F;

    public Rigidbody2D _rigidbody;
    public MeleeAttackHandler _attackArea;

    Vector2 _lookDir;

    Animator animator;
    Transform pivot;

    [SerializeField]
    Collider2D interactableCollider;
    [SerializeField]
    ContactFilter2D interactableContactFilter;
    [SerializeField]
    List<Collider2D> interactableCollidedColliders;

    public float deadZone = 0.25f;
    // Start is called before the first frame update
    void Awake()
    {
        _attackArea.Initialize(this);
        animator = GetComponent<Animator>();
        pivot = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();

        if (_rigidbody)
            Move();


        if (Rewired.ReInput.players.Players[0].GetButton("ButtonA"))
        {
            if (_attackArea)
                Attack();
        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonB"))
        {
            interactableCollider.OverlapCollider(interactableContactFilter, interactableCollidedColliders);

            foreach (Collider2D col in interactableCollidedColliders)
            {
                if (col.gameObject.tag == "Hair")
                {
                    Debug.Log("Pelo pelo pelo");
                }
            }
        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonX"))
        {
            Debug.Log("X!");
        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonY"))
        {
            Debug.Log("Y!");
        }

        if (DEBUG_INPUT)
            DebugInputOutput();        
    }

    void Attack()
    {
        _attackArea.gameObject.SetActive(true);

        //TEMP!!!
        //DESACTIVAR COLLIDER CUANDO ANIMACIÓN? TIEMPO?
        Invoke("StopAttack", AttackDuration);
    }

    void StopAttack()
    {
        _attackArea.gameObject.SetActive(false);
    }

    void Throw()
    {

    }

    void Interact()
    {
    }

    void Rotate()
    {
        float y = Rewired.ReInput.players.Players[0].GetAxis("YAxisLook");
        float x = Rewired.ReInput.players.Players[0].GetAxis("XAxisLook");

        if (x == 0.0F && y == 0.0F)
            return;

        Vector2 newLookDir = DetermineLookDir(x, y);

        if (newLookDir == _lookDir)
            return;

        _lookDir = newLookDir;

        if (_lookDir.y > deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 0);
            animator.Play("piojoseUp");
        }
        else if (_lookDir.y < -deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 180);
            animator.Play("piojoseDown");
        }
        else if (_lookDir.x < -deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 90);
            animator.Play("piojoseLeft");
        }
        else if (_lookDir.x > deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 270);
            animator.Play("piojoseRight");
        }
    }

    Vector2 DetermineLookDir(float x, float y)
    {
        Vector2 look = Vector2.zero;

        if(MathF.Abs(x) >= MathF.Abs(y))
            look = new Vector2(x  / MathF.Abs(x), 0);
        else
            look = new Vector2(0, y / MathF.Abs(y));

        return look;
    }

    void Move()
    {
        float y = Rewired.ReInput.players.Players[0].GetAxis("YAxisMove");
        float x = Rewired.ReInput.players.Players[0].GetAxis("XAxisMove");

        if (y > deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 0);
            animator.Play("piojoseUp");
        }
        else if (y < -deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 180);
            animator.Play("piojoseDown");
        }
        else if (x < -deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 90);
            animator.Play("piojoseLeft");
        }
        else if (x > deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 270);
            animator.Play("piojoseRight");
        }

        _rigidbody.velocity = new Vector2(x * Speed, y * Speed);
    }

    private void DebugInputOutput()
    {
        
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonB"))
        {
            Debug.Log("B!");
        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonX"))
        {
            Debug.Log("X!");
        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonY"))
        {
            Debug.Log("Y!");
        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonStart"))
        {

        }
        if (Rewired.ReInput.players.Players[0].GetAxis("YAxisMove") != 0.0f)
        {
            float y = Rewired.ReInput.players.Players[0].GetAxis("YAxisMove");
            Debug.Log("Axis Y = " + y);
        }
        if (Rewired.ReInput.players.Players[0].GetAxis("XAxisMove") != 0.0f)
        {
            float x = Rewired.ReInput.players.Players[0].GetAxis("XAxisMove");
            Debug.Log("Axis X = " + x);
        }
    }

    public void SetStartingPosition(float y)
    {
        transform.position = new Vector3(transform.position.x, y, 0);
    }
}
