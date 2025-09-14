using UnityEngine;

public class GameManager : MonoBehaviour
{
    //- Singleton -//
    public static GameManager Instance { get; private set; }
    private void CheckInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Awake()
    {
        CheckInstance();
    }

    public void LogToConsole(string inString)
    {
        Debug.Log(inString);
    }
}
