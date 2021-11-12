using System.Collections;
using UnityEngine;

namespace LostWarehouse.ProviderManager
{
    /// <summary>
    /// Allows Located services to transfer data to their replacements if needed.
    /// </summary>
    /// <typeparam name="T">The base interface type you want to Provide.</typeparam>
    public interface IProvider<T>
    {
        void OnReProvided(T previousProvider);
    }
}