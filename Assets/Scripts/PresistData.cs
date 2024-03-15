using UnityEngine;

public class PresistData : MonoBehaviour
{
    public static PresistData instance;
    public string PresText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Ensures only one instance exists
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
