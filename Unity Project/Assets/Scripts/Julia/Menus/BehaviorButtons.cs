﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BehaviorButtons : MonoBehaviour
{
    public Button btn;
    public RectTransform size;
    public Vector3 scale;
    public bool mouseOnButton;

    // Start is called before the first frame update
    void Awake()
    {
        btn = GetComponent<Button>();
        size = GetComponent<RectTransform>();
        scale = size.localScale;
    }

    public void Hover()
    {
        size.localScale = scale * 1.1f;
    }

    public void NotHover()
    {
        size.localScale = scale;
    }
}
