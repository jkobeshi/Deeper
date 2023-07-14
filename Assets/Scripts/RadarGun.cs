using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarGun : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private CircleCollider2D circleCollider;
    private float currentSize;

    [SerializeField] private int numVertices = 100;
    [SerializeField] private float lineWidth = 0.05f;
    [SerializeField] private float maxRadius = 12f;
    [SerializeField] private float expansionRate = 5f;
    private Vector3 startPosition;

    [SerializeField] private GameObject enemyPingLight;
    [SerializeField] private GameObject collectablePingLight;
    [SerializeField] private GameObject goalPingLight;
    [SerializeField] private GameObject cashPingLight;
    [SerializeField] private GameObject safeZonePingLight;
    [SerializeField] private AudioClip radarSonarNoise;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1);
        StartCoroutine(SendRadarWave());
    }

    void DrawPolygon(float radius, Vector3 centerPos)
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / numVertices;
        lineRenderer.positionCount = numVertices;

        for (int i = 0; i < numVertices; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(
                new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(0, 0, 0, 1)
            );
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        CustomTags tags = other.gameObject.GetComponent<CustomTags>();

        bool hasTag = other.gameObject.CompareTag("Enemy");
        if (tags != null)
        {
            hasTag = hasTag || tags.HasTag("Enemy");
        }
        if (hasTag) {
            Instantiate(enemyPingLight, other.gameObject.transform.position + new Vector3(0, 0, -0.75f),
                        Quaternion.identity);
            return;
        }

        hasTag = false;
        hasTag = other.gameObject.CompareTag("Collectable");
        if (tags != null)
        {
            hasTag = hasTag || tags.HasTag("Collectable");
        }
        if (hasTag){
            Instantiate(collectablePingLight, other.gameObject.transform.position + new Vector3(0, 0, -0.75f),
                        Quaternion.identity);
            return;
        }

        hasTag = false;
        hasTag = other.gameObject.CompareTag("Goal");
        if (tags != null)
        {
            hasTag = hasTag || tags.HasTag("Goal");
        }
        if (hasTag) {
            Instantiate(goalPingLight, other.gameObject.transform.position + new Vector3(0, 0, -0.75f),
                        Quaternion.identity);
            return;
        }

        hasTag = false;
        hasTag = other.gameObject.CompareTag("CashPickup");
        if (tags != null)
        {
            hasTag = hasTag || tags.HasTag("CashPickup");
        }
        if (hasTag) {
            Instantiate(cashPingLight, other.gameObject.transform.position + new Vector3(0, 0, -0.75f),
                        Quaternion.identity);
            return;
        }

        hasTag = false;
        hasTag = other.gameObject.CompareTag("SafeZone");
        if (tags != null)
        {
            hasTag = hasTag || tags.HasTag("SafeZone");
        }
        if (hasTag && other.gameObject.name != "Above Ground Safezone") {
            Instantiate(safeZonePingLight, other.gameObject.transform.position + new Vector3(0, 0, -0.75f),
                        Quaternion.identity);
            return;
        }
    }

    IEnumerator SendRadarWave() {
        AudioSource.PlayClipAtPoint(radarSonarNoise, gameObject.transform.position, GameControl.sound_effects_vol);
        circleCollider.enabled = true;
        currentSize = 0.5f;
        startPosition = gameObject.transform.position;
        while (currentSize < maxRadius) {
            circleCollider.radius = currentSize;
            DrawPolygon(currentSize, startPosition);
            currentSize += Time.deltaTime * expansionRate;
            yield return null;
        }
        circleCollider.radius = 0f;
        circleCollider.enabled = false;
        lineRenderer.positionCount = 0;
        Destroy(gameObject);
    }
}
