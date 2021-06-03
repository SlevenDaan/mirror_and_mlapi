using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine;

namespace Mirror
{
    /// <summary>
    /// Component that will display the clients ping in milliseconds
    /// </summary>
    [DisallowMultipleComponent]
    public class NetworkPingDisplay : NetworkBehaviour
    {
        public bool showPing = true;
        [Tooltip("True shows the round trip time, from origin to destination, then back again. Set to false for time to server only.")]
        public bool showRoundTripTime = true;
        private double rttMultiplier = 1;
        public Vector2 position = new Vector2(200, 0);
        public int fontSize = 24;
        public Color textColor = new Color32(255, 255, 255, 80);
        public string format = "{0}ms";

        GUIStyle style;

        void Awake()
        {
            style = new GUIStyle();
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = fontSize;
            style.normal.textColor = textColor;
        }

        void OnGUI()
        {
            if (!showPing) { return; }
            if (showRoundTripTime) { rttMultiplier = 1; } else { rttMultiplier = 2; }

            string text = string.Format(format, (int)((NetworkManager.Singleton.GetComponent<UNetTransport>().GetCurrentRtt(OwnerClientId) / rttMultiplier)));

            // leave here or create special method to update fontSize and textColor
            style.fontSize = fontSize;
            style.normal.textColor = textColor;

            int width = Screen.width;
            int height = Screen.height;
            Rect rect = new Rect(position.x, position.y, width - 200, height * 2 / 100);

            GUI.Label(rect, text, style);
        }
    }
}
