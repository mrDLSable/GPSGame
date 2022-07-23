using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> Skills;

    public static SkillManager _skillManager;

    // Start is called before the first frame update
    void Start()
    {
        _skillManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
