using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Fiery Death", menuName = "State Machine/Enemy/Fiery/State/Death")]
    public class FieryDeath : FieryMachineData
    {
        [SerializeField, Foldout("Death")] private bool destroyOnEnd;

        public bool DestroyOnEnd => destroyOnEnd;
        public override FieryMachineFunctions Initialize(FieryMonoStateMachine machine)
        {
            return new FieryDeathFunctions(machine, this);
        }
    }

    public class FieryDeathFunctions : FieryMachineFunctions
    {
        bool DestroyOnEnd;
        public FieryDeathFunctions(FieryMonoStateMachine machine, FieryDeath data) : base(machine, data)
        {
            DestroyOnEnd = data.DestroyOnEnd;
            if (DestroyOnEnd) machine.AnimationEvents.FindEvent("On Animation End").AddListener(machine.DestroyGameobject);
            machine.Agent.isStopped = true;
        }
        public override void Discard()
        {
            base.Discard();
            if (DestroyOnEnd) machine.AnimationEvents.FindEvent("On Animation End").RemoveListener(machine.DestroyGameobject);
        }

    }
}
