using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// SkillTree and Skill scripts are partly from tutorial https://www.youtube.com/watch?v=fE0R6WLpmrE
// but I have redone some of the base structure, because I did not like it. I also made it a DontDestroyOnLoad so
// the skills dont reset if you restart level.
// Also did not come up with any skills for a simple snek game. So I just added music tracks instead of actual skills.

public class SkillTree : MonoBehaviour
{
    private static SkillTree instance;
    
    [NonSerialized]public int[] _skillLevels;
    [NonSerialized]public int[] _skillCaps;
    [NonSerialized]public string[] _skillNames;
    [NonSerialized]public string[] _skillDescriptions;
    
    [NonSerialized]public List<Skill> _skillList = new List<Skill>();
    [NonSerialized]public GameObject _skillHolder;
    
    [NonSerialized]public List<GameObject> _connectorList = new List<GameObject>();
    [NonSerialized]public GameObject _connectorHolder;

    [NonSerialized]public static int _skillPoint = 0;
    
    public static int _scoreNeededForSkillPoint = 40;

    [NonSerialized] public static UnityEvent updateSkillTreeUI = new UnityEvent();
    public List<AudioClip> songs;
    [NonSerialized]public AudioSource _audioSource;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        
        _audioSource = GetComponent<AudioSource>();
    }

    public void InitializeSkillTree()
    {
        updateSkillTreeUI.AddListener(UpdateAllSkillUI);
        
        if (_skillLevels == null) _skillLevels = new int[6];
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
        
        _skillList.Clear();
        _connectorList.Clear();
        
        foreach (Skill skill in _skillHolder.GetComponentsInChildren<Skill>())
        {
            _skillList.Add(skill);
            skill._skillTree = this;
            skill._skillBoxColor = skill.gameObject.GetComponent<Image>();
        }
        foreach (RectTransform connector in _connectorHolder.GetComponentsInChildren<RectTransform>()) _connectorList.Add(connector.gameObject);

        for (int i = 0; i < _skillList.Count; i++) _skillList[i].id = i;

        _skillList[0].connectedSkills = new[] {1};
        _skillList[1].connectedSkills = new[] {2,3};
        _skillList[2].connectedSkills = new[] {4,5};
        
        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in _skillList) skill.UpdateUI();
    }
    
}
