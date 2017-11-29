using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Catch : MonoBehaviour {
    public GameObject Camera;
    public Image CatchImage;
    public float MaxCatchRatio = 100f, CatchRatio = 0f;
    float TimeCatching = 0f; // Guarda el tiempo en el que se empezo a capturar a un jugador
    bool catching=false; // indica si el seeker esta capturando a un jugador
    int layerMask = 1 << 9; // Layer 9 para Hiders
    void Start () {
        Camera = GetComponent<GameObject>();
        
	}
	
	void Update () {
        RaycastHit hit;

        // Correcciones de direccion de spherecast debido a la posicion de la mira y camara
        Vector3 CameraPos = transform.position + new Vector3(0f, 2f, 0f);
        Vector3 Dir = transform.forward + new Vector3 (0f,0f,0f);

        

        if (!catching)
        {
            TimeCatching = Time.time;

        }

        // spherecast para localizar cosas
        if (Physics.SphereCast(CameraPos, 3f, Dir, out hit, 20f, layerMask)) 
        {
          
            catching = true;
           // Debug.Log(Time.deltaTime - TimeCatching);
            if ( Time.time - TimeCatching >= 1f)
            {
                Debug.Log("ha pasado un segundo");
                Catching();
                TimeCatching = Time.time;
            }

            if (CatchRatio>=100f)
            {
                Destroy(hit.transform.gameObject); // destruyo el objeto al que capture el seeker (CAMBIARA)
                SetCatchRatio(0);
                CatchRatio = 0;
                catching = false;
            }

           
        }
        else // reseteamos los valores de Catching
        {
            SetCatchRatio(0);
            CatchRatio = 0;
            catching = false;
        }
        Debug.DrawRay(CameraPos, Dir*20f, Color.green); // dibujo de raycast como prueba
    }



    void SetCatchRatio (float Catch) // le asigno un valor a el llenado de la barra 
    {
        CatchImage.fillAmount = Mathf.Lerp(CatchImage.fillAmount, Catch, Time.deltaTime*1000);
    }

    void Catching() // Realiza los calculos del valor del catch ratio
    {
        CatchRatio += 20f;
        float ratio = CatchRatio / MaxCatchRatio ;
        SetCatchRatio(ratio);
    }
}
