using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
namespace Quest
{
    [CreateAssetMenu(fileName = "Quest 2 Level 1", menuName = "Quests/Level 1/Quest 2")]
    public class Level1Quest2Data : QuestData
    {
        public override QuestFunction Initialize(QuestsHandler qHandler)
        {
            return new Level1Quest2Functions(qHandler, this);
        }
    }

    public class Level1Quest2Functions : QuestFunction
    {
        bool IsPastTheGate;

        public Level1Quest2Functions(QuestsHandler qHandler, Level1Quest2Data qData) : base (qHandler, qData)
        {
            qHandler.AddBool(IsPastTheGate, "N/A");
            GlobalEvents.Instance.FindEvent("On Past The Quest Gate").AddListener(CheckIfDone);
        }

        void CheckIfDone()
        {
            IsPastTheGate = true;
            Handler.ChangeToNextQuest();
        }
        public override void QuestDiscard()
        {
            GlobalEvents.Instance.FindEvent("On Past The Quest Gate").RemoveListener(CheckIfDone);
        }

        public override void QuestUpdateFunc()
        {
            
        }


    }
}

