using UnityEngine;

public class OreItem : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float destroyY = -10f;

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < destroyY)
            Destroy(gameObject);
    }

    public void SetSprite(Sprite sprite)
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.sprite = sprite;
    }
}