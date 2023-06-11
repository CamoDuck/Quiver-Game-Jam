using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
	public AudioSource m_BGAmbient, m_BGMusic, m_Battle1, m_Battle2, m_Dialogue, m_Select, m_Anxiety;
	
	// Start is called before the first frame update
	void Start()
	{
		PlayBackground(0.0f);
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void PlayBGAfter()	{ m_BGMusic.Play();	m_BGAmbient.Play(); }

	private void PlayBattle1After()	{ m_Battle1.Play();	}

	private void PlayBattle2After()	{ m_Battle2.Play();	}

	private void PlayAnxietyAfter() { m_Anxiety.Play(); }

	void PlayBackground(float after=0.0f)
	{
		m_Battle1.Stop();
		m_Battle2.Stop();
		m_Anxiety.Stop();

		Invoke(nameof(PlayBGAfter), after);
	}

	void PlayBattle1(float after=0.0f)
	{
		m_Battle2.Stop();
		m_Anxiety.Stop();
		m_BGMusic.Stop();
		m_BGAmbient.Stop();
		
		Invoke(nameof(PlayBattle1After), after);
	}

	void PlayBattle2(float after=0.0f)
	{
		m_Battle1.Stop();
		m_Anxiety.Stop();
		m_BGMusic.Stop();
		m_BGAmbient.Stop();

		Invoke(nameof(PlayBattle2After), after);
	}
	
	void PlayAnxiety(float after=0.0f)
	{
		m_Battle1.Stop();
		m_Battle2.Stop();
		m_BGMusic.Stop();
		m_BGAmbient.Stop();
		
		Invoke(nameof(PlayAnxietyAfter), after);
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
