using System.Collections;
using UnityEngine;

// Script for handling the spawning of fruits and bombs in Fruit Ninja
public class FruitSpawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject[] fruitPrefabs;

    public GameObject bombPrefab;

    [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 0.25f;
    public float maxSpawnDelay = 1f;

    [Header("Fruit Physics")]
    public float minAngle = -15f;
    public float maxAngle = 15f;
    public float minForce = 18f;
    public float maxForce = 22f;

    [Header("Fruit Lifetime")]
    public float maxLifetime = 5f;

    private void Awake() {
        spawnArea = GetComponent<Collider>();

    }

    private void OnEnable() {
        StartCoroutine(Spawn());
    }

    private void OnDisable() {
        StopAllCoroutines();
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(2f);

        while (enabled) {
            // Picks a random fruit to spawn
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            // Spawns a bomb randomly
            if (Random.value < bombChance) {
                prefab = bombPrefab;
            }

            // Spawns the fruits at a random place within the spawn area
            Vector3 position = new Vector3();
            position.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            // Shoots the fruits up at a random angle
            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLifetime);

            // Adds a random amount of force to how the fruit shoots up
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            // Waits for a set amount of time before spawning a new fruit
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}
