using UnityEngine;
using UnityEngine.UI;

public class ModelCycler : MonoBehaviour
{
    public GameObject[] models; // Arrastrar los modelos a este arreglo desde UI de unity
    private int currentIndex = 0;

    public Button nextButton;
    public Button prevButton;

    private void Start()
    {

        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(i == currentIndex);
        }

        nextButton.onClick.AddListener(ShowNextModel);
        prevButton.onClick.AddListener(ShowPreviousModel);
    }

    public void ShowNextModel()
    {
        // quite modelo actual
        models[currentIndex].SetActive(false);

        // suma indice
        currentIndex = (currentIndex + 1) % models.Length;

        // Activa modelo del indice actual
        models[currentIndex].SetActive(true);
    }

    public void ShowPreviousModel()
    {
        models[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex < 0) currentIndex = models.Length - 1;
        models[currentIndex].SetActive(true);
    }
}