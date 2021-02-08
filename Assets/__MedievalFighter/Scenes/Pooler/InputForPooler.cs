using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputForPooler : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootStrength;

    public ObjectPooler pooler;

    public float interval;
    private float tmpInterval;
    void Start() {
        tmpInterval = interval;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButton(0)) {
            tmpInterval -= Time.deltaTime;
            if (tmpInterval<=0) {
                Shoot();
                tmpInterval = interval;
            }
        }
    }

    private void Shoot() {
        //GameObject bullet = Instantiate(bulletPrefab);
        // Rigidbody rb = bullet.GetComponent<Rigidbody>();
        //  rb.AddForce(Vector3.forward * shootStrength, ForceMode.Impulse);
        GameObject bullet = pooler.GetObject();
        bullet.transform.SetParent(null);
        bullet.gameObject.SetActive(true);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * shootStrength, ForceMode.Impulse);
        Debug.Log("GaisroleTyvia");
    }
}
