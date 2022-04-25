using BepInEx.Configuration;

// Variable
ConfigEntry<float> bind;

var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "NAME.cfg"), true);
bind = customFile.Bind("CATEGORY", "NAME",1f /* Default */, "DESCRIPTION");
