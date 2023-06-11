using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
	public AudioSource m_BGAmbient, m_BGMusic, m_Battle1, m_Battle2, m_Dialogue, m_Select, m_Anxiety;
	
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
		//m_Dialogue.Stop();
		//m_Select.Stop();
		m_Anxiety.Stop();

		m_BGMusic.Play();
		m_BGAmbient.Play();
		
	}

	void PlayBattle1()
	{
		m_Battle1.Play();
		m_Battle2.Stop();
		m_Anxiety.Stop();

		m_BGMusic.Stop();
		m_BGAmbient.Stop();
	}

	void PlayBattle2()
	{
		m_Battle1.Stop();
		m_Battle2.Play();
		m_Anxiety.Stop();

		m_BGMusic.Stop();
		m_BGAmbient.Stop();
	}
	
	void PlayAnxiety()
	{
		m_Battle1.Stop();
		m_Battle2.Stop();
		m_Anxiety.Play();

		m_BGMusic.Stop();
		m_BGAmbient.Stop();
	}

	void PlayDialogue()
	{
		m_Dialogue.Play();
	}

	void PlaySelect()
	{
		m_Select.Play();
	}
}
