using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Colisao: MonoBehaviour
{

    private Transform place;
    private GameObject canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        this.place = GetComponent<Transform>();
        this.canvas = GameObject.Find("Canvas");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        Collider[] collider1 = Physics.OverlapSphere(this.place.position, 0.3f);
        this.canvas.SendMessage("ChangeText", collider1.Length);
        if (collider1.Length > 1)
        {
            this.canvas.SendMessage("ChangeText", collider1.Length);
            this.canvas.SendMessage("ChangeTextColor", Color.red);
        }
        else
        {
            this.canvas.SendMessage("ChangeTextColor", Color.white);
            this.canvas.SendMessage("ChangeText", collider1.Length);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        this.canvas.SendMessage("ChangeText", 9999);
    }

}
