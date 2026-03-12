using UnityEngine;
using UnityHFSM;
using System;
using UnityEngine.AI;

namespace CompanionAI.FSM
{
    public abstract class CompanionStateBase : State <CompanionState, StateEvent>
    {
        protected readonly Companion Companion;
        protected readonly NavMeshAgent Agent;
        protected readonly Animator Animator;
        protected bool RequestedExit;
        protected float ExitTime;

        protected readonly Action<State<CompanionState, StateEvent>> onEnter;
        protected readonly Action<State<CompanionState, StateEvent>> onLogic;
        protected readonly Action<State<CompanionState, StateEvent>> onExit;
        protected readonly Func<State<CompanionState, StateEvent>, bool> canExit;

        public CompanionStateBase(bool needsExitTime, 
            Companion Companion, 
            float ExitTime = 0.1f, 
            Action<State<CompanionState, StateEvent>> onEnter = null,
            Action<State<CompanionState, StateEvent>> onlogic = null,
            Action<State<CompanionState, StateEvent>> onExit = null,
            Func<State<CompanionState, StateEvent>, bool> canExit = null)
        {
            this.Companion = Companion;
            this.onEnter = onEnter;
            this.onLogic = onLogic;
            this.onExit = onExit;
            this.canExit = canExit;
            this.needsExitTime = needsExitTime;
            Agent = Companion.GetComponent<NavMeshAgent>();
            Animator = Companion.GetComponent<Animator>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            RequestedExit = false;
            onEnter?.Invoke(this);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (RequestedExit && timer.Elapsed >=  ExitTime)
            {
                fsm.StateCanExit();
            }
        }

        public override void OnExitRequest() 
        {
            if (!needsExitTime || canExit != null && canExit(this))
            {
                fsm.StateCanExit();
            }
            else
            {
                RequestedExit = true;
            }
        }
    }
}

