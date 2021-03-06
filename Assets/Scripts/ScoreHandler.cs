using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class HighScoreIntEvent : UnityEvent<int> { }
public class ScoreHandler : MonoBehaviour
{
    // Handles score and adds skill points as well
    
    [NonSerialized] public static HighScoreIntEvent score = new HighScoreIntEvent();
    [SerializeField] private TextMeshProUGUI scoreBox;

    private int _currentScoreAmount;
    private int _skillPointScoreCount;
    
    private void Start()
    {
        score.AddListener(UpdateScore);
        UpdateScore(0);
    }

    private void UpdateScore(int scoreAmount)
    {
        _currentScoreAmount += scoreAmount;
        _skillPointScoreCount += scoreAmount;
        
        if (_skillPointScoreCount == SkillTree._scoreNeededForSkillPoint)
        {
            SkillTree._skillPoint++;
            SkillTree.updateSkillTreeUI.Invoke();
            _skillPointScoreCount = 0;
        }

        scoreBox.text = $"Score: {_currentScoreAmount}\nSkill Points: {SkillTree._skillPoint}";
    }
}
