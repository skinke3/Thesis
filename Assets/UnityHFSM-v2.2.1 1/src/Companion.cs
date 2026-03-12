using UnityEngine;
using UnityHFSM;
using UnityEngine.AI;



namespace CompanionAI.FSM
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class Companion : MonoBehaviour
    {
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
            CompanionFSM.AddState(CompanionState.Patrol, new PatrolState(true, this));
            CompanionFSM.AddState(CompanionState.Attack, new AttackState(true, this));
            CompanionFSM.AddState(CompanionState.Dodge, new DodgeState(true, this));

            CompanionFSM.SetStartState(CompanionState.Idle);

            CompanionFSM.Init();
        }

        private void Update()
        {
            CompanionFSM.OnLogic();
        }
    }
}

