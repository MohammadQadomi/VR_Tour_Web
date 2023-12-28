using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleContent : MonoBehaviour
{
    [SerializeField] Image contentImage;
    int id;
    public int GetID() => id;
    public void SetImage(Texture2D texture, int id)
    {
        this.id = id;
        contentImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero, 100, 0, SpriteMeshType.FullRect, Vector4.zero, false);
    }
}
