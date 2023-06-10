using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FollowerNumber : MonoBehaviour
{
    // this code just displays the number of followers


    public int followerCount; // replace this with follower count in different script?
    public TMP_Text followerCountText;

    void Start()
    {
        followerCountText = GetComponent<TMP_Text>();
    }

    
    void Update()
    {
        followerCountText.text = followerCount.ToString();
    }
}
