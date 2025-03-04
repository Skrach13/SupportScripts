using UnityEngine;

namespace SupportScripts.Scripts
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;

        private void Awake()
        {
            HideLoadingsScreen();
        }

        public void ShowLoadingsScreen()
        {
            _loadingScreen.SetActive(true);
        } 
        public void HideLoadingsScreen()
        {
            _loadingScreen.SetActive(false);
        }
    }
}
