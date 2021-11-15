using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [NonSerialized]public int id;

    public TMP_Text titleText;
    public TMP_Text descText;

    public int[] connectedSkills;

    private Image _skillBoxColor;
    private SkillTree _skillTree;

    private void Awake()
    {
        _skillTree = GameObject.Find("GameManager").GetComponent<SkillTree>();
        _skillBoxColor = GetComponent<Image>();
    }

    public void UpdateUI()
    {
        titleText.text = $"{_skillTree._skillLevels[id]}/{_skillTree._skillCaps[id]} \n {_skillTree._skillNames[id]}";

        if (_skillTree._skillLevels[id] != _skillTree._skillCaps[id])
        {
            descText.text = $"{_skillTree._skillDescriptions[id]} \n cost: 1 sp";
        }
        else descText.text = $"{_skillTree._skillDescriptions[id]}";

        _skillBoxColor.color = _skillTree._skillLevels[id] >= _skillTree._skillCaps[id] ? Color.yellow
            : SkillTree._skillPoint >= 1 ? Color.green : Color.white;

        foreach (var connectedSkill in connectedSkills)
        {
            _skillTree._skillList[connectedSkill].gameObject.SetActive(_skillTree._skillLevels[id] > 0);
            _skillTree._connectorList[connectedSkill].SetActive(_skillTree._skillLevels[id] > 0);
        }
    }

    public void Buy()
    {
        if (SkillTree._skillPoint < 1 || _skillTree._skillLevels[id] >= _skillTree._skillCaps[id]) return;

        SkillTree._skillPoint -= 1;
        _skillTree._skillLevels[id]++;
        _skillTree.UpdateAllSkillUI();
        HighScore.score.Invoke(0);
    }

    public void PlaySong()
    {
        if (_skillTree._skillLevels[id] == 0) return;
        
        _skillTree._audioSource.Stop();
        _skillTree._audioSource.clip = _skillTree.songs[id];
        if (_skillTree._skillLevels[id] == 1) _skillTree._audioSource.pitch = _skillTree._skillLevels[id];
        else _skillTree._audioSource.pitch = 1 + (_skillTree._skillLevels[id] * 0.5f);
        _skillTree._audioSource.Play();
    }
}
