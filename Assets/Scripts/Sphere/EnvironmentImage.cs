using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentImage : MonoBehaviour
{
    [SerializeField] Renderer sphereRenderer;

    [SerializeField] Material skyboxMaterial;

    [ContextMenu("Lean Transition")]
    public void LeanTransition(Transform transform)
    {
        //ResetTransition();
        //LeanTween.move(gameObject, -Camera.main.transform.forward, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(ImageTransition);
        //LeanTween.move(gameObject, -Vector3.MoveTowards(this.transform.position, transform.position, 3), 0.5f).setEase(LeanTweenType.easeOutCubic).setOnComplete(ImageTransition);
        ImageTransition();
    }

    public void ImageTransition()
    {
        ResetTransition();
        LeanTween.color(gameObject, Color.clear, 0.5f);
    }


    public void ResetTransition()
    {
        transform.position = Vector3.zero;
        sphereRenderer.material.SetColor("_Color", Color.black);
        //sphereRenderer.material.SetTexture("_MainTex", skyboxMaterial.mainTexture);
        //sphereRenderer.material.SetTexture("_EmissionMap", skyboxMaterial.mainTexture);
    }
}
