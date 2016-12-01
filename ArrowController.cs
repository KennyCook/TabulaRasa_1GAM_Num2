using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
    public GameObject ArrowDeath;

    public float MovementSpeed = -0.05f;

    private bool _isDead = false;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    private void Update()
    {
        transform.Translate(new Vector2(MovementSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player Attack"))
        {
            _isDead = true;
            MovementSpeed = 0;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            ArrowDeath.GetComponent<SpriteRenderer>().enabled = true;
            ArrowDeath.GetComponent<Animator>().Play(null);
        }
    }

    public void DestroyArrow()
    {
        Destroy(gameObject);
    }
}
