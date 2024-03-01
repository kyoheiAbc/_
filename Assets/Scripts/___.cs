using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// public class Configuration_
// {
//     public static int THRESHOLD = 3;
//     public static int COLOR = 4;
//     public static int FIRE = 4;
//     public static int DOWN = 3;
//     public static int COMBO = 90;
//     public static int BOT_HEALTH = 64;
//     public static int BOT_ATTACK = 100;
//     public static int BOT_SPEED = 3 * 60;

//     private class Json
//     {
//         public int threshold;
//         public int color;
//         public int fire;
//         public int down;
//         public int combo;
//         public int botHealth;
//         public int botAttack;
//         public int botSpeed;
//     }

// public Configuration()
// {
//     if (File.Exists(Application.persistentDataPath + "/json.json"))
//     {
//         Json json = JsonUtility.FromJson<Json>(File.ReadAllText(Application.persistentDataPath + "/json.json"));
//         Configuration.THRESHOLD = json.threshold;
//         Configuration.COLOR = json.color;
//         Configuration.FIRE = json.fire;
//         Configuration.DOWN = json.down;
//         Configuration.COMBO = json.combo;
//         Configuration.BOT_HEALTH = json.botHealth;
//         Configuration.BOT_ATTACK = json.botAttack;
//         Configuration.BOT_SPEED = json.botSpeed;
//     }
//     else
//     {
//         Json json = new Json();
//         json.threshold = Configuration.THRESHOLD;
//         json.color = Configuration.COLOR;
//         json.fire = Configuration.FIRE;
//         json.down = Configuration.DOWN;
//         json.combo = Configuration.COMBO;
//         json.botHealth = Configuration.BOT_HEALTH;
//         json.botAttack = Configuration.BOT_ATTACK;
//         json.botSpeed = Configuration.BOT_SPEED;
//         File.WriteAllText(Application.persistentDataPath + "/json.json", JsonUtility.ToJson(json));
//     }
// }
// }
