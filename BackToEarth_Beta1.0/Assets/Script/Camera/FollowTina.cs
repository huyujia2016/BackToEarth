using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTina : MonoBehaviour {

    private GameObject player;
    public float smoothing = 4;
    public float TopEdge;
    public float BottomEdge;
    public float LeftEdge;
    public float RightEdge;



    // Use this for initialization
    void Start()
    {
        player = GameManager._instance.Player;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPos = player.transform.position;
        Vector3 MovePos = playerPos;
        MovePos.z = -10f;
        if (playerPos.y >= TopEdge)
        {
            MovePos.y = TopEdge;
        }
        if (playerPos.y <= BottomEdge)
        {
            MovePos.y = BottomEdge;
        }
        if (playerPos.x <= LeftEdge)
        {
            MovePos.x = LeftEdge;
        }
        if (playerPos.x >= RightEdge)
        {
            MovePos.x = RightEdge;
        }
        transform.position = Vector3.Lerp(transform.position, MovePos, smoothing * Time.deltaTime);
    }
}
