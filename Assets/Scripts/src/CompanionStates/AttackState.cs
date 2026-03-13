using System;
using UnityEngine;
using UnityHFSM;

namespace CompanionAI.FSM
{
    public class AttackState : CompanionStateBase
    {
        public AttackState(bool needsExitTime,
            Companion Companion,
            Action<State<CompanionState, StateEvent>> onEnter,
            float ExitTime = 0.33f) : base(needsExitTime, Companion, ExitTime, onEnter) {}
        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play("Attack1");
        }
    }
}
