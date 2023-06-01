using Cinemachine;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject previousLevel;
    [SerializeField] private GameObject nextLevel;
    [SerializeField] private Transform newRespawnLevel;

    private Transform newRespawnPoint;
    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {   
        if (this.newRespawnLevel != null)
        {
            this.newRespawnPoint = newRespawnLevel.Find("StageRespawnPoint");
        }
        this.virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            virtualCamera.Follow = nextLevel.transform;
            
            if (this.newRespawnPoint != null)
            {
                other.gameObject.GetComponent<PlayerController>().setRespawnPoint(this.newRespawnPoint);
            }
        }
    }
}
