using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script updates skilltree on restart, so the DontDestroyOnLoad does not f**k up.

public class SetHolders : MonoBehaviour
{
    public GameObject ConnectorHolder;
    public GameObject SkillTreeHolder;
    
    
    private void Start()
    {
        SkillTree skillTree = GameObject.Find("SkillTreeManager").GetComponent<SkillTree>();
        skillTree._connectorHolder = ConnectorHolder;
        skillTree._skillHolder = SkillTreeHolder;
        skillTree.InitializeSkillTree();
    }
}
