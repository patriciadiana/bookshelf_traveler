using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material objectMaterial;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectMaterial = spriteRenderer.material;

        DisableOutline();
    }

    public void EnableOutline()
    {
        objectMaterial.SetFloat("_Thickness", 0.03f);
    }

    public void DisableOutline()
    {
        objectMaterial.SetFloat("_Thickness", 0f);
    }
}
