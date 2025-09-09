using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controller
{
    public class EndGameScreenController : MonoBehaviour
    {
        [SerializeField] private string mainSceneName;
    
        public void ResetGame()
        {
            SceneManager.LoadScene(mainSceneName);
        }
    }
}
