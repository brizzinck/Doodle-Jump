using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private GameOver _gameOver;
    [SerializeField] private Camera _mainCamera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Environments environments))
        {
            if (environments.GetComponent<Enemies>() != null)
            {
                environments.GetComponent<SpriteRenderer>().enabled = false;
                environments.GetComponent<Collider2D>().enabled = false;
                Destroy(environments.gameObject, 0.7f);
            }
            else Destroy(environments.gameObject);
        }

        else if (collision.TryGetComponent(out Player player))
        {
            StartCoroutine(_gameOver.Fall());
        }       
    }
}
