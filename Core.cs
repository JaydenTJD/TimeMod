using MelonLoader;
using UnityEngine;
using UniverseLib;
using UniverseLib.UI;
using UniverseLib.UI.Models;
using UniverseLib.UI.Panels;
using UniverseLib.Utility;

[assembly: MelonInfo(typeof(TimeMod.Core), "TimeMod", "2.0.0", "Jayden", null)]
[assembly: MelonGame(developer:"Notional Games")]

namespace TimeMod
{
    public class Core : MelonMod
    {
        public static UIBase UI { get; private set; }
        public static InputPanel SpeedField { get; private set; }
        public override void OnInitializeMelon()
        {
            Universe.Init(1f, OnUniverseInit, LogHandler, new UniverseLib.Config.UniverseLibConfig());
        }

        private void OnUniverseInit()
        {
            UI = UniversalUI.RegisterUI("com.jayden.timemod", UpdateUIBehavior);
            SpeedField = new(UI);
            SpeedField.SetActive(true);
        }

        private void UpdateUIBehavior() { }

        public override void OnUpdate()
        {
            if (UnityEngine.InputSystem.Keyboard.current.f5Key.wasPressedThisFrame)
            {
                SpeedField.SetActive(!SpeedField.Enabled);
            }
        }

        private void LogHandler(string message, LogType logtype)
        {
            LoggerInstance.Msg($"[{logtype}] {message}");
        }
    }

    public class InputPanel(UIBase owner) : PanelBase(owner)
    {
        private InputFieldRef textInput;

        public override string Name => "Time Mod";
        public override int MinWidth => 150;
        public override int MinHeight => 80;
        public override Vector2 DefaultAnchorMin => new(0, 1);
        public override Vector2 DefaultAnchorMax => new(0, 1);
        public override bool CanDragAndResize => true;

        protected override void ConstructPanelContent()
        {
            textInput = UIFactory.CreateInputField(ContentRoot, "SpeedInputField", Time.timeScale.ToString());
            UIFactory.SetLayoutElement(textInput.UIRoot, minWidth: 130, minHeight: 30);

            textInput.Component.GetOnEndEdit().AddListener(val =>
            {
                if (float.TryParse(val, out float f))
                {
                    Time.timeScale = f;
                }
            });
        }
    }
}