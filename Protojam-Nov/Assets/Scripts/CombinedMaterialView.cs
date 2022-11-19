using UnityEngine;
using UnityEngine.UI;

public class CombinedMaterialView : MonoBehaviour
{
    [SerializeField] private Image[] orderRecipeImages;
    
    private static Sprite[] MaterialSprites => InGameCanvas.Instance.CookingView.materialSprites;

    public void Set(MaterialType[] recipe)
    {
        for (int i = 0; i < orderRecipeImages.Length; i++)
        {
            if (i < recipe.Length)
            {
                orderRecipeImages[i].gameObject.SetActive(true);
                orderRecipeImages[i].sprite = MaterialSprites[(int)recipe[i]];
            }
            else
            {
                orderRecipeImages[i].gameObject.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        Set(new MaterialType[] { });
    }
}
