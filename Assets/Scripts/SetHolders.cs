using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHolders : MonoBehaviour
{
    public GameObject ConnectorHolder;
    public GameObject SkillTreeHolder;
    
    
    private void Start()
    {
        SkillTree skillTree = GameObject.Find("SkillTreeManager").GetComponent<SkillTree>();
        skillTree.InitializeSkillTree();
    }
}
