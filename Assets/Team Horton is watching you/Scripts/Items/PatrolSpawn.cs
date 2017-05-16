using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpawn : MonoBehaviour
{
    public Chase patrollerPrefab = null;
    protected List<Chase> patrol = new List<Chase>();
    public float timeBetweenSpawns = 1f;
    [SerializeField]
    protected bool debug = false;
    protected bool busy = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (debug && Input.GetKeyDown(KeyCode.K) && patrol.Count > 0)
        {
            Chase patroller = patrol[patrol.Count - 1];
            patrol.RemoveAt(patrol.Count - 1);
            Destroy(patroller.gameObject);
        }

    }

    public void alert(GameObject target, int nbPatrollers = 1)
    {
        if (patrollerPrefab == null)
            throw new System.Exception("PatrolSpawn: Patroller Prefab field is empty.");

        if (!busy)
            StartCoroutine(spawnDelay(target, nbPatrollers));
    }

    protected IEnumerator spawnDelay(GameObject target, int nbPatrollers)
    {
        busy = true;

        nbPatrollers -= patrol.Count;

        Chase patrollerInstance;
        for (int i = 0; i < nbPatrollers; ++i)
        {
            patrollerInstance = Instantiate(patrollerPrefab, transform.position, transform.rotation);
            patrollerInstance.target = target.transform;
            patrol.Add(patrollerInstance);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        busy = false;
    }
}
