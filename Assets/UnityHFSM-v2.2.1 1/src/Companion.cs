using UnityEngine;
using UnityHFSM;
using UnityEngine.AI;

namespace CompanionAI.FSM
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class Companion : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private GameObject target;

        [Header("Sensors")]
        [SerializeField]
        private ProjectileSensor ProjectileSensor;

        [Header("Dodge Config")]
        [SerializeField]
        private float DodgeCooldown = 2;
        private float LastDodgeTime = 0;

        [Space]
        [Header("DebugInfo")]
        [SerializeField]
        private bool isInDodgeRange;

        private StateMachine<CompanionState, StateEvent> CompanionFSM;
        private Animator animator;
        private NavMeshAgent agent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            CompanionFSM = new StateMachine<CompanionState, StateEvent>();

            // Add all states
            CompanionFSM.AddState(CompanionState.Idle, new IdleState(false, this));
            CompanionFSM.AddState(CompanionState.Patrol, new PatrolState(true, this, target.transform));
            CompanionFSM.AddState(CompanionState.Attack, new AttackState(true, this, OnAttack));
            CompanionFSM.AddState(CompanionState.Dodge, new DodgeState(true, this, OnDodge));

            // Add transitions
            CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Idle, CompanionState.Dodge, ShouldDodge, forceInstantly: true));
            CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Patrol, CompanionState.Dodge, ShouldDodge, forceInstantly: true));

            CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Dodge, CompanionState.Idle, ShouldDodge));
            CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Dodge, CompanionState.Patrol, ShouldDodge));



            //CompanionFSM.SetStartState(CompanionState.Idle);

            CompanionFSM.Init();
        }

        private void Start()
        {
            ProjectileSensor.OnProjectileEnter += ProjectileSensor_OnProjectileEnter; 
            ProjectileSensor.OnProjectileExit += ProjectileSensor_OnProjectileExit;
        }

        private void ProjectileSensor_OnProjectileEnter(Transform Projectile)
        {
            CompanionFSM.Trigger(StateEvent.DetectProjectile);
            isInDodgeRange = true;
        }

        private void ProjectileSensor_OnProjectileExit(Vector3 LastKnownPosition)
        {
            CompanionFSM.Trigger(StateEvent.LostProjectile);
            isInDodgeRange = false;
        }

        private void OnAttack(State<CompanionState, StateEvent> State) { }

        private void OnDodge(State<CompanionState, StateEvent> State)
        {
            LastDodgeTime = Time.time;
        }

        private bool ShouldDodge(Transition<CompanionState> Transition) => LastDodgeTime + DodgeCooldown <= Time.time && isInDodgeRange;

        private void Update()
        {
            CompanionFSM.OnLogic();
        }
    }
}

