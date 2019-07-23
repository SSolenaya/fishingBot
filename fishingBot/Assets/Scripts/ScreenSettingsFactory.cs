using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Scripts;
using UnityEngine;

public static class ScreenSettingsFactory {
    private static Dictionary<string, Type> settingsByName;
    private static bool Initialized = settingsByName != null;

    private static void InitializeFactory() {
        if (Initialized)
            return;
        var settings = Assembly.GetAssembly(typeof(ScreenSettings)).GetTypes().Where(myType =>
            myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(ScreenSettings)));
        settingsByName = new Dictionary<string, Type>();
        foreach (var setting in settings) {
            var tempSettings = Activator.CreateInstance(setting) as ScreenSettings;
            settingsByName.Add(tempSettings.Name, setting);
        }
        }

    public static ScreenSettings GetSettings(string settingsType) {
        InitializeFactory();
        if (settingsByName.ContainsKey(settingsType)) {
            Type type = settingsByName[settingsType];
            var set = Activator.CreateInstance(type) as ScreenSettings;
            return set;
        }
        return null;
    }

    internal static IEnumerable<string> GetSettingsNames() {
        InitializeFactory();
        return settingsByName.Keys;
    }
}

    
