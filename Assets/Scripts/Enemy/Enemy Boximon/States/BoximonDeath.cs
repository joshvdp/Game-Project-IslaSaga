using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StateMachine.Enemy.State
{
    [CreateAssetMenu(fileName = "Boximon Death", menuName = "State Machine/Enemy/Boximon/State/Death")]
    public class BoximonDeath : BoximonMachineData
    {
        [SerializeField, Foldout("Death")] private bool destroyOnEnd;

        public bool DestroyOnEnd => destroyOnEnd;
        public override BoximonMachineFunctions Initialize(BoximonMonoStateMachine machine)
        {
            return new BoximonDeathFunctions(machine, this);
        }
    }

    public class BoximonDeathFunctions : BoximonMachineFunctions
    {
        bool DestroyOnEnd;
        public BoximonDeathFunctions(BoximonMonoStateMachine machine, BoximonDeath data) : base(machine, data)
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
