using UnityEngine;
using System.Collections;

/// <summary>
/// Class used for controlling basic enemy behavior
/// </summary>
public class EnemyMovement : MonoBehaviour {

    /// <summary>
    /// Used for navigating from right to left
    /// </summary>
    public NavMeshAgent nav;

    /// <summary>
    /// The location that the enemies march towards
    /// </summary>
    public Vector3 dest;

    /// <summary>
    /// the location enemies must march to whe
    /// </summary>
    public GameObject despawnPoint;

	// Use this for initialization per life cycle
	void  Awake() {
        nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        /*if (this.transform.position == despawnPoint.transform.position)
            Destroy(this.gameObject);
        else if (this.transform.position == nav.destination)
        {
            dest = despawnPoint.transform.position;
            Debug.Log("Switched");
        }*/

        nav.destination = dest;

        if (this.transform.position.x < (dest.x + Mathf.Epsilon) && this.transform.position.x > (dest.x - Mathf.Epsilon))
        {
            Debug.Log("Passed");
            //dest = despawnPoint.transform.position;
            //No longerbeing destroyedDestroy(this.gameObject);
        }
    }
}
