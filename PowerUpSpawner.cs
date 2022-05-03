using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    public List<GameObject> powerUps = new List<GameObject>();

    public float spawnDelay = 25;
    public float offset;
    private float _spawnTimer = 0;

	void Update () {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= spawnDelay && powerUps.Count != 0)
        {
            _spawnTimer = 0;
            int random = Random.Range(0, powerUps.Count);

            float xCoord = Random.Range(-this.transform.localScale.x, this.transform.localScale.x)* offset;
            float zCoord = Random.Range(-this.transform.localScale.z, this.transform.localScale.z)* offset;

            GameObject newPowerUp = Instantiate(powerUps[random]);
            newPowerUp.transform.position = new Vector3(xCoord,.5f,zCoord);
        }
	}
}
