using System;
using System.Net;
using UnityEngine;

public class CommandLineArgs : MonoBehaviour
{
    public static bool RunAsServer { get; private set; } = false;
    public static string IpAddress { get; private set; } = "localhost";
    public static ushort Port { get; set; } = 7777;
    public static int PlayerChildObjects { get; private set; } = 0;
    private static string[] args;

    private void OnEnable()
    {
        args = Environment.GetCommandLineArgs();

        RunAsServer = FindArgValue("-runAsServer") != null;

        string ipAddress = FindArgValue("-ipaddress=");
        IpAddress = ipAddress == null ? IpAddress : ipAddress;

        string port = FindArgValue("-port=");
        Port = port == null ? Port : Convert.ToUInt16(port);

        PlayerChildObjects = Convert.ToInt32(FindArgValue("-children="));

        Debug.Log($"Run Specs:\n  Server: {RunAsServer}\n  Ip: {IpAddress}\n  Port: {Port}\n  PlayerChildren: {PlayerChildObjects}");
    }

    private static string FindArgValue(string argKey)
    {
        foreach (var arg in args)
        {
            if (arg.StartsWith(argKey))
            {
                return arg.Substring(argKey.Length, arg.Length - argKey.Length);
            }
        }
        return null;
    }
}
