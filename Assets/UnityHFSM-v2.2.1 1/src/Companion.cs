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
        [SerializeField]
        private TargetSensor TargetSensor;

        [Header("Dodge Config")]
        [SerializeField]
        private float DodgeCooldown = 2;
        private float LastDodgeTime = 0;

        [Space]
        [Header("DebugInfo")]
        [SerializeField]
        private bool isInDodgeRange;
        [SerializeField]
        private bool isInChaseRange;

        private StateMachine<CompanionState, StateEvent> CompanionFSM;
        private Animator Animator;
        private NavMeshAgent Agent;

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Agent = GetComponent<NavMeshAgent>();
            CompanionFSM = new StateMachine<CompanionState, StateEvent>();

            // Add all states
            CompanionFSM.AddState(CompanionState.Idle, new IdleState(false, this));
            CompanionFSM.AddState(CompanionState.Patrol, new PatrolState(true, this, target.transform));
            CompanionFSM.AddState(CompanionState.Attack, new AttackState(true, this, OnAttack));
            CompanionFSM.AddState(CompanionState.Dodge, new DodgeState(true, this, OnDodge));

            // Add transitions
            CompanionFSM.AddTriggerTransition(StateEvent.DetectTarget, new Transition<CompanionState>(CompanionState.Idle, CompanionState.Patrol));
            CompanionFSM.AddTriggerTransition(StateEvent.DetectProjectile, new Transition<CompanionState>(CompanionState.Patrol, CompanionState.Idle));

            CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Idle, CompanionState.Patrol, (transition) => isInChaseRange && Vector3.Distance(target.transform.position, transform.position) > Agent.stoppingDistance));
            CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Patrol, CompanionState.Idle, (transition) => !isInChaseRange || Vector3.Distance(target.transform.position, transform.position) <= Agent.stoppingDistance));


            //CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Idle, CompanionState.Dodge, ShouldDodge, forceInstantly: true));
            //CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Patrol, CompanionState.Dodge, ShouldDodge, forceInstantly: true));

            //CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Dodge, CompanionState.Idle, ShouldDodge));
            //CompanionFSM.AddTransition(new Transition<CompanionState>(CompanionState.Dodge, CompanionState.Patrol, ShouldDodge));

            //CompanionFSM.SetStartState(CompanionState.Idle);

            CompanionFSM.Init();
        }

        private void Start()
        {
            //ProjectileSensor.OnProjectileEnter += ProjectileSensor_OnProjectileEnter; 
            //ProjectileSensor.OnProjectileExit += ProjectileSensor_OnProjectileExit;
            TargetSensor.OnTargetEnter += TargetSensor_OnTargetEnter;
            TargetSensor.OnTargetExit += TargetSensor_OnTargetExit;
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

        private void TargetSensor_OnTargetEnter(Transform target)
        {
            CompanionFSM.Trigger(StateEvent.DetectTarget);
            isInChaseRange = true;
        }

        private void TargetSensor_OnTargetExit(Vector3 lastKnownPosition)
        {
            CompanionFSM.Trigger(StateEvent.LostTarget);
            isInChaseRange = false;
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

