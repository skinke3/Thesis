using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

namespace CompanionAI.FSM
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] private float health = 5f;
        [SerializeField] private float knockback = 5000f;
        [SerializeField] private float knockUpMultiplier = 4f;
        [HideInInspector] public bool knockedUp = false;

        //GameObject player;
        Animator animator;
        Rigidbody rb;
        ThirdPersonController playerScript;

        public bool grounded = true;
        private float groundedTimer = 0f;

        [Tooltip("Useful for rough ground")]
        public float groundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float groundedRadius = 0.35f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask groundLayers;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            playerScript = player.GetComponent<ThirdPersonController>();
        }

        private void Update()
        {
            GroundedCheck();
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            Vector3 targetDireciton = player.transform.position - transform.position;
            float step = 200f * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDireciton, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);

            if (playerScript.attackCounter == 4 && grounded == true)
            {
                groundedTimer = 0;
                Debug.Log(playerScript.attackCounter);
                knockedUp = true;
                animator.SetBool("knockup", knockedUp);
                rb.AddForce(Vector3.up * knockback * knockUpMultiplier);
            }
            else
            {
                knockedUp = false;
                animator.SetTrigger("damage");
                rb.AddForce(Vector3.up * knockback);
            }

        }

        private void GroundedCheck()
        {
            groundedTimer += Time.deltaTime;
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset,
                transform.position.z);
            grounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers,
                QueryTriggerInteraction.Ignore);

            if (grounded && groundedTimer > 2)
            {
                groundedTimer = 0;
                knockedUp = false;
                animator.SetBool("knockup", knockedUp);
                transform.localEulerAngles = transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
            }
        }
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z),
                groundedRadius);
        }
    }
}


