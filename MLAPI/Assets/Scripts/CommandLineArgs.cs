using System;
using UnityEngine;

public class CommandLineArgs : MonoBehaviour
{
    public static bool RunAsServer { get; private set; } = false;
    public static string IpAddress { get; private set; } = "localhost";
    public static int PlayerChildObjects { get; private set; } = 0;
    private static string[] args;

    private void OnEnable()
    {
        args = Environment.GetCommandLineArgs();

        RunAsServer = FindArgValue("-runAsServer") != null;
        IpAddress = FindArgValue("-ipaddress=");
        PlayerChildObjects = Convert.ToInt32(FindArgValue("-children="));
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
