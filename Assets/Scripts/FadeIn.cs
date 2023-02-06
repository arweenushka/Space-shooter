using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//class for fading canvas element. element will dissapear after certain time
public class FadeIn : MonoBehaviour
{
    public TextMeshProUGUI excelentText;
    public TextMeshProUGUI nextWaveText;

    // Start is called before the first frame update
    void Start()
    {
        //config for fading text
        excelentText.canvasRenderer.SetAlpha(0.0f);
        nextWaveText.canvasRenderer.SetAlpha(0.0f);

        //show slowly excelent text
        FadeExcelentText();

        //added for longer pause. will work without it as well
        StartCoroutine(WaitAndLoadNextScene());

    }

    void Update()
    {
        //when excelent text is appeared , then hide it and show next wave text
        if (excelentText.canvasRenderer.GetAlpha() == 1)
        {
            FadeOutExcelentText();
            FadeNextWaveText();
        }

    }

    void FadeExcelentText()
    {
        excelentText.CrossFadeAlpha(1, 2, false);
    }

    void FadeOutExcelentText()
    {
        excelentText.CrossFadeAlpha(0, 2, false);
    }

    void FadeNextWaveText()
    {
        nextWaveText.CrossFadeAlpha(1, 2, false);
    }

    //load next level with delay
    IEnumerator WaitAndLoadNextScene()
    {
        yield return new WaitForSeconds(2);
        FindObjectOfType<GameManager>().LoadNextScene();
    }
}
