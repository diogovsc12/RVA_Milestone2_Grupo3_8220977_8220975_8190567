using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;

public class ARPlacementWithMultiple : MonoBehaviour
{
    
    [SerializeField]
    private Button arLampButton;

    [SerializeField]
    private Button argarrafaButton;

    [SerializeField]
    private Button arMesinhaButton;

    [SerializeField]
    private TMP_Text selectionText;

    public GameObject placedPrefab;
    public ARRaycastManager aRRaycastManager;

    private GameObject canvas;
    private int countLamp, countGarrafa, countMesinha;

    private string _nprefab;

    private int contador;
    public GameObject placementIndicator;

    

    Pose pose;
    bool placementIsValid;

    void Awake()
    {
        
        arLampButton.onClick.AddListener(()=> ChangePrefabTo("lamp"));
        argarrafaButton.onClick.AddListener(() => ChangePrefabTo("garrafa"));
        arMesinhaButton.onClick.AddListener(() => ChangePrefabTo("mesinha"));

    }

    void ChangePrefabTo(string prefabName)
    {
        placedPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

        if(placedPrefab == null)
        {
            Debug.LogError($"Prefab com o nome {prefabName} não consegue ser carregado, verifica o nome dos prefabs");

        }

        switch (prefabName)
        {
            case "lamp":
                selectionText.text = $"Objecto Seleccionado: {prefabName}";
                _nprefab = prefabName;
                break;
            case "garrafa":
                selectionText.text = $"Objecto Seleccionado:{prefabName} ";
                _nprefab = prefabName;
                break;
            case "mesinha":
                selectionText.text = $"Objecto Seleccionado:{prefabName} ";
                _nprefab = prefabName;
                break;
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
        placementIsValid = false;
        this.canvas = GameObject.Find("Canvas");
        countLamp = 0;
        countGarrafa = 0;
        countMesinha = 0;

    }

    // Update is called once per frame
    void Update()
    {
       
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if(placementIsValid && Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began )
        {
            placeObject();
        }

    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementIsValid = hits.Count > 0;
        if (placementIsValid)
        {
            pose = hits[0].pose;
            var cameraFoward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraFoward.x, 0, cameraFoward.z).normalized;
            pose.rotation = Quaternion.LookRotation(cameraBearing); // objeto sempre a olhar para a camera ( inicial)
           
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(pose.position,
            pose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void placeObject()
    {
        bool flag = false;
        switch (_nprefab)
        {
            case "lamp":
                if (countLamp <= 2)
                {
                    countLamp++;
                    flag = true;
                }
                break;
            case "garrafa":
                if (countGarrafa <= 2)
                {
                    countGarrafa++;
                    flag = true;
                }
                break;
            case "mesinha":
                if (countMesinha <= 2)
                {
                    countMesinha++;
                    flag = true;
                }
                break;

            default:
                break;
        }

        if (flag)
        {
            Instantiate(placedPrefab, pose.position, pose.rotation);
            Collider[] collider1 = Physics.OverlapSphere(pose.position, 0.3f);
            if (collider1.Length > 4)
            {
                this.canvas.SendMessage("ChangeText", "Distância Mínima não cumprida");
                this.canvas.SendMessage("ChangeTextColor", Color.red);
            }
        }

    }
}
