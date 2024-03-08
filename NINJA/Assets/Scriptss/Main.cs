using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    Controller controller;
    int frame; public int getFrame() { return this.frame; }
    static public Main instance;
    List<Ninja> list;
    Ninja player; public Ninja getPlayer() { return this.player; }
    int stage;
    void Awake()
    {
        this.gameObject.transform.position = new Vector3(0, 8, -8 * Mathf.Sqrt(3));
        this.gameObject.transform.eulerAngles = new Vector3(30, 0, 0);

        Camera camera = this.gameObject.AddComponent<Camera>();
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.orthographic = true;
        camera.backgroundColor = Color.HSVToRGB(120 / 360f, 0.3f, 0.5f);

        this.gameObject.AddComponent<Light>().type = LightType.Directional;

        this.controller = new Controller();
        this.list = new List<Ninja>();

        Main.instance = this;

        Application.targetFrameRate = 30;
    }

    void Start()
    {
        this.reset();
    }

    private void reset()
    {
        this.frame = 0;

        for (int i = 0; i < this.list.Count; i++) Destroy(this.list[i].renderer.getGameObject());
        this.list.Clear();

        this.player = null;

        this.stage = 0;

        for (int i = 0; i < 4; i++)
        {
            Ninja n = new Ninja(i);
            n.setPos(new Vector3(2 * i, 0, 0));
            n.setRot(Quaternion.identity * Quaternion.AngleAxis(180, new Vector3(0, 1, 0)));
            n.setAi(null);
            this.list.Add(n);
        }
    }

    void Update()
    {
        this.frame++;
        if (this.frame == 30 * 60) this.frame = 0;

        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i].getHp() < -60)
            {
                Destroy(this.list[i].renderer.getGameObject());
                this.list.RemoveAt(i);
                i--;
                continue;
            }
            this.list[i].update();
        }

        control();
        void control()
        {
            this.controller.update();

            if (this.player == null)
            {
                Vector3 gtpb = this.transform.GetComponent<Camera>().ScreenToWorldPoint(this.controller.getTouchPhaseBegan());

                if (gtpb == Vector3.zero) return;
                for (int i = 0; i < this.list.Count; i++)
                {
                    if (Main.inSphere(gtpb, gtpb + Main.forward(Quaternion.Euler(30, 0, 0)).normalized * 256, this.list[i].getPos() + Vector3.up, 0.499f))
                    {
                        this.player = this.list[i];
                        for (int i_ = 0; i_ < this.list.Count; i_++)
                        {
                            if (this.player == this.list[i_]) continue;
                            Destroy(this.list[i_].renderer.getGameObject());
                            this.list.RemoveAt(i_);
                            i_--;
                        }
                        break;
                    }
                }
                return;
            }

            Vector3 s = this.controller.getStick().normalized;
            this.player.mv(s * 0.1f);
            int b = this.controller.getButton();
            if (b == 1) this.player.attack.exe();
            if (b == 4) this.player.jump(this.controller.getDeltaPosition().normalized);
        }

        for (int i = 0; i < this.list.Count; i++)
        {
            this.list[i].renderer.update();
        }

        next();
        void next()
        {
            if (this.player == null) return;
            if (this.player.getHp() < -60 || this.list.Count == 1) this.stage++;
            if (this.stage % 100 < 30) return;

            if (this.player.getHp() < -60) this.reset();
            else
            {
                this.stage = 100 * (this.stage / 100) + 100;
                for (int i = 0; i < Mathf.Pow(2, this.stage / 100 - 1); i++) this.list.Add(new Ninja(Random.Range(0, 4)));
            }
        }
    }


    public List<Ninja> getList(Ninja n, float d, float a)
    {
        List<Ninja> ret = new List<Ninja>();
        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i] == n) continue;
            if ((this.list[i].getPos() - n.getPos()).sqrMagnitude > d) continue;
            if (Vector3.Angle(Main.forward(n.getRot()), this.list[i].getPos() - n.getPos()) > a) continue;
            ret.Add(this.list[i]);
        }
        return ret;
    }

    public List<Ninja> getList(Vector3 p, Quaternion r, Vector3 s)
    {
        List<Ninja> ret = new List<Ninja>();
        for (int i = 0; i < this.list.Count; i++)
        {
            if (!Main.inCube(this.list[i].getPos(), p, r, s)) continue;
            ret.Add(this.list[i]);
        }
        return ret;
    }

    private static bool inCube(Vector3 p_, Vector3 p, Quaternion r, Vector3 s)
    {
        p_ = Quaternion.Inverse(r) * (p_ - p);
        p_.Scale(new Vector3(1 / s.x, 1 / s.y, 1 / s.z));
        return (Mathf.Abs(p_.x) <= 0.5f && Mathf.Abs(p_.y) <= 0.5f && Mathf.Abs(p_.z) <= 0.5f);
    }

    public Ninja nearestNinja(Ninja n)
    {
        Ninja ret = n;
        float min = float.MaxValue;
        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i] == n) continue;
            if ((this.list[i].getPos() - n.getPos()).sqrMagnitude < min)
            {
                min = (this.list[i].getPos() - n.getPos()).sqrMagnitude;
                ret = this.list[i];
            }
        }
        return ret;
    }
    private static bool inSphere(Vector3 lineA, Vector3 lineB, Vector3 p, float r)
    {
        return (nearestPoint(lineA, lineB, p) - p).sqrMagnitude < r * r;
    }

    private static Vector3 nearestPoint(Vector3 lineA, Vector3 lineB, Vector3 p)
    {
        Vector3 v = lineB - lineA;
        return lineA + v * Mathf.Clamp01(Vector3.Dot(p - lineA, v) / v.sqrMagnitude);
    }


    static public Vector3 forward(Quaternion q) { return (q * Vector3.forward).normalized; }
}