using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
namespace Quest
{
    public class QuestData : QuestMachineData<QuestsHandler, QuestFunction>
    {
        [SerializeField] protected string questName;
        public string QuestName => questName;

        public override QuestFunction Initialize(QuestsHandler qHandler)
        {
            return null;
        }
    }

    public class QuestFunction : QuestMachineFunctions<QuestsHandler, QuestData>
    {
        
        public QuestFunction(QuestsHandler qHandler, QuestData qData) : base(qHandler, qData)
        {
            
        }
        public override void QuestUpdateFunc()
        {
            
        }
        public override void QuestDiscard()
        {
            
        }

        
    }
}

