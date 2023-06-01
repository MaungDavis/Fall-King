using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public GameObject DestinationLevel;
    public Camera MainCamera;
    public GameObject player;
    float timer = 0.0f;

    private void Update()
    {
        Debug.Log("plyer: " + this.player.transform.position.y);
        
        
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= 5.0f)
        {
            Debug.Log(this.DestinationLevel.transform.position.x + " " + this.DestinationLevel.transform.position.y);
            
            this.MainCamera.transform.position = new Vector3(this.DestinationLevel.transform.position.x, this.DestinationLevel.transform.position.y, this.MainCamera.transform.position.z);
        }
    }
}
