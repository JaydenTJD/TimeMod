using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(TimeMod.Core), "TimeMod", "1.0.0", "jayde", null)]
[assembly: MelonGame(developer:"Notional Games")]

namespace TimeMod
{
    public class Core : MelonMod
    {
        private bool frozen = false;
        private float prevtimescale = 1;
        private Rect window = new(10, 10, 150, 200);
        readonly List<Rect> btnrects = [new(10, 20, 130, 20), new(10, 50, 130, 20), new(10, 80, 130, 20), new(10, 110, 130, 20), new(10, 140, 130, 20), new(10, 170, 130, 20)];
        Vector2 mpos;
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized Time Mod.");
        }

        public override void OnGUI()
        {
            window = GUI.Window(0, window, (GUI.WindowFunction)DrawWindow, "Time Mod Menu");

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                mpos = Event.current.mousePosition;

                if (GetAbsolute(btnrects[0]).Contains(mpos))
                {
                    frozen = !frozen;
                    if (frozen)
                    {
                        prevtimescale = Time.timeScale;
                        Time.timeScale = 0;
                    } else
                    {
                        Time.timeScale = prevtimescale;
                    }
                    Event.current.Use();
                } else if (GetAbsolute(btnrects[1]).Contains(mpos))
                {
                    frozen = false;
                    Time.timeScale = 0.25F;
                    Event.current.Use();
                } else if (GetAbsolute(btnrects[2]).Contains(mpos))
                {
                    frozen = false;
                    Time.timeScale = 0.5F;
                    Event.current.Use();
                } else if (GetAbsolute(btnrects[3]).Contains(mpos))
                {
                    frozen = false;
                    Time.timeScale = 1;
                    Event.current.Use();
                } else if (GetAbsolute(btnrects[4]).Contains(mpos))
                {
                    frozen = false;
                    Time.timeScale = 2;
                    Event.current.Use();
                } else if (GetAbsolute(btnrects[5]).Contains(mpos))
                {
                    frozen = false;
                    Time.timeScale = 5;
                    Event.current.Use();
                }
            }
        }

        private void DrawWindow(int id) {
            GUI.DragWindow(new Rect(0, 0, 150, 20));

            GUI.Box(btnrects[0], frozen ? "Unpause" : "Pause");
            GUI.Box(btnrects[1], Time.timeScale == 0.25 ? "Current Speed" : "0.25x Speed");
            GUI.Box(btnrects[2], Time.timeScale == 0.5 ? "Current Speed" : "0.5x Speed");
            GUI.Box(btnrects[3], Time.timeScale == 1 ? "Current Speed" : "1x Speed");
            GUI.Box(btnrects[4], Time.timeScale == 2 ? "Current Speed" : "2x Speed");
            GUI.Box(btnrects[5], Time.timeScale == 5 ? "Current Speed" : "5x Speed");
        }

        private Rect GetAbsolute(Rect local)
        {
            return new Rect(window.x + local.x, window.y + local.y, local.width, local.height);
        }
    }
}