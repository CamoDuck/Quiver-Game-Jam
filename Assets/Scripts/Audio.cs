using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource m_Background, m_Battle1, m_Battle2, m_Dialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        //AudioSource m_MyAudioSource = GetComponent<AudioSource>();
        PlayBackground();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayBackground()
    {
        m_Battle1.Stop();
        m_Battle2.Stop();
        m_Background.Play();
    }

    void PlayBattle1()
    {
        m_Background.Stop();
        m_Battle2.Stop();
        m_Battle1.Play();
    }

    void PlayBattle2()
    {
        m_Background.Stop();
        m_Battle1.Stop();
        m_Battle2.Play();
    }
    
    void PlayDialogue()
    {
        m_Dialogue.Play();
    }
}
