using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    private LevelGrid levelGrid;
    [SerializeField] private GameObject prefabPeople;
    [SerializeField] private List<GameObject> peoples;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start(){
        levelGrid = new LevelGrid(19, 9, 5, 10);
        addPeople();
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void addPeople(int n = 1) {
        for (int i = 0; i < n; i++) {
            Vector3 pos = levelGrid.SpawnPosition();
            
            if (freeGridPos(pos) == null) {
                peoples.Add(Instantiate(prefabPeople, pos, Quaternion.identity));
            } else i--;
            
        }
    }

    public void removePeople(GameObject people) { 
        peoples.Remove(people);
    }

    public GameObject freeGridPos(Vector3 pos) {

        foreach (var people in peoples) {
            if (people.transform.position == pos) {
                return people;
            }
        }
        foreach (var follower in player.peopleFollowing) {
            if (follower.getGameObject().transform.position == pos) {
                return follower.getGameObject();
            }
        }

        return null;
    }
}
