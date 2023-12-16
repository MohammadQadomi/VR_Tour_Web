using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeanAnimation : MonoBehaviour
{
    [SerializeField] Image image_1;
    [SerializeField] GameObject tagText;

    private bool isMoving = false;

    [ContextMenu("Expand")]
    public void ExpandImage(Image image)
    {
        if (isMoving) return;
        isMoving = true;
        LeanTween.scale(image.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeInOutCubic).setOnComplete(SetIsMoving);

        LeanTween.scale(tagText, Vector3.zero, 1f).setEase(LeanTweenType.easeInOutCubic);
    }

    [ContextMenu("Shrink")]
    public void ShrinkImage(Image image)
    {
        if (isMoving) return;
        isMoving = true;
        LeanTween.scale(image.gameObject, Vector3.zero, 1f).setEase(LeanTweenType.easeInCubic).setOnComplete(SetIsMoving);

        LeanTween.scale(tagText, Vector3.one, 1f).setEase(LeanTweenType.easeInCubic);
    }

    [ContextMenu("Trigger Animation")]
    public void TriggerCardAnimation()
    {
        if (image_1.transform.localScale.x <= 0)
        {
            ExpandImage(image_1);
            SetImageRaycastTarget(tagText, false);
        }
        else
        {
            ShrinkImage(image_1);
            SetImageRaycastTarget(tagText, true);
        }
    }

    void SetIsMoving()
    {
        isMoving = false;
    }

    void SetImageRaycastTarget(GameObject obj, bool value)
    {
        obj.GetComponent<Image>().raycastTarget = value;
    }
}
