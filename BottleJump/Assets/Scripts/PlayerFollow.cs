using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    BottleManager player;
    private void Start()
    {
        player = FindObjectOfType<BottleManager>();
    }
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 2, 6.75f, -7f);
    }
}
