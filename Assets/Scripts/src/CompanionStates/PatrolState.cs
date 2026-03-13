using Unity.Mathematics;
using UnityEngine;
using UnityHFSM;


namespace CompanionAI.FSM
{
    public class PatrolState : CompanionStateBase
    {
        private Transform singleTarget;
        private Pathing path;

        public PatrolState(bool needsExitTime, Companion Companion, Transform target, Pathing path) : base(needsExitTime, Companion)
        {
            this.singleTarget = target;
            this.path = path;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.enabled = true;
            Agent.isStopped = false;
            Animator.Play("Sprint_Forward");
            //Agent.SetDestination(path.GetCurrentWayPoint());
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (!RequestedExit)
            {
                //if (Agent.remainingDistance <= Agent.stoppingDistance)
                //{
                //    Agent.SetDestination(path.GetNextWayPoint());
                //}
                //float normalizedSpeed = Mathf.InverseLerp(0f, Agent.speed, Agent.velocity.magnitude);
                //Animator.SetFloat("Speed", normalizedSpeed);
                Agent.SetDestination(singleTarget.position);
            }
            else if (Agent.remainingDistance <= Agent.stoppingDistance)
            {
                // In case that we were requested to exit, we will continue moving to the last known position prior to transitioning out to idle
                fsm.StateCanExit();
            }
        }

    }
}

