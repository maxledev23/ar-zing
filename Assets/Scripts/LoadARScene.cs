using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadARScene : MonoBehaviour
{
   public void LoadScene(string sceneName) {
       SceneManager.LoadScene(sceneName);
   }
}
