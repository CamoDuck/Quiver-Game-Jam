using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class MainMenuScript : MonoBehaviour
{

    private bool gameStart;
    public GameObject player;

    public Image titleScreen;
    public Image cutscene1;
    public Image cutscene2;
    public Image cutscene3;
    public Image noPeeking;



    void Awake()
    {
        StartCoroutine(StartCutscene());
    }

    void Update()
    {

    }

    IEnumerator StartCutscene()
    {
        while (!Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }

        while (titleScreen.color.a > 0)
        {
            titleScreen.color = new Color(titleScreen.color.r, titleScreen.color.g, titleScreen.color.b, titleScreen.color.a - 0.05f);
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.5f);








        while (!Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }

        while (cutscene1.color.a > 0)
        {
            cutscene1.color = new Color(cutscene1.color.r, cutscene1.color.g, cutscene1.color.b, cutscene1.color.a - 0.05f);
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.5f);






        while (!Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }


        while (cutscene2.color.a > 0)
        {
            cutscene2.color = new Color(cutscene2.color.r, cutscene2.color.g, cutscene2.color.b, cutscene2.color.a - 0.05f);
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.5f);







        while (!Input.anyKey)
        {
            yield return new WaitForEndOfFrame();
        }        

        while (cutscene3.color.r > 0)
        {
            cutscene3.color = new Color(cutscene3.color.r - 0.05f, cutscene3.color.g - 0.05f, cutscene3.color.b - 0.05f, 1);
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(1.0f);






        while (cutscene3.color.a > 0)
        {
            cutscene3.color = new Color(cutscene3.color.r, cutscene3.color.g, cutscene3.color.b, cutscene3.color.a - 0.05f);
            noPeeking.color = new Color(noPeeking.color.r, noPeeking.color.g, noPeeking.color.b, noPeeking.color.a - 0.05f);
            yield return new WaitForSeconds(0.025f);
        }

        this.gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().playerCanMove = true;
    }
}
