using UnityEditor;
using UnityEngine;

// Implement from https://hextantstudios.com/unity-singletons/
// A MonoBehaviour-based singleton for use at runtime. It should *not* be placed in
// a scene as it will be created on demand.
// Note: OnEnable() / OnDisable() should be used to register with any global events
// to properly support domain reloads.
namespace KaneTemplate.Singleton
{
    public class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : MonoBehaviourSingleton<T>
    {
        // The singleton instance.
        public static T Instance => instance != null ? instance :
            Application.isPlaying                    ? Initialize() : null;
        static T instance;

        // True if the singleton instance has been destroyed. Used to prevent possible
        // re-creation of the singleton when exiting.
        protected static bool destroyed;

        // Finds or creates the singleton instance and stores it in instance. This can
        // be called from a derived type to ensure creation of the singleton using the
        // [RuntimeInitializeOnLoadMethod] attribute on a static method.
        protected static T Initialize()
        {
            // Prevent re-creation of the singleton during play mode exit.
            if (destroyed) return null;

            // If the instance is already valid, return it. Needed if called from a
            // derived class that wishes to ensure the instance is initialized.
            if (instance != null) return instance;

            // Find the existing instance (across domain reloads).
            if ((instance = FindObjectOfType<T>()) != null) return instance;

            // Create a new GameObject instance to hold the singleton component.
            var gameObject = new GameObject(typeof(T).Name);

            // Move the instance to the DontDestroyOnLoad scene to prevent it from
            // being destroyed when the current scene is unloaded.
            DontDestroyOnLoad(gameObject);

            // Create the MonoBehavior component. Awake() will assign _instance.
            return gameObject.AddComponent<T>();
        }

        protected virtual void Awake()
        {
            // Verify there is not more than one instance and assign instance.
            if(instance != null)
            {
                Debug.LogError($"More than one instance of {typeof(T).Name} exists with names {instance.name} and {name}.");
            }
            instance = (T)this;
        }

        // Clear the instance field when destroyed and prevent it from being re-created.
        protected virtual void OnDestroy()
        {
            instance = null;
            destroyed = true;
        }

        // Called when the singleton is created *or* after a domain reload in the editor.
        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        // Called when entering or exiting play mode.
        static void OnPlayModeStateChanged(PlayModeStateChange stateChange)
        {
            // Reset static destroyed field. Required when domain reloads are disabled.
            // Note: ExitingPlayMode is called too early.
            if (stateChange == PlayModeStateChange.EnteredEditMode)
            {
                EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                destroyed = false;
            }
        }
#endif
    }
}
