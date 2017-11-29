using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour {
    public GameObject Camera;
    int i=0;
    
	void Start () {
        Camera = GetComponent<GameObject>();
	}
	
	void Update () {
        RaycastHit hit;
        // Correcciones de direccion de spherecast debido a la posicion de la mira y camara
        Vector3 CameraPos = transform.position + new Vector3(0f, 2f, 0f);
        Vector3 Dir = transform.forward + new Vector3 (0f,0f,0f);

        int layerMask = 1<<9; // Layer 9 para Hiders
        
        if (Physics.SphereCast(CameraPos, 3f, Dir, out hit, 20f, layerMask)) // spherecast para localizar cosas
        {
            i++;
            Debug.Log(hit.collider.gameObject.tag + i);
        }
        Debug.DrawRay(CameraPos, Dir*20f, Color.green); // dibujo de raycast como prueba
    }
}
