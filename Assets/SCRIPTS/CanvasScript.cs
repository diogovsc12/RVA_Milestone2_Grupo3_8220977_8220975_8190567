using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.XR.ARFoundation;

public class CanvasScript : MonoBehaviour
{

    public GameObject canvas;
    public TMP_Text textObj;

    public GameObject menuPanel;
    public Button dismissButton;
    

    void Awake()
    {
        dismissButton.onClick.AddListener(Dismiss);
    }

    private void Dismiss() => menuPanel.SetActive(false);

    // Start is called before the first frame update
    void Start()
    {
        this.canvas = GameObject.FindWithTag("Canvas");

    }

    // Update is called once per frame
    void Update()
    {
        if (menuPanel.activeSelf)
            return;
    }

    void ChangeTextColor(Color color )
    {
        textObj.color = color;
        
    }

    void ChangeText(string frase)
    {
        textObj.text = frase;
    }

}
