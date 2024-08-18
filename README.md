# FantaziaDesign

This is the basic class libraries to abstract some framework-independency common code for `FantaziaDesign`. The `TargetFrameworks` for this project are `net7.0` and `net8.0`.

`FantaziaDesign.Core` defines severial interfaces and basic structs that are agnostic to each desktop UI / web framework and enchanced the usability. For example, the interface `IDeepCopyable<T>` is the extension of the interface `ICloneable`, which is explicitly indicated the deep- copyablity of the object.

`FantaziaDesign.Events` is the enhancement to `delegate` and `event` for solving the strong reference issue by using `WeakDelegate` and `WeakEvent`.

`FantaziaDesign.Interop` collects some useful P/Invoke Api from win32 native libraries such as `dwmapi.dll`, `user32.dll` and `gdi32.dll`.