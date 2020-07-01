using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private GameObject _soldierPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Soldier.OnDeath += SoldierDied;
    }

    private void SoldierDied(string tag)
    {
        Debug.Log($"Soldier with tag {tag} died");
        var soldier = Instantiate(_soldierPrefab) as GameObject;
        if (tag == "Red")
        {
            SkinnedMeshRenderer renderer = soldier.GetComponentInChildren<SkinnedMeshRenderer>();
            Debug.Log($"Renderer name: {renderer.name}");
            renderer.material = _materials[0];
            soldier.tag = "Red";
        }        
        // soldier.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        soldier.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
