using UnityEngine;

namespace Notteam.AppCore
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private string sceneLoad;

        public void Load()
        {
            App.LoadScene(sceneLoad);
        }
    }
}
