using UnityEngine;
using System.Collections;

/// <summary>
/// Used to despawn enemies and put them back into the cycle
/// </summary>
public class EnemyDespawn : MonoBehaviour {

	void OnEnable()
    {
        Invoke("Destroy", 500f);
    }

    /// <summary>
    /// Disables the enemy
    /// </summary>
    public void Destroy()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Prevents double bisabling and issues created therein
    /// </summary>
    void OnDisable()
    {
        CancelInvoke();
    }
}
