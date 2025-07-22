using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    private Transform camera;
    private Transform target;
    private Transform canvas;

    public Vector3 offset;

    private TMP_Text textBox;
    public string[] lines;
    public float textSpeed;
    private int index;
    private PlayerInputActions playerControls;

    void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera  = Camera.main.transform;
        target = transform.parent;
        canvas = GameObject.FindFirstObjectByType<Canvas>().transform;

        transform.SetParent(canvas);
        textBox = GetComponent<TMP_Text>();
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        transform.position = target.position + offset;

        if (playerControls.Player.Attack.triggered) {
            if (textBox.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textBox.text = lines[index];
            }
        }
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText() {
        foreach (char c in lines[index].ToCharArray()) {
            textBox.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;    
            textBox.text = string.Empty;
            StartCoroutine(TypeText());
        } else {
            gameObject.SetActive(false);
        }
    }
}
