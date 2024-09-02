using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonIllumination : MonoBehaviour
{
    public float fadeSpeed = 1f; // Vitesse de transition
    private Renderer objectRenderer; // Renderer de l'objet à modifier (par exemple, le Material)

    private float currentOpacity = 0f; // Opacité actuelle

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        // Assurez-vous que l'objet n'est pas visible au début
        SetOpacity(0f);
    }

    void Update()
    {
        // Fait progresser l'opacité de l'objet de 0 à 1
        FadeIn();

        // Simule un retard avant de commencer à diminuer l'opacité
        if (currentOpacity >= 1f)
        {
            // Fait progresser l'opacité de l'objet de 1 à 0
            FadeOut();
        }
    }

    void FadeIn()
    {
        // Incrémente l'opacité actuelle avec une vitesse
        currentOpacity = Mathf.Clamp01(currentOpacity + fadeSpeed * Time.deltaTime);

        // Applique l'opacité actuelle à l'objet
        SetOpacity(currentOpacity);
    }

    void FadeOut()
    {
        // Décrémente l'opacité actuelle avec une vitesse
        currentOpacity = Mathf.Clamp01(currentOpacity - fadeSpeed * Time.deltaTime);

        // Applique l'opacité actuelle à l'objet
        SetOpacity(currentOpacity);
    }

    void SetOpacity(float opacity)
    {
        // Modifie l'opacité de l'objet en mettant à jour le canal alpha du matériau ou du sprite
        Color objectColor = objectRenderer.material.color;
        objectColor.a = opacity;
        objectRenderer.material.color = objectColor;
    }
}
