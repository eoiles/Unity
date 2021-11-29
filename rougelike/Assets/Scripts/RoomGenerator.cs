using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }


    public Direction direction;


    [Header("RoomInformation")] public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("PositionControl")] public Transform generatorPoint;
    public float xoffset;
    public float yoffset;
    public LayerMask roomLayer;
    public List<Room> rooms = new List<Room>();

    public int maxStep;

    private List<GameObject> farRooms = new List<GameObject>();
    private List<GameObject> lessFarRooms = new List<GameObject>();

    private List<GameObject> oneWayRooms = new List<GameObject>();


    // Start is called before the first frame update
    private void Start()
    {
        for (var i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            ChangePointPosition();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        endRoom = rooms[0].gameObject;


        //find endroom
        foreach (var room in rooms)
        {
            // if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            // {
            //     endRoom = room.gameObject;
            // }

            setupRoom(room, room.transform.position);
        }

        findEndRoom();
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    public void ChangePointPosition()
    {
        do
        {
            direction = (Direction) Random.Range(0, 4);

            switch (direction)
            {
                case Direction.Up:
                    generatorPoint.position += new Vector3(0, yoffset, 0);
                    break;
                case Direction.Down:
                    generatorPoint.position += new Vector3(0, -yoffset, 0);
                    break;
                case Direction.Left:
                    generatorPoint.position += new Vector3(-xoffset, 0, 0);
                    break;
                case Direction.Right:
                    generatorPoint.position += new Vector3(xoffset, 0, 0);
                    break;
                default:
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }


    public void setupRoom(Room newRoom, Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yoffset, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yoffset, 0), 0.2f, roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xoffset, 0, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xoffset, 0, 0), 0.2f, roomLayer);

        newRoom.updateRoom();
    }

    public void findEndRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }


        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
            {
                farRooms.Add(room.gameObject);
            }

            if (room.stepToStart == maxStep - 1)
            {
                lessFarRooms.Add(room.gameObject);
            }
        }

        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
            {
                oneWayRooms.Add(farRooms[i]);
            }
        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
            {
                lessFarRooms.Add(lessFarRooms[i]);
            }
        }

        if (oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
    }
}