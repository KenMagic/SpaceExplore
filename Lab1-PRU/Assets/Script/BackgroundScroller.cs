using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    // instantiate the scroller
    public static BackgroundScroller Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private float scrollSpeed = 0.2f;

    [SerializeField]
    private Renderer bgRenderer;


    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
    // Method to get the current scroll speed
    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }

    // Method to set the scroll speed
    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }
}