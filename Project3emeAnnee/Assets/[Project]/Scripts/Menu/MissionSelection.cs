using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionSelection : MonoBehaviour
{
    private MissionManager _missionManager;
    private bool _isSelected, _isComplete;

    public int _numberToDo, _progress;
    
    public Image _stampSprite;
    
    private void Start()
    {
        _missionManager = GetComponentInParent<MissionManager>();
    }

    // Clicked on a quest in the Quests Menu
    public void SelectQuest()
    {
        if (_isSelected && _isComplete) return;
        
        _missionManager.SelectQuest(this);
        
        _isSelected = true;
        _stampSprite.color = new Color(1,0,0,1);
    }
    
    // Unclicked on a quest in the Quests Menu
    public void UnSelectMission()
    {
        _isSelected = false;

        _stampSprite.color = new Color(1,0,0,0);
    }

    // Call when we progress on the quest
    public void Progress()
    {
        _progress++;
        if (_progress >= _numberToDo)
        {
            AchieveMission();
        }
    }
    
    // Called if the quest if is achieved
    private void AchieveMission()
    {
        _isComplete = true;
        _isSelected = false;
        
        _stampSprite.color = new Color(0,1,0,1);
    }
}
