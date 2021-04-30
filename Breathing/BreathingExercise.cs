using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class BreathingExercise : MonoBehaviour
{
    private int startExercise = 0;
    private float totalTime = 180;
    private float timeRemaining = 180;
    public Text remainingTimeText;
    public Slider remainingTimeSlider;

    public GameObject StartButton;

    private float timeToWaitBeforeStart = 4;
    public GameObject StartCountDownObject;
    public GameObject FadeInObjects;

    public GameObject FinishTimeLine;
    public GameObject removeObject;
    public string SceneToLoad;

    public void onClickStartExercise() {
        StartButton.GetComponent<Button>().enabled = false;
        StartCountDownObject.SetActive(true);
        startExercise = -1;
    }

    public void onClickFinishedExercise() {
        SceneManager.LoadScene(SceneToLoad);
    }

    private void waitSecond() {
        timeToWaitBeforeStart -= Time.deltaTime;
        if (timeToWaitBeforeStart < 0) {
            startExercise = 1;
        }

        if (timeToWaitBeforeStart < 0.4f)
        {
            FadeInObjects.SetActive(true);
        }
    }



    void Update()
    {
        if (startExercise == -1) {
            waitSecond();
        } else if (startExercise == 1) {
            gameObject.GetComponent<PlayableDirector>().Play();

            updateTimeRemaining();
        } else if (startExercise == 2) {
            gameObject.GetComponent<PlayableDirector>().Stop();

            FinishTimeLine.SetActive(true);
            Debug.Log("FINISHED");
        }
    }

    private void updateTimeRemaining() {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);

            if (minutes < 0) {
                minutes = 0;
            }

            if (seconds < 0) {
                seconds = 0;
            }

            remainingTimeText.text = "Time left: " + minutes + ":" + seconds;

            remainingTimeSlider.value = (totalTime - timeRemaining) / totalTime;
        }
        else {
            startExercise = 2;
        }
    }
}
