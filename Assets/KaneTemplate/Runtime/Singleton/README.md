# ScriptableSingleton
Implement from https://hextantstudios.com/unity-singletons/

## Requirements
- Addressable package
## Usage
- Create a class that inherits from `ScriptableSingleton<T>` where `T` is the class itself.
- Create a instance and mark it as `Addressable` in UnityEditor with key is type name of the class.
- Example: 
  - When you create a singleton class `MySingleton` that inherits from `ScriptableSingleton<MySingleton>`.
  - You should create a instance of `MySingleton` and mark it as `Addressable` in UnityEditor with key is `MySingleton`.
## Issues
- The assets in addressable often missing on build. You can fix it by `Window > Asset Management > Addressables > Groups > Build > Clean Build > All` and `Window > Asset Management > Addressables > Groups > Build > New Build > Default Build Script` and then build again.