using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Look { UP, DOWN, LEFT, RIGHT };
public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    bool DEBUG_INPUT = false;

    public float HP;
    public float Speed = 1.0F;
    public float AttackDuration = 1.0F;

    public Rigidbody2D _rigidbody;
    public MeleeAttackHandler _attackArea;
    public SpawnerPiojos spawner;

    bool _attacking = false;
    bool _digging = false;

    Vector2 _lookDir = new Vector2(0.0F, -1.0F);

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
        if (_digging)
        {
            if (Rewired.ReInput.players.Players[0].GetButtonUp("ButtonB"))
            {
                _digging = false;
                animator.SetTrigger("StopAction");
            }
        }

        else
        {

            if (!_attacking)
                Rotate();

            if (_rigidbody)
                Move();


            if (Rewired.ReInput.players.Players[0].GetButtonDown("ButtonA") && _attackArea)
            {
                if (!_attacking)
                    Attack();
            }
            if (Rewired.ReInput.players.Players[0].GetButtonDown("ButtonB"))
            {
                interactableCollider.OverlapCollider(interactableContactFilter, interactableCollidedColliders);

                foreach (Collider2D col in interactableCollidedColliders)
                {
                    if (col.gameObject.tag == "Hair")
                    {
                        Interact(col);
                    }
                }
            }
            if (Rewired.ReInput.players.Players[0].GetButtonDown("ButtonX"))
            {
                Debug.Log("X!");
            }
            if (Rewired.ReInput.players.Players[0].GetButtonDown("ButtonY"))
            {
                Debug.Log("Y!");
            }
        }

        if (DEBUG_INPUT)
            DebugInputOutput();        
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        _attacking = true;

        _attackArea.Attack();

        //TEMP!!!
        //DESACTIVAR COLLIDER CUANDO ANIMACI�N? TIEMPO?
        StartCoroutine(StopAttack());
    }

    

    IEnumerator StopAttack()
    {
        yield return new WaitForEndOfFrame();

        string animName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        while(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == animName)
        {
            yield return new WaitForEndOfFrame();
        }

        _attackArea.StopAttack();
        _attacking = false;
    }

    void Throw()
    {

    }
    void Interact(Collider2D col)
    {
        if (col != null)
        {
            _digging = true;
            animator.SetTrigger("Action");

            StartCoroutine(StopDig(col));
        }
    }

    IEnumerator StopDig(Collider2D col)
    {
        yield return new WaitForEndOfFrame();

        string animName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        while (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == animName && _digging)
        {
            yield return new WaitForEndOfFrame();
        }

        if(_digging)
        {
            col.gameObject.GetComponent<Pelo>().Excavate(spawner);
            _digging = false;
        }
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
            animator.SetInteger("Dir", (int)Look.UP);
        }
        else if (_lookDir.y < -deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 180);
            animator.SetInteger("Dir", (int)Look.DOWN);
        }
        else if (_lookDir.x < -deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 90);
            animator.SetInteger("Dir", (int)Look.LEFT);
        }
        else if (_lookDir.x > deadZone)
        {
            pivot.localEulerAngles = new Vector3(0, 0, 270);
            animator.SetInteger("Dir", (int)Look.RIGHT);
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

        animator.SetBool("Moving", !(x == 0.0f && y == 0.0f));

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

    public void TakeDamage(float dmg)
    {
        HP -= dmg;

        if(HP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HitAnim(0.1f));
        }
    }

    IEnumerator HitAnim(float t)
    {
        Color c = new Color(1, 0, 0);

        Color realC = new Color(1, 1, 1);

        float timer = 0.0f;

        while(timer < t)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(c, realC, timer / t);

            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
        }

        GetComponent<SpriteRenderer>().color = realC;
    }

    public void Die()
    {

    }
}
