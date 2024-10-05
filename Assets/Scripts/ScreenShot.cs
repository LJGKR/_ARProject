using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ScreenShot : MonoBehaviour
{
	public AudioSource audioSource;
	private float shotWaitTime = 0.2f; //촬영 전 대기 시간 (UI 비활성화 처리)

	public PlacementOnPlane placementOnPlane;

	void Awake()
	{
		//*안드로이드 전처리기 필요

		//안드로이드 디스크 저장 권한 설정이 안되어 있으면
		if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
		{
			//저장 권한 허용 요청
			Permission.RequestUserPermission(Permission.ExternalStorageWrite);
		}
	}

	//스크린샷 버튼 클릭
	public void OnScreenShotButtonClick()
	{
		StartCoroutine(ScreenShotCoroutine());
	}

	//스크린샷 생성 코루틴
	IEnumerator ScreenShotCoroutine()
	{
		// UI 및 트래킹 요소 비활성화
		placementOnPlane.VisibleScreenUI(false);

		audioSource.Play(); //셔터음 재생

		yield return new WaitForSeconds(shotWaitTime);

		//스크린샷 동작
		//NativeToolkit.SaveScreenshot("ARPhoto_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"),
		//	"/storage/emulated/0/DCIM/Screenshots", "jpeg");

		yield return new WaitForSeconds(shotWaitTime);

		//UI 및 트래킹 요소 활성화
		placementOnPlane.VisibleScreenUI(true);
	}
}
