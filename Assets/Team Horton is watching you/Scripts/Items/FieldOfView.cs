using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
    public float maxRadius = 6f;
    public float maxAngle = 35f;

    [SerializeField] private bool debug = false;
    private float logInterval = 0.5f;

    public List<GameObject> targets = new List<GameObject>();
    protected LineRenderer lineRend = null;
    [SerializeField] protected Light spotLight = null;
    public PatrolSpawn patrolSpawn = null;
    public int nbPatrollers = 1;

	public Puzzle puzzle;

	// Use this for initialization
	void Start ()
    {
		if (puzzle)
			puzzle.isSpotted = false;
        // Visualize angle (Debug)
        if (debug)
        {
            lineRend = gameObject.AddComponent<LineRenderer>();
            lineRend.material = new Material(Shader.Find("Particles/Additive"));
            lineRend.widthMultiplier = 0.01f;
            lineRend.numPositions = 3;
            lineRend.startColor = lineRend.endColor = Color.red;

            SphereCollider sphereColl = gameObject.AddComponent<SphereCollider>();
            sphereColl.center = Vector3.zero;
            sphereColl.radius = maxRadius;
            sphereColl.isTrigger = true;

            InvokeRepeating("Log", 0f, logInterval);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetVisiblePlayers();

        if (spotLight != null)
            spotLight.GetComponent<Light>().color = (targets.Count > 0) ? Color.red : Color.green;

        if (patrolSpawn != null)
            for (int i = 0; i < targets.Count; ++i)
                patrolSpawn.alert(targets[i], nbPatrollers);

        // Visualize angle (Debug)
            if (debug)
        {
            Vector3 target = (targets.Count > 0) ? targets[0].transform.position : transform.position;
            lineRend.SetPositions(new Vector3[] { transform.forward * maxRadius, transform.position, target });
        }
    }

    protected void GetVisiblePlayers()
    {
        targets.Clear();

        // Get all players within radius
        List<GameObject> playersInRadius = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxRadius);
        for (int i = 0; i < hitColliders.Length; ++i)
            if (hitColliders[i].tag == "Player")
                playersInRadius.Add(hitColliders[i].gameObject);

        // Get all players within angle
		for (int i = 0; i < playersInRadius.Count; ++i)
			if (CalculateAngleWith (playersInRadius [i]) < maxAngle) 
			{
				targets.Add (playersInRadius [i]);
				if (puzzle)
					puzzle.isSpotted = true;
			}
    }

    protected float CalculateAngleWith(GameObject target)
    {
        Vector3 toTarget = target.transform.position - transform.position;
        return Vector3.Angle(toTarget, transform.forward);
    }

    protected void Log()
    {
        if (debug)
            for (int i = 0; i < targets.Count; ++i)
                Debug.Log(targets[i].name + " has been spotted (angle: " + CalculateAngleWith(targets[i]) + "°).");
    }
}
