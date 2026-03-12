using UnityEngine;
using UnityHFSM;


namespace CompanionAI.FSM
{
    public class PatrolState : CompanionStateBase
    {
        private Transform target;

        public PatrolState(bool needsExitTime, Companion Companion, Transform target) : base(needsExitTime, Companion)
        {
            this.target = target;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.enabled = true;
            Agent.isStopped = false;
            Animator.Play("Sprint_Forward");
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (!RequestedExit)
            {
                Agent.SetDestination(target.position);
            }
            else if (Agent.remainingDistance <= Agent.stoppingDistance) 
            {
                // In case that we were requested to exit, we will continue moving to the last knoiwn position prior to transitioning out to idle
                fsm.StateCanExit();
            }
        }

    }
}

