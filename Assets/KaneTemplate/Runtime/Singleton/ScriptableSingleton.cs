using KaneTemplate.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;

// Implement from https://hextantstudios.com/unity-singletons/
namespace KaneTemplate.Singleton
{
    public class ScriptableSingleton<T> : ScriptableObject
        where T : ScriptableSingleton<T>
    {
        protected static string addressableKey;

        public static T Instance => instance != null ? instance : Initialize();
        static T instance;

        // Finds or creates the singleton instance and stores it in _instance. This can
        // be called from a derived type to ensure creation of the singleton using the
        // [RuntimeInitializeOnLoadMethod] attribute on a static method.
        protected static T Initialize()
        {
            // Prevent runtime instances from being created outside of Play Mode or
            // re-created during OnDestroy() handlers when exiting Play Mode.
            // if (!Application.isPlaying) return null;
            if (instance != null) return instance;

            if (string.IsNullOrEmpty(addressableKey))
            {
                addressableKey = typeof(T).Name;
            }

            var isExist = AddressableUtility.IsAddressableKeyExists(addressableKey);
            if (isExist)
            {
                var instanceOpHandle = Addressables.LoadAssetAsync<T>(addressableKey);
                var addressableInstance = instanceOpHandle.WaitForCompletion();
                instance = addressableInstance;
            }
            else
            {
                instance = CreateInstance<T>();
            }

            return instance;
        }

        protected virtual void Awake()
        {
            // Verify there is only a single instance; catches accidental creation
            // from other CreateInstance() calls.
            if(instance != null)
            {
                Debug.LogError($"Multiple instances of singleton {typeof(T).Name} detected with names {instance.name} and {name}");
            }

            // Ensure instance is assigned here to prevent possible double-creation
            // should the instance property be called by a derived class handler.
            instance = (T)this;

            // Prevent Resources.UnloadUnusedAssets() from destroying the instance if called or when new scenes are loaded.
            instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
        }

        // Called when the singleton is created *or* after a domain reload in the editor.
        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        // Called when entering or exiting play mode.
        void OnPlayModeStateChanged(UnityEditor.PlayModeStateChange stateChange)
        {
            // Note that EnteredEditMode is used because ExitingPlayMode occurs *before*
            // MonoBehavior.OnDestroy() which is likely too early.
            if (stateChange == UnityEditor.PlayModeStateChange.EnteredEditMode)
            {
                UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                instance = null;
            }
        }
#endif
    }
}
