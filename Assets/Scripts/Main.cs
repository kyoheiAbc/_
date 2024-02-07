using UnityEngine;
public class Main : MonoBehaviour
{
    private Controller controller;
    private PuyoManager puyoManager;
    void Start()
    {
        Application.targetFrameRate = 30;

        this.gameObject.transform.position = new Vector3(0, 0, 0);

        Camera camera = this.gameObject.AddComponent<Camera>();
        camera.backgroundColor = Color.HSVToRGB(0, 0, 0.5f);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.orthographic = true;
        camera.orthographicSize = 16;
        camera.transform.position = new Vector3(4, 7, -1);

        SpriteRenderer spriteRenderer = new GameObject("").AddComponent<SpriteRenderer>();
        spriteRenderer.color = Color.HSVToRGB(2 / 3f, 1f, 1f);
        spriteRenderer.sprite = Resources.Load<Sprite>("Square");
        spriteRenderer.transform.localScale = new Vector3(6, 12, 0);
        spriteRenderer.transform.position = new Vector3(4, 7, 0);

        this.controller = new Controller(this.gameObject.GetComponent<Camera>());
        this.puyoManager = new PuyoManager();
    }

    void Update()
    {
        this.puyoManager.Update();
        this.puyoManager.MovePuyoPuyo(this.controller.Update());
    }

    static public GameObject Instantiate(Vector2 position)
    {
        return Instantiate(Resources.Load<GameObject>("Puyo"), position, Quaternion.identity);
    }
}
