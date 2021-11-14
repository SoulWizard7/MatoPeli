using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;

    private void Awake()
    {
        skillTree = this;
        _audioSource = GetComponent<AudioSource>();
    }
    
    [HideInInspector]public int[] _skillLevels;
    [HideInInspector]public int[] _skillCaps;
    [HideInInspector]public string[] _skillNames;
    [HideInInspector]public string[] _skillDescriptions;
    
    [HideInInspector]public List<Skill> _skillList;
    public GameObject SkillHolder;
    
    [HideInInspector]public List<GameObject> _connectorList;
    public GameObject connectorHolder;

    [HideInInspector]public int _skillPoint = 1;
    
    public int _scoreNeededForSkillPoint = 150;

    public List<AudioClip> songs;
    [HideInInspector]public AudioSource _audioSource;

    private void Start()
    {
        InitializeSkillTree();
        UpdateAllSkillUI();
    }

    private void InitializeSkillTree()
    {
        _skillLevels = new int[6];
        _skillCaps = new[] {1, 5, 5, 5, 5, 5, 5};

        _skillNames = new[] {"Rock Music", "Epic Music", "Battle Music", "Sexy Music", "Sci-Fi Music", "Sad Music"};
        _skillDescriptions = new[]
        {
            "Groovy",
            "Such Epicness",
            "Ready for war?",
            "mmm... *kiss*",
            "Kinda Hiphopy?",
            "Sad violin face"
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) _skillList.Add(skill);
        foreach (var connector in connectorHolder.GetComponentsInChildren<RectTransform>()) _connectorList.Add(connector.gameObject);

        for (int i = 0; i < _skillList.Count; i++) _skillList[i].id = i;

        _skillList[0].connectedSkills = new[] {1};
        _skillList[1].connectedSkills = new[] {2,3};
        _skillList[2].connectedSkills = new[] {4,5};
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in _skillList) skill.UpdateUI();
    }
    
}
