using UnityEngine;

namespace Notteam.AppCore
{
    [CreateAssetMenu(fileName = "ManagerSettings", menuName = "Notteam/AppCore/Create Manager Settings", order = 0)]
    public class AppManagerSettings : ScriptableObject
    {
        [Header("Scene")]
        [SerializeField] private string loadScene;
        [Tooltip("First app scene after boot and load")]
        [SerializeField] private string mainScene;

        public string LoadScene => loadScene;
        public string MainScene => mainScene;
    }
}
