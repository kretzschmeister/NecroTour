using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

	public float aliveTime;
// dit script is vastgemaakt aan de bullet, zodat hij verdwijnt na een bepaalde tijd.
	void Awake () {
        Destroy(this.gameObject, aliveTime);
    }

	void FixedUpdate () {

    }
}

