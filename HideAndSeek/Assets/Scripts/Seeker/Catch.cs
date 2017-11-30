using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Catch : MonoBehaviour {

    //DECLARACIONES
    public Image CatchImage, mira;// imagenes UI
    public float MaxCatchRatio = 100f, CatchRatio = 0f; // Valores del filling para la UI de captura
    float TimeCatching = 0f; // Guarda el tiempo en el que se empezo a capturar a un jugador
    bool catching = false; // indica si el seeker esta capturando a un jugador
    int layerMask = 1 << 9; // Layer 9 para Hiders

    //MAIN
    void Start() {
    }

    void Update() {
        RaycastHit hit;

        // Correcciones de direccion de spherecast debido a la posicion de la mira y camara
        Vector3 CameraPos = transform.position + new Vector3(0f, 0f, 0f);
        Vector3 Dir = transform.forward + new Vector3(0f, 0f, 0f);

        if (!catching)
        {
            TimeCatching = Time.time;// Guardamos el tiempo hasta que el seeker vea a un hider
        }

        // spherecast para localizar cosas
        if (Physics.SphereCast(CameraPos, 0.8f, Dir, out hit, 20f, layerMask)) // si esta "viendo" a un hider
        {
            catching = true;//Indica que el seeker esta capturando a un hider
            mira.GetComponent<Image>().color = Color.green; // Cambiar tonalidad de la mira para indicar que esta capturando a un hider
            if (Time.time - TimeCatching >= 1f)
            {
                Catching(); // Realiza los calculos del valor del catch ratio
                TimeCatching = Time.time; // Reseteamos el tiempo en el que se empieza a capturar a un hider
            }

            if (CatchRatio >= 100f)
            {
                Destroy(hit.transform.gameObject); // Destruimos el objeto al que capture el seeker (POR CAMBIAR, EN VEZ DE DESTRUIR OTRA COSA)
                ResetCatching();//reseteamos los valores cuando no ve a un hider
            }


        }
        else // si ya no esta viendo a un player
        {
            ResetCatching();//reseteamos los valores cuando no ve a un player
        }

        Debug.DrawRay(CameraPos, Dir * 20f, Color.green); // dibujo de raycast como prueba
    }





    //SUBRUTINAS

    void SetCatchRatio(float Catch) // le asigno un valor a el llenado de la barra 
    {
        CatchImage.fillAmount = Mathf.Lerp(CatchImage.fillAmount, Catch, Time.deltaTime * 1000); // INTENTO de hacer smooth el llenado del UI de captura
    }

    private void ResetCatching()
    {
        mira.GetComponent<Image>().color = Color.white; // Retomamos la coloracion normal de la mira
        SetCatchRatio(0);// Colocamos el fill bar del UI de captura en 0
        CatchRatio = 0; // Reseteamos la variable local del valor de captura a 0
        catching = false; // indicamos que ya no se esta capturando a un hider
    }
    
    void Catching() // Realiza los calculos del valor del catch ratio
    {
        CatchRatio += 20f; // aumentamos el llenado de captura en una cantidad, en este caso 1/5
        float ratio = CatchRatio / MaxCatchRatio ; // calculamos el ratio del filling
        SetCatchRatio(ratio);// asignamos un valor a el UI de captura
    }
}
