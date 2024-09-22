using UnityEngine;

namespace Notteam.AppCore
{
    [CreateAssetMenu(fileName = "ManagerSettings", menuName = "Notteam/AppCore/Create Manager Settings", order = 0)]
    public class AppManagerSettings : ScriptableObject
    {
        [Header("Scene")]
        [SerializeField] private string loadScene;
        [SerializeField] private string mainScene;

        public string LoadScene => loadScene;
        public string MainScene => mainScene;
    }
}
