using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float HP;

    protected AudioSource audioSource;
    public AudioClip dmgSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Chiquitin")
        {
            Damage(1);
            Destroy(collision.gameObject);
            if (HP <= 0) Die(collision.transform);
        }
    }

    public void Damage(float dmg)
    {
        HP -= dmg;

        if(HP > 0)
        {
            StartCoroutine(HitAnim(0.1f));
        }

        audioSource.clip = dmgSound;
        audioSource.Play();
    }

    IEnumerator HitAnim(float t)
    {
        Color c = new Color(1, 0, 0);

        Color realC = new Color(1, 1, 1);

        float timer = 0.0f;

        while (timer < t)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(c, realC, timer / t);

            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
        }

        GetComponent<SpriteRenderer>().color = realC;
    }

    public virtual void Die(Transform dir)
    {
        Vector2 deathDir = (transform.position - dir.position).normalized;
        GetComponent<EnemyDeath>().SendFlyingAndDie(deathDir);
    }

}
