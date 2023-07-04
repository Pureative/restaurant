using UnityEngine.AddressableAssets;

namespace KaneTemplate.Utility
{
    public static class AddressableUtility
    {
        public static bool IsAddressableKeyExists(string key)
        {
            var handle = Addressables.LoadResourceLocationsAsync(key);
            handle.WaitForCompletion();
            return handle.Result.Count > 0;
        }
    }
}
