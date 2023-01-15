using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    public Camera currentCam;

    Pose pose;
    public ARRaycastManager aRRaycastManager;
    bool placementIsValid;
   


    // Start is called before the first frame update
    void Start()
    {
        placementIsValid = false;
        
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
        var screenCenter = currentCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementIsValid = hits.Count > 0;
        if (placementIsValid)
        {
            pose = hits[0].pose;
            var cameraFoward = currentCam.transform.forward;
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
        Instantiate(objectToPlace, pose.position, pose.rotation);
    }
}
