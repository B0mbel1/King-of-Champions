using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class CharacterEntry
{
    public string displayName;
    public GameObject prefab;
}

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private CharacterEntry[] characters;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private float characterScale = 0.4f;

    private int index;
    private GameObject current;

    void Start()
    {
        Show(0);
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) Next();
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) Previous();
    }

    public void Next()     => Show(index + 1);
    public void Previous() => Show(index - 1);

    private void Show(int newIndex)
    {
        if (characters.Length == 0) return;

        // Zawijanie: po ostatniej wraca do pierwszej i odwrotnie
        index = (newIndex % characters.Length + characters.Length) % characters.Length;

        if (current != null) Destroy(current);

        CharacterEntry entry = characters[index];
        current = Instantiate(entry.prefab, spawnPoint.position, Quaternion.identity);
        current.transform.localScale = Vector3.one * characterScale;

        if (nameText != null) nameText.text = entry.displayName;
    }

    public void Confirm()
    {
        PlayerPrefs.SetInt("SelectedCharacter", index);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}