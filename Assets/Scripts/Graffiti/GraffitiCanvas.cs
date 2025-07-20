using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GraffitiCanvas : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);
    public Camera mainCamera;
    public int spraySize = 20; // Denotes the height and width of a spray
    public Color sprayColor = Color.black;
    private Color[] colors;
    private bool updateMipMap = false;
    private bool sprayedLastFrame = false;
    private Vector2 lastSprayPos;

    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
        colors = Enumerable.Repeat(sprayColor, spraySize * spraySize).ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            var mouseScreenPosition = Mouse.current.position.ReadValue();

            var ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject != this.gameObject) return;

                var touchPos = new Vector2(hit.textureCoord.x, hit.textureCoord.y);

                var x = (int)(touchPos.x * textureSize.x) - (spraySize / 2);
                var y = (int)(touchPos.y * textureSize.y) - (spraySize / 2);

                if (y < 0 || y > textureSize.y || x < 0 || x > textureSize.x) return;

                if (sprayedLastFrame)
                {
                    texture.SetPixels(x, y, spraySize, spraySize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(lastSprayPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(lastSprayPos.y, y, f);
                        texture.SetPixels(lerpX, lerpY, spraySize, spraySize, colors);
                    }

                    texture.Apply(false);

                    updateMipMap = true;
                }

                lastSprayPos = new Vector2(x, y);
                sprayedLastFrame = true;
                return;
            }
        }

        sprayedLastFrame = false;

        if (updateMipMap && !Mouse.current.leftButton.isPressed)
        {
            Debug.Log("GraffitiCanvas: Updating MipMap");

            texture.Apply(true);

            updateMipMap = false;
        }
    }
}
