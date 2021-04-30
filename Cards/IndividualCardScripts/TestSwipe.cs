using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSwipe : MonoBehaviour
{
    public Text swipeText;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    private void Update()
    {
        Swipe();
    }

    //fast swipe
    public void Swipe() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;//get first position of touch
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            currentPosition = Input.GetTouch(0).position; // where current finger is
            Vector2 Distance = currentPosition - startTouchPosition;

            if (!stopTouch) {
                if (Distance.x < -swipeRange)
                {
                    swipeText.text = "left";
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    swipeText.text = "right";
                    stopTouch = true;
                }
            }
        }


    }
}
