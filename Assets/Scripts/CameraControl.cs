using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x,player.transform.position.y,-10f);
        transform.position = playerPos;
    }
}
