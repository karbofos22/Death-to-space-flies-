using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private SpawnManager spawnManager;
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
