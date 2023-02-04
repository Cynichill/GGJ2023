using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateFixer : MonoBehaviour
{

    //Unity updates have slightly broken the way save and load work.
    //It can no longer grab instances from disabled objects, so we need to enable everything for a frame.

    void Start()
    {
       //This is dumb and hacky but whatever
        this.gameObject.SetActive(false);

    }

}
