using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<People> peopleFollowing;

    [SerializeField] private int directionScale = 10;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameHandler gameHandler;

    private Vector2Int gridPosition;
    private Vector2Int gridMoveDirection;
    private Vector2Int nextMove;
    private List<Vector3> movedPositionList;

    private float gridMoveTimerMax;
    private float gridMoveTimer;
    private bool moveDecided = false;
    private int peopleTail = 0;

    private void Awake() {
        gridPosition = new Vector2Int(5, 5);
        gridMoveDirection = new Vector2Int(1, 0);

        gridMoveTimerMax = 1f;
        gridMoveTimer = gridMoveTimerMax;
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        peopleFollowing = new List<People>();
        movedPositionList = new List<Vector3>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        playerMovement();

        gridMovement();
        
    }

    private void playerMovement() {
        if (!moveDecided) {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
                moveDecided = true;
            } else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
                moveDecided = true;
            } else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
                moveDecided = true;
            } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
                moveDecided = true;
            }
            arrow.transform.eulerAngles = new Vector3(0, 0, getAngleFromVector(gridMoveDirection));
        }
    }

    private void gridMovement() {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax) {
            gridMoveTimer -= gridMoveTimerMax;

            if (moveDecided) { // if move decided you can move if not you need to whait for another turn
                nextMove = gridPosition + (gridMoveDirection * directionScale);

                GameObject people = gameHandler.freeGridPos(new Vector3(nextMove.x, nextMove.y));

                if (people == null) { //if grid position is free
                    movedPositionList.Insert(0, new Vector3(gridPosition.x, gridPosition.y)); //Tail handler

                    gridPosition = nextMove;

                    tailCheck();

                    transform.position = new Vector3(gridPosition.x, gridPosition.y);
                } else { //Gird position isnt free
                    People pLife = people.GetComponent<People>();
                    if (!pLife.happy) {
                        if (pLife.hit()) {
                            peopleTail++;
                            peopleFollowing.Add(pLife);
                            gameHandler.removePeople(people);
                            gameHandler.addPeople(1);
                        }
                        //movedPositionList.Insert(0, new Vector3(gridPosition.x, gridPosition.y)); //Tail handler
                        tailCheck();
                    }
                }
                moveDecided = false;
            }
        }
    }

    private void tailCheck() {
        if (movedPositionList.Count >= peopleTail + 6) { //Tail handler
            movedPositionList.RemoveAt(movedPositionList.Count - 1);
        }

        for (int i = 0; i < peopleFollowing.Count; i++) {
            peopleFollowing[i].setPosition(movedPositionList[i]);
        }
    }

    private float getAngleFromVector(Vector2Int dir) {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
