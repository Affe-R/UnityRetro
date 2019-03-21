using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Vector3 bgStartPosition;
    public Vector3 bgEndPosition;
    [SerializeField]Transform bgTransform;
    public float beginMoveTime;
    public float bgMoveDuration;
    [SerializeField]MouseWidget mouseWidget;

    [SerializeField] Transform highScoreField;
    [SerializeField]GameObject highScorePrefab;
    string currentHighScore;

    Transform[] activeHS;
    //Vector3 mousePosition;
    //Vector3 mouseLastPosition;
    //Vector3 mouseDelta;

    private void Start()
    {
        activeHS = new Transform[4];
        UpdateHighscore();
        bgStartPosition = bgTransform.position;
        StartCoroutine(AnimateBG(bgTransform, bgStartPosition, bgEndPosition, bgMoveDuration, beginMoveTime));
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Playing", LoadSceneMode.Single);
    }

    public void UpdateHighscore()
    {
        highScoreField.GetComponent<Text>().text = "PHD:150125";
    }

    public void Quit()
    {
        print("Quit");
        Application.Quit();

    }

    private void Update()
    {
        Vector3 mousePos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        mouseWidget.UpdatePosition(mousePos);
    }


    IEnumerator AnimateBG(Transform _transform, Vector3 _startPosition, Vector3 _endPosition, float duration, float startTime = 0)
    {
        if(bgTransform == null || duration <= 0) yield return null;

        float elapsedTime = 0;
        float animationTime = 0;

        while (elapsedTime < duration + startTime)
        {
            if (elapsedTime > startTime) animationTime += Time.deltaTime;
            _transform.position = Vector3.Lerp(bgStartPosition, bgEndPosition, (animationTime / duration));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
