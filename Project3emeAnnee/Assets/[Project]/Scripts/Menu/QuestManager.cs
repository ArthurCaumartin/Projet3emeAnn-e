using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
   public QuestSelection _selectedQuest;
   public static QuestManager instance;
   
   private void Awake()
   {
      instance = this;
   }
   
   // Call this Function when a quest is selected in the menu
   public void SelectQuest(QuestSelection questSelectedScirpt)
   {
      if (_selectedQuest != null)
      {
         _selectedQuest.UnSelectQuest();
      }
      
      _selectedQuest = questSelectedScirpt;
      //Add on Scriptable not destroyOnLoad
   }

   public void QuestKillEvent()
   {
      _selectedQuest.Progress();
   }
   public void QuestDamagesEvent()
   {
      _selectedQuest.Progress();
   }

   public void QuestObjectiveEvent()
   {
      _selectedQuest.Progress();
   }
}
