using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.Base;
namespace Quest
{
    [CreateAssetMenu(fileName = "Quest 3 Level 1", menuName = "Quests/Level 1/Quest 3")]
    public class Level1Quest3Data : QuestData
    {
        public override QuestFunction Initialize(QuestsHandler qHandler)
        {
            return new Level1Quest3Functions(qHandler, this);
        }
    }

    public class Level1Quest3Functions : QuestFunction
    {
        bool BossFound;

        public Level1Quest3Functions(QuestsHandler qHandler, Level1Quest3Data qData) : base (qHandler, qData)
        {
            qHandler.AddBool(BossFound, "N/A");
            GlobalEvents.Instance.FindEvent("On Boss Found").AddListener(CheckIfDone);
        }

        void CheckIfDone()
        {
            BossFound = true;
            Handler.ChangeToNextQuest();
        }
        public override void QuestDiscard()
        {
            GlobalEvents.Instance.FindEvent("On Boss Found").RemoveListener(CheckIfDone);
        }

        public override void QuestUpdateFunc()
        {
            
        }


    }
}

