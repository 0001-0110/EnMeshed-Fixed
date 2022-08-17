using UnityEngine;
using Photon.Pun;

namespace Services
{
    public static class PhotonService
    {
        public static void AddToPrefabPool(params GameObject[] prefabs)
        {
            DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
            foreach (GameObject prefab in prefabs)
            {
                pool.ResourceCache.Add(prefab.name, prefab);
            }
        }
    }
}