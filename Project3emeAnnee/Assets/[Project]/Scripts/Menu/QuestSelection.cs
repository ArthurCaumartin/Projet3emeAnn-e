using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelection : MonoBehaviour
{
    private QuestManager _questManager;
    private bool _isSelected, _isComplete;

    public int _numberToDo, _progress;
    
    public Image _stampSprite;
    
    private void Start()
    {
        _questManager = GetComponentInParent<QuestManager>();
    }

    public void SelectQuest()
    {
        if (_isSelected && _isComplete) return;
        
        _questManager.SelectQuest(this);
        
        _isSelected = true;
        _stampSprite.color = new Color(1,0,0,1);
    }
    public void UnSelectQuest()
    {
        _isSelected = false;

        _stampSprite.color = new Color(1,0,0,0);
    }

    public void Progress()
    {
        _progress++;
        if (_progress >= _numberToDo)
        {
            AchieveQuest();
        }
    }

    private void AchieveQuest()
    {
        _isComplete = true;
        _isSelected = false;
        
        _stampSprite.color = new Color(0,1,0,1);
    }
}
