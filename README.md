# Material 3 theme for Avalonia
![Placeholder](https://raw.githubusercontent.com/gor1kartem/Material3Avalonia/refs/heads/main/screenshot.png)

Setup
---

Add material theme in App.axaml. Make sure you have another theme as a fallback because material theme does not support all controls
```diff
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="CrossDL.App"
             xmlns:local="using:CrossDL"
             RequestedThemeVariant="Light"
+             xmlns:material="clr-namespace:MaterialTheme;assembly=MaterialTheme">
    <Application.Styles>
        <FluentTheme /> // Make sure you have fallback theme
+        <material:MaterialTheme></material:MaterialTheme>
        
    </Application.Styles>
</Application>
```
