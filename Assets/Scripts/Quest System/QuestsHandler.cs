using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InterfaceAndInheritables;
using StateMachine.Base;
using UnityEngine.Events;

namespace Quest
{
    public class QuestsHandler : QuestMachineHandler<QuestData, QuestFunction>
    {
        public static QuestsHandler Instance;
        [SerializeField] List<QuestData> QuestListInOrder; 

        public List<BoolWithName> Bools;
        public List<FloatWithNameAndMaxVal> Floats;

        public UnityEvent<QuestData> OnChangeQuest;

        public int CurrentQuestIndex = 0;

        private void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
        }
        private void Start()
        {
            // ChangeQuest(QuestListInOrder[CurrentQuestIndex]); THIS IS STARTED IN QUEST UI HANDLER TO PREVENT ISSUE/S
        }

        public void ChangeQuest(QuestData newQuest)
        {
            ClearVars();
            CurrentQuest?.QuestDiscard();
            CurrentQuest = newQuest.Initialize(this);
            CurrentQuestName = newQuest.QuestName;

            OnChangeQuest?.Invoke(newQuest);

        }

        public void ChangeToNextQuest()
        {
            CurrentQuestIndex++;

            if(CurrentQuestIndex >= QuestListInOrder.Count)
            {
                Debug.Log("NO QUEST AVAILABLE");
                QuestUIHandler.Instance.QuestTitle.text = "NO QUEST AVAILABLE";
                QuestUIHandler.Instance.DeleteAllQuestContents();
                return;
            }
            ChangeQuest(QuestListInOrder[CurrentQuestIndex]);
        }

        public void ChangeToQuestByIndex(int index)
        {
            CurrentQuestIndex = index;
            ChangeQuest(QuestListInOrder[index]);
        }
        public void ClearVars()
        {
            Bools.Clear();
            Floats.Clear();
        }

        public void AddBool(bool boolToAdd, string boolName)
        {
            BoolWithName boolToAddToList = new BoolWithName();
            boolToAddToList.Boolean = boolToAdd;
            boolToAddToList.BooleanName = boolName;
            Bools.Add(boolToAddToList);
        }
        public void AddFloat(float floatToAdd,float neededVal, string floatName)
        {
            FloatWithNameAndMaxVal floatToAddToList = new FloatWithNameAndMaxVal();
            floatToAddToList.Float = floatToAdd;
            floatToAddToList.MaxFloat = neededVal;
            floatToAddToList.FloatName = floatName;
            Floats.Add(floatToAddToList);
        }
        public void UpdateBool(BoolWithName boolWithName) => Bools.Find(_ => _.BooleanName == boolWithName.BooleanName).Boolean = boolWithName.Boolean;
        public void UpdateFloat(string floatName, float newValue)
        {
            FloatWithNameAndMaxVal floatVar = Floats.Find(_ => _.FloatName == floatName);
            floatVar.Float = newValue;

            QuestUIHandler.Instance.UpdateFloatQuestContent(floatVar);
        }


    }
}


