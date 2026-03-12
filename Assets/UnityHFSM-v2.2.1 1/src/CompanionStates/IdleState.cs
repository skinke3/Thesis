using UnityEngine;


namespace CompanionAI.FSM
{
    public class IdleState : CompanionStateBase
    {
        public IdleState(bool needsExitTime, Companion Companion) : base(needsExitTime, Companion) { }

        public override void OnEnter()
        {
            base.OnEnter();
            Agent.isStopped = true;
            Animator.Play("Idle_1H");
        }
    }
}
