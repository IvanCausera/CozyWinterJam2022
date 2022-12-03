using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour {

    [SerializeField] private List<Sprite> happyPeople;
    [SerializeField] private List<Sprite> sadPeople;

    private int spritePos;

    public bool happy;
    public int life;

    private void Setup(Vector3 position, bool happy) {
        transform.position = position;
        this.happy = happy;
    }

    /// <summary>
    /// Reduce life in one
    /// </summary>
    /// <returns>True if its dead, false if its alive</returns>
    public bool hit() {
        life--;
        if (life <= 0) {
            gameObject.GetComponent<SpriteRenderer>().sprite = happyPeople[spritePos];
            happy = true;
            return true;
        }
        return false;
    }

    public void setPosition(Vector3 pos) {
        transform.position = pos;
    }

    public GameObject getGameObject() {
        return this.gameObject;
    }

    // Start is called before the first frame update
    void Start() {
        spritePos = Random.Range(0, sadPeople.Count);
        life = 1;
        gameObject.GetComponent<SpriteRenderer>().sprite = sadPeople[spritePos];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
