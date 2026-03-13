using UnityEngine;
using UnityHFSM;
using System;
using UnityEngine.AI;

namespace CompanionAI.FSM
{
    public class DodgeState : CompanionStateBase
    {
        public DodgeState(bool needsExitTime,
            Companion Companion,
            Action<State<CompanionState, StateEvent>> onEnter,
            float ExitTime = 3f) : base(needsExitTime, Companion, ExitTime, onEnter) { }

        public override void OnEnter()
        {
            Agent.isStopped = true;
            base.OnEnter();
            Animator.Play("Dodge");
        }

        public override void OnLogic()
        {
            Agent.Move(1.5f * Agent.speed * Time.deltaTime * Agent.transform.forward);
            base.OnLogic();
        }

    }
}
