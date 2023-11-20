using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour{
    [SerializeField] private new Camera camera;

    private void OnCollisionStay(Collision collision){
        if (collision.gameObject.CompareTag("Wall")){
            Vector3 position = camera.transform.position;
            Quaternion rotation = camera.transform.rotation;
            camera.transform.position = transform.position;
            camera.transform.rotation = transform.rotation;
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
