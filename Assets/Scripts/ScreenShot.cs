using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class ScreenShot : MonoBehaviour
{
	public AudioSource audioSource;
	private float shotWaitTime = 0.2f; //�Կ� �� ��� �ð� (UI ��Ȱ��ȭ ó��)

	public PlacementOnPlane placementOnPlane;

	void Awake()
	{
		//*�ȵ���̵� ��ó���� �ʿ�

		//�ȵ���̵� ��ũ ���� ���� ������ �ȵǾ� ������
		if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
		{
			//���� ���� ��� ��û
			Permission.RequestUserPermission(Permission.ExternalStorageWrite);
		}
	}

	//��ũ���� ��ư Ŭ��
	public void OnScreenShotButtonClick()
	{
		StartCoroutine(ScreenShotCoroutine());
	}

	//��ũ���� ���� �ڷ�ƾ
	IEnumerator ScreenShotCoroutine()
	{
		// UI �� Ʈ��ŷ ��� ��Ȱ��ȭ
		placementOnPlane.VisibleScreenUI(false);

		audioSource.Play(); //������ ���

		yield return new WaitForSeconds(shotWaitTime);

		//��ũ���� ����
		//NativeToolkit.SaveScreenshot("ARPhoto_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss"),
		//	"/storage/emulated/0/DCIM/Screenshots", "jpeg");

		yield return new WaitForSeconds(shotWaitTime);

		//UI �� Ʈ��ŷ ��� Ȱ��ȭ
		placementOnPlane.VisibleScreenUI(true);
	}
}
