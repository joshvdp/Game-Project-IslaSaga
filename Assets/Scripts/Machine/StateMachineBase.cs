using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Base
{
    public abstract class StateMachineData<TMachine, TFunctions> : ScriptableObject
    {
        public string StateName;

        public abstract TFunctions Initialize(TMachine machine);
    }

    public abstract class StateMachineFunction<TMachine, TData>
    {
        protected TData data;
        protected TMachine machine;

        public TData Data => data;
        public StateMachineFunction(TMachine mainMachine, TData dataHolder)
        {
            machine = mainMachine;
            data = dataHolder;
        }

        public abstract void StateUpdate();
        public abstract void StateFixedUpdate();
        public abstract void Discard();
    }
    public abstract class QuestMachineData<QMachine, QFuncs> : ScriptableObject
    {
        protected QMachine handler;
        public abstract QFuncs Initialize(QMachine qHandler);
    }
    public abstract class QuestMachineFunctions<QMachine, QData>
    {
        public QMachine Handler;
        public QData Data;
        public QuestMachineFunctions(QMachine qHandler, QData qData)
        {
            Handler = qHandler;
            Data = qData;
        }
        public abstract void QuestUpdateFunc();
        public abstract void QuestDiscard();
    }
}

