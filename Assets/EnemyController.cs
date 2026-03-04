using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float health = 5;
    [SerializeField] GameObject player;

    //GameObject player;
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");
        Vector3 targetDireciton =  player.transform.position - transform.position;
        float step = 200f * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDireciton, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
