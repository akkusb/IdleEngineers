using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticHorizontalSize : MonoBehaviour
{
    public float childWidth = 0f;

    // Use this for initialization
    void Start()
    {
        AdjustSize();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AdjustSize()
    {
        Vector2 size = this.GetComponent<RectTransform>().sizeDelta;
        if(childWidth > 0){
            
        }
        else
        {
            if(this.transform.childCount > 0)
            {
                childWidth = Screen.width / this.transform.childCount;
            }
            else
            {
                return;
            }
            
        }

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            Vector2 childSize = child.GetComponent<RectTransform>().sizeDelta;
            childSize.x = childWidth;
            child.GetComponent<RectTransform>().sizeDelta = childSize;
        }
        size.x = this.transform.childCount * childWidth;

        this.GetComponent<RectTransform>().sizeDelta = size;
    }
}
