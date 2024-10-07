using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionManager : MonoBehaviour
{
   public MissionSelection _selectedMission;
   public static MissionManager instance;
   
   private void Awake()
   {
      instance = this;
   }
   
   // Call this Function when a quest is selected in the menu
   public void SelectQuest(MissionSelection missionSelectedScirpt)
   {
      if (_selectedMission != null)
      {
         _selectedMission.UnSelectMission();
      }
      
      _selectedMission = missionSelectedScirpt;
      //Add on Scriptable not destroyOnLoad
   }

   public void MissionKillEvent()
   {
      _selectedMission.Progress();
   }
   public void MissionDamagesEvent()
   {
      _selectedMission.Progress();
   }

   public void MissionObjectiveEvent()
   {
      _selectedMission.Progress();
   }
}
