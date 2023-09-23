using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambiaEscena : MonoBehaviour
{
    public void nuevaEscena_menu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    
    public void nuevaEscena_1()
    {
        SceneManager.LoadScene("Escena1", LoadSceneMode.Single);
    }
    
    public void nuevaEscena_2()
    {
        SceneManager.LoadScene("Escena2", LoadSceneMode.Single);
    }
    
}
