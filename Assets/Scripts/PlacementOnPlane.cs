using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementOnPlane : MonoBehaviour
{
	//Plane에 배치할 게임오브젝트 프리팹
	public GameObject placePrefab;

	private GameObject spawnObject; //생성된 AR게임오브젝트

	public ARPlaneManager arPlaneManager; //AR Plane 매니저 참조
	public ARPointCloudManager arPointCloudManager; //AR Point Cloud 매니저 참조
	public ARRaycastManager arRaycastManager; //AR Raycast 매니저 참조

	// 터치 정보 List 생성
	private List<ARRaycastHit> touchHits = new List<ARRaycastHit>();

	public GameObject shotButton; //촬영 버튼 게임오브젝트

	void Update()
	{
		//UI 요소를 터치했을 경우에는 패스
		if (EventSystem.current.currentSelectedGameObject) return;

		//터치가 이루어졌다면
		if (Input.touchCount > 0)
		{
			//화면 터치 값을 추출
			Touch touch = Input.GetTouch(0);

			//AR바닥과 터치 위치간의 충돌점을 체크함
			if (arRaycastManager.Raycast(touch.position, touchHits, TrackableType.PlaneWithinPolygon))
			{
				//충돌된 터치 정보
				Pose HitPose = touchHits[0].pose;

				//AR 게임 오브젝트가 아직 생성된 상태가 아니라면
				if (spawnObject == null)
				{
					//터치한 위치에 AR게임 오브젝트를 생성함
					spawnObject = Instantiate(placePrefab, HitPose.position, HitPose.rotation);
				}
				else //이미 AR 게임오브젝트가 생성된 상태라면
				{
					//게임오브젝트의 위치를 옮겨줌
					spawnObject.transform.position = HitPose.position;
				}
			}
		}
	}

	//UI요소와 트래킹 요소들을 비활성화/활성화
	public void VisibleScreenUI(bool isVisible)
	{
		//평면 인식 기능 비활성화
		arPlaneManager.enabled = isVisible;

		//트래킹된 바닥 게임오브젝트를 참조하여 비활성화
		foreach (ARPlane plane in arPlaneManager.trackables)
		{
			plane.gameObject.SetActive(isVisible);
		}

		//포인트 클라우드 매니저 비활성화
		arPointCloudManager.enabled = isVisible;

		//트래킹된 포인트클라우드 게임오브젝트를 참조하여 비활성화
		foreach (ARPointCloud point in arPointCloudManager.trackables)
		{
			point.gameObject.SetActive(isVisible);
		}

		shotButton.SetActive(isVisible);
	}

}
