using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject currentCube;

    private bool release = false;
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Position Storage Variables
    private Vector3 tempPos = new();
    private Vector3 startPos = new();

    [SerializeField] private List<GameObject> availableObject = new();
    [SerializeField] private GameObject originalObject;
    [SerializeField] private int amountOfCopies;
    [SerializeField] private float spawnInterval = 3.5f;

    private void Awake()
    {
        for (int index = 0; index < amountOfCopies; index++)
        {
            CreateCopy();
        }
    }
    private void Start()
    {
        startPos = transform.position;
        startPos.y = transform.position.y + 1.15f;
        StartCoroutine(SpawnCube());
    }
    private void LevitateCube()
    {
        currentCube.transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.Self);

        // Float up/down with a Sin()
        tempPos = startPos;
        tempPos.y += Mathf.Clamp(Mathf.Sin(Time.deltaTime * Mathf.PI * frequency) * amplitude, 0.3f, 2.5f);

        currentCube.transform.position = tempPos;
    }
    private void SetOnSpawner(GameObject cube)
    {
        if (!currentCube.Equals(cube))
        {
            currentCube = cube;
        }
        cube.SetActive(true);
        cube.transform.SetPositionAndRotation(startPos, Quaternion.identity);
        release = true;
        StartCoroutine(ReactivateSpawner());
    }
    private void CreateCopy()
    {
        GameObject tempObject = Instantiate(originalObject, transform);
        tempObject.GetComponent<CubePickable>().LinkToPool(this);
        tempObject.gameObject.SetActive(false);
        availableObject.Add(tempObject);
    }

    public void ResetCube(GameObject cube)
    {
        cube.transform.SetParent(transform);
        cube.SetActive(false);
        availableObject.Add(cube);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            release = true;
        }

       
        if (!release && !other.transform.parent.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<CubePickable>() != null && currentCube != null && !currentCube.Equals(other))
            {
                ResetCube(currentCube);
                currentCube = other.gameObject;
                SetOnSpawner(other.gameObject);

            }
            if (other.gameObject.GetComponent<CubePickable>() != null && currentCube == null)
            {
                currentCube = other.gameObject;
                SetOnSpawner(other.gameObject);
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            release = false;
        }
        if (other.gameObject.Equals(currentCube))
        {
            currentCube = null;
            StartCoroutine(SpawnCube());
        }
    }
    private void Update()
    {
        if (!release)
        {
            if (currentCube != null)
            {
                if (!currentCube.activeSelf)
                {
                    currentCube.SetActive(true);
                }
                LevitateCube();
            }
            
        }
    }
    IEnumerator SpawnCube()
    {
        yield return new WaitForSeconds(spawnInterval);
        if (currentCube == null)
        {
            if (availableObject.Count <= 0)
            {
                CreateCopy();
            }
            currentCube = availableObject[0];
            SetOnSpawner(availableObject[0]);
            availableObject.RemoveAt(0);
        }
    }
    IEnumerator ReactivateSpawner()
    {
        yield return new WaitForSeconds(0.4f);
        float time = 0;
        float lerpDuration = 4;
        float startY = currentCube.transform.position.y;
        while (time < lerpDuration)
        {
            float tempY = Mathf.Lerp(startY, startPos.y, time/lerpDuration);
            time += Time.deltaTime;
            currentCube.transform.position += new Vector3(0, tempY, 0);
        }
        release = false;
    }
}
