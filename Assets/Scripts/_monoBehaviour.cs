public class _monoBehaviour : UnityEngine.MonoBehaviour
{
    private Main main;
    void Start()
    {
        UnityEngine.Application.targetFrameRate = 60;
        this.main = new Main();
    }
    void Update()
    {
        this.main.Update();
    }
}