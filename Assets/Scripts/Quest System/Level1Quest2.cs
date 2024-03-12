using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
namespace Quest
{
    [CreateAssetMenu(fileName = "Quest 2 Level 1", menuName = "Quests/Level 1/Quest 2")]
    public class Level1Quest2Data : QuestData
    {

        [SerializeField] string triggerEventName;
        public string TriggerEventName => triggerEventName;
        public override QuestFunction Initialize(QuestsHandler qHandler)
        {
            return new Level1Quest2Functions(qHandler, this);
        }
    }

    public class Level1Quest2Functions : QuestFunction
    {
        bool IsPastTheTrigger;
        string TriggerEventName;
        public Level1Quest2Functions(QuestsHandler qHandler, Level1Quest2Data qData) : base (qHandler, qData)
        {
            qHandler.AddBool(IsPastTheTrigger, "N/A");
            TriggerEventName = qData.TriggerEventName;
            GlobalEvents.Instance.FindEvent(TriggerEventName).AddListener(CheckIfDone);
        }

        void CheckIfDone()
        {
            IsPastTheTrigger = true;
            Handler.ChangeToNextQuest();
        }
        public override void QuestDiscard()
        {
            GlobalEvents.Instance.FindEvent(TriggerEventName).RemoveListener(CheckIfDone);
        }

        public override void QuestUpdateFunc()
        {
            
        }


    }
}

