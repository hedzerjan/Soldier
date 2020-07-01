using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private GameObject _head;
    float x = 0.2f;
    float y = 1;
    float z = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        _head = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Neck/Bip001 Head").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Ray ray = new Ray(transform.position, transform.forward);
        // RaycastHit hit;
        // if (Physics.SphereCast(ray, 0.75f, out hit))
        // {
        //     GameObject hitObject = hit.transform.gameObject;
        //     // if (hitObject.GetComponent<PlayerCharacter>())
        //     // {
        //     //     if (_fireball == null)
        //     //     {
        //     //         _fireball = Instantiate(fireballPrefab) as GameObject;
        //     //         _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        //     //         _fireball.transform.rotation = transform.rotation;
        //     //     }
        //     // }
        //     // else if (hit.distance < obstacleRange)
        //     // {
        //     //     float angle = Random.Range(-110, 110);
        //     //     transform.Rotate(0, angle, 0);
        //     // }
        // }
        // var dir = Random.rotationUniform;
        // var dirEuler = dir.eulerAngles;
        // dirEuler *= 0.25f;
        // var offset = new Vector3(0f, 0f, 45f);
        // dirEuler -= offset;
        // var dirWorld = _head.transform.TransformDirection(dirEuler);
    }

    public GameObject ScoutScan()
    {
        var scanX = Random.Range(-x, x);
        var scanZ = Random.Range(-z, z);
        Ray ray = new Ray(_head.transform.position, _head.transform.TransformDirection(new Vector3(scanX, y, scanZ)));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, .1f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var hitObject = hit.collider.transform.gameObject;
            if (hitObject.tag != "Untagged" && hitObject.tag != transform.gameObject.tag)
            {
                // Debug.Log($"Enemy: {hitObject.tag} (I am {transform.gameObject.tag})");
                return hitObject;
            }
        }
        return null;
    }
    public GameObject FireScan(GameObject target)
    {
        if (target != null)
        {
            var rayDirection = target.transform.position - _head.transform.position;
            rayDirection.y = 0;
            Ray ray = new Ray(_head.transform.position, rayDirection);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, .1f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.collider.transform.gameObject;
                if (hitObject.tag != "Untagged" && hitObject.tag != transform.gameObject.tag)
                {
                    // Debug.Log($"Enemy: {hitObject.tag} (I am {transform.gameObject.tag})");
                    return hitObject;
                }
            }
        }
        return null;
    }
    private void DrawDebugLines()
    {
        Debug.DrawRay(_head.transform.position, _head.transform.TransformDirection(new Vector3(-1f, 0f, 0f)) * 10, Color.red);
        Debug.DrawRay(_head.transform.position, _head.transform.TransformDirection(new Vector3(0f, 1f, 0f)) * 10, Color.green);
        Debug.DrawRay(_head.transform.position, _head.transform.TransformDirection(new Vector3(0f, 0f, 1f)) * 10, Color.blue);
        Debug.DrawRay(_head.transform.position, _head.transform.TransformDirection(new Vector3(-x, y, z)), Color.white);
        Debug.DrawRay(_head.transform.position, _head.transform.TransformDirection(new Vector3(x, y, z)), Color.white);
        Debug.DrawRay(_head.transform.position, _head.transform.TransformDirection(new Vector3(-x, y, -z)), Color.white);
        Debug.DrawRay(_head.transform.position, _head.transform.TransformDirection(new Vector3(x, y, -z)), Color.white);
    }
}
