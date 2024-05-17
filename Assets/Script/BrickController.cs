
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] GameObject brickAva;
    bool isBlanked;
    private void Start()
    {
        CheckBlanked();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && isBlanked)
        {
            if (this.gameObject.CompareTag("BlankBrick"))
            {
                brickAva.SetActive(true);
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            brickAva.SetActive(false);
        }

    }
    void CheckBlanked()
    {
        if (this.gameObject.CompareTag("BlankBrick"))
        {
            isBlanked = true;
            brickAva.SetActive(false);
        }
    }
}
