using UnityEngine;
public class _monoBehaviour : MonoBehaviour
{
    private Main main;
    void Start()
    {
        this.main = new Main();
    }
    void Update()
    {
        this.main.Update();
    }
}