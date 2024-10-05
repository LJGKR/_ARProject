using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImageTrackingManager : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager arImageTrackedManager;

    [SerializeField] GameObject[] objectPrefabs;

    private Dictionary<string, GameObject> spawnObject;

	void Awake()
	{
		arImageTrackedManager = GetComponent<ARTrackedImageManager>();
		spawnObject = new Dictionary<string, GameObject>();

		foreach(GameObject obj in objectPrefabs)
		{
			GameObject newObject = Instantiate(obj);
			newObject.name = obj.name;
			newObject.SetActive(false);

			spawnObject.Add(newObject.name, newObject);
		}
	}

	void OnEnable()
	{
		arImageTrackedManager.trackedImagesChanged += OnTrackedImageChanged;
	}

	void OnDisable()
	{
		arImageTrackedManager.trackedImagesChanged -= OnTrackedImageChanged;
	}

	void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs arEventArgs)
	{
		foreach(ARTrackedImage arTrackedImage in arEventArgs.added)
		{
			UpdateObject(arTrackedImage);
		}

		foreach (ARTrackedImage arTrackedImage in arEventArgs.updated)
		{
			UpdateObject(arTrackedImage);
		}

		foreach (ARTrackedImage arTrackedImage in arEventArgs.removed)
		{
			spawnObject[arTrackedImage.referenceImage.name].SetActive(false);
		}
	}

	void UpdateObject(ARTrackedImage arTrackedImage)
	{
		string referenceImageName = arTrackedImage.referenceImage.name;

		spawnObject[referenceImageName].transform.position = arTrackedImage.transform.position;
		spawnObject[referenceImageName].transform.rotation = arTrackedImage.transform.rotation;

		spawnObject[referenceImageName].SetActive(true);
	}
}
