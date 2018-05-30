using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomLoader : MonoBehaviour {

    [SerializeField]private Button btn;

	private void LoadRoom()
    {
        Debug.Log(btn.name + " Click");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync((currentScene.name == "RoomList" ? "JoinRoom" : "RoomList"));
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable " + btn.name);
        btn.onClick.AddListener(LoadRoom);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable " + btn.name);
        btn.onClick.RemoveListener(LoadRoom);
    }
}
