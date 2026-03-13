using UnityEngine;
using UnityHFSM;


namespace CompanionAI.FSM
{
    public class IdleState : CompanionStateBase
    {
        public IdleState(bool needsExitTime, Companion Companion) : base(needsExitTime, Companion) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.isStopped = true;
            Animator.SetFloat("Speed", 0f);
        }
    }
}
