using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    [NonSerialized]public int[] _skillLevels;
    [NonSerialized]public int[] _skillCaps;
    [NonSerialized]public string[] _skillNames;
    [NonSerialized]public string[] _skillDescriptions;
    
    [NonSerialized]public List<Skill> _skillList = new List<Skill>();
    public GameObject SkillHolder;
    
    [NonSerialized]public List<GameObject> _connectorList = new List<GameObject>();
    public GameObject connectorHolder;

    [NonSerialized]public static int _skillPoint = 1;
    
    public static int _scoreNeededForSkillPoint = 40;

    public List<AudioClip> songs;
    [NonSerialized]public AudioSource _audioSource;

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
