
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;
    [SerializeField] CameraFollow cam;
    [SerializeField] Canvas canvas;
    [SerializeField] public GameObject win;
    // Start is called before the first frame update
    public void SpawnMap(GameObject map)
    {
        Instantiate(map);
        cam.FindPlayer();
        canvas.gameObject.SetActive(false);
    }
    public void Victory()
    {
        win.SetActive(true);
    }
}
