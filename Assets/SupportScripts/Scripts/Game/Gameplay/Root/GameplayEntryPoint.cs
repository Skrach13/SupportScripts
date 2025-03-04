using UnityEngine;

namespace SupportScripts.Scripts.Gameplay
{

    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _sceneRootBinder;

        public void Run()
        {
            Debug.Log("Gameplay scene loaded");
        }
    }
}