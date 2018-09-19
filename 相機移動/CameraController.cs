using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    [Header("玩家物件")]
    public GameObject player;

    [Header("相對位移")]
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
        //相對位移 = 當前物件(攝影機)座標-玩家物件座標
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
        //當前物件座標 = 玩家物件座標 + 相對位移
	}
}
