using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementOnPlane : MonoBehaviour
{
	//Plane�� ��ġ�� ���ӿ�����Ʈ ������
	public GameObject placePrefab;

	private GameObject spawnObject; //������ AR���ӿ�����Ʈ

	public ARPlaneManager arPlaneManager; //AR Plane �Ŵ��� ����
	public ARPointCloudManager arPointCloudManager; //AR Point Cloud �Ŵ��� ����
	public ARRaycastManager arRaycastManager; //AR Raycast �Ŵ��� ����

	// ��ġ ���� List ����
	private List<ARRaycastHit> touchHits = new List<ARRaycastHit>();

	public GameObject shotButton; //�Կ� ��ư ���ӿ�����Ʈ

	void Update()
	{
		//UI ��Ҹ� ��ġ���� ��쿡�� �н�
		if (EventSystem.current.currentSelectedGameObject) return;

		//��ġ�� �̷�����ٸ�
		if (Input.touchCount > 0)
		{
			//ȭ�� ��ġ ���� ����
			Touch touch = Input.GetTouch(0);

			//AR�ٴڰ� ��ġ ��ġ���� �浹���� üũ��
			if (arRaycastManager.Raycast(touch.position, touchHits, TrackableType.PlaneWithinPolygon))
			{
				//�浹�� ��ġ ����
				Pose HitPose = touchHits[0].pose;

				//AR ���� ������Ʈ�� ���� ������ ���°� �ƴ϶��
				if (spawnObject == null)
				{
					//��ġ�� ��ġ�� AR���� ������Ʈ�� ������
					spawnObject = Instantiate(placePrefab, HitPose.position, HitPose.rotation);
				}
				else //�̹� AR ���ӿ�����Ʈ�� ������ ���¶��
				{
					//���ӿ�����Ʈ�� ��ġ�� �Ű���
					spawnObject.transform.position = HitPose.position;
				}
			}
		}
	}

	//UI��ҿ� Ʈ��ŷ ��ҵ��� ��Ȱ��ȭ/Ȱ��ȭ
	public void VisibleScreenUI(bool isVisible)
	{
		//��� �ν� ��� ��Ȱ��ȭ
		arPlaneManager.enabled = isVisible;

		//Ʈ��ŷ�� �ٴ� ���ӿ�����Ʈ�� �����Ͽ� ��Ȱ��ȭ
		foreach (ARPlane plane in arPlaneManager.trackables)
		{
			plane.gameObject.SetActive(isVisible);
		}

		//����Ʈ Ŭ���� �Ŵ��� ��Ȱ��ȭ
		arPointCloudManager.enabled = isVisible;

		//Ʈ��ŷ�� ����ƮŬ���� ���ӿ�����Ʈ�� �����Ͽ� ��Ȱ��ȭ
		foreach (ARPointCloud point in arPointCloudManager.trackables)
		{
			point.gameObject.SetActive(isVisible);
		}

		shotButton.SetActive(isVisible);
	}

}
