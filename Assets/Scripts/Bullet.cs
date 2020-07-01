using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
	public float speed = 15.0f;
	public int damage = 1;

	void Update() {
		transform.Translate(0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		Soldier soldier = other.GetComponent<Soldier>();
		if (soldier != null) {
			soldier.Hurt(damage);
		}
		Destroy(this.gameObject);
	}
}
