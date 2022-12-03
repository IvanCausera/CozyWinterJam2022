using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid {

    private Vector2Int peopleGridPosition;
    private int width;
    private int height;
    private int min;
    private int increase;

    public LevelGrid(int width, int height, int min, int increase) {
        this.width = width;
        this.height = height;
        this.min = min;
        this.increase = increase;
    }

    public Vector3 SpawnPosition() {
        return new Vector3((Random.Range(0, width) * increase) + min, (Random.Range(0, height) * increase) + min);
    }

}
