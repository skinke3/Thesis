using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float health = 5;

    GameObject player;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
    }
}
