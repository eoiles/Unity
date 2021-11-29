using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorLeft, doorRight, doorUp, doorDown;

    public bool roomLeft, roomRight, roomUp, roomDown;

    public Text text;

    public int stepToStart;

    public int doorNumber;


    void Start()
    {
        doorLeft.SetActive(roomLeft);
        doorRight.SetActive(roomRight);
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
    }

    public void updateRoom()
    {
        int x = (int) GameObject.Find("RoomGenerator").GetComponent<RoomGenerator>().xoffset;
        int y = (int) GameObject.Find("RoomGenerator").GetComponent<RoomGenerator>().yoffset;


        stepToStart = (int) (Mathf.Abs(transform.position.x / x) + Mathf.Abs(transform.position.y / y));
        text.text = stepToStart.ToString();

        if (roomUp)
            doorNumber++;
        if (doorDown)
            doorNumber++;
        if (doorLeft)
            doorNumber++;
        if (doorRight)
            doorNumber++;
    }


}