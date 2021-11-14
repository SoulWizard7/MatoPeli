using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillTree;

public class Skill : MonoBehaviour
{
    [NonSerialized]public int id;

    public TMP_Text titleText;
    public TMP_Text descText;

    public int[] connectedSkills;

    public void UpdateUI()
    {
        titleText.text = $"{skillTree._skillLevels[id]}/{skillTree._skillCaps[id]} \n {skillTree._skillNames[id]}";

        if (skillTree._skillLevels[id] != skillTree._skillCaps[id])
        {
            descText.text = $"{skillTree._skillDescriptions[id]} \n cost: 1 sp";
        }
        else descText.text = $"{skillTree._skillDescriptions[id]}";

        GetComponent<Image>().color = skillTree._skillLevels[id] >= skillTree._skillCaps[id] ? Color.yellow
            : skillTree._skillPoint >= 1 ? Color.green : Color.white;

        foreach (var connectedSkill in connectedSkills)
        {
            skillTree._skillList[connectedSkill].gameObject.SetActive(skillTree._skillLevels[id] > 0);
            skillTree._connectorList[connectedSkill].SetActive(skillTree._skillLevels[id] > 0);
        }
    }

    public void Buy()
    {
        if (skillTree._skillPoint < 1 || skillTree._skillLevels[id] >= skillTree._skillCaps[id]) return;

        skillTree._skillPoint -= 1;
        skillTree._skillLevels[id]++;
        skillTree.UpdateAllSkillUI();
        HighScore.score.Invoke(0);
    }

    public void PlaySong()
    {
        if (skillTree._skillLevels[id] == 0) return;
        
        skillTree._audioSource.Stop();
        skillTree._audioSource.clip = skillTree.songs[id];
        if (skillTree._skillLevels[id] == 1) skillTree._audioSource.pitch = skillTree._skillLevels[id];
        else skillTree._audioSource.pitch = 1 + (skillTree._skillLevels[id] * 0.5f);
        skillTree._audioSource.Play();
    }
}
