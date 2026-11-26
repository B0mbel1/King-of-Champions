using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TMP_Text nameText;

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
        if (characterPrefabs.Length == 0) return;

        index = (newIndex % characterPrefabs.Length + characterPrefabs.Length) % characterPrefabs.Length;

        if (current != null) Destroy(current);

        current = Instantiate(characterPrefabs[index], spawnPoint.position, Quaternion.identity);

        if (nameText != null) nameText.text = characterPrefabs[index].name;
    }
}