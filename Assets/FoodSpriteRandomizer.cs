
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FoodSpriteRandomizer : MonoBehaviour
{
    [SerializeField] private Sprite[] foodSprites;

    private void Awake()
    {
        if (foodSprites == null || foodSprites.Length == 0)
            return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = foodSprites[Random.Range(0, foodSprites.Length)];
    }
}
