using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public int objectCount;
    public GameObject bulletPrefab;

    private void Start() {
        for (int i = 0; i < objectCount; i++) {
            Instantiate(bulletPrefab, transform);
        }
    }
    public GameObject GetObject() {
        return transform.GetChild(0).gameObject;
    }

    public void SetObject(GameObject obj) {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(90, 0, 0);
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
