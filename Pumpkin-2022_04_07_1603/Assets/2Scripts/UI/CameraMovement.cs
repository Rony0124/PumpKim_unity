using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    static CameraMovement instance;
    public static CameraMovement Instance
    {
        get { return instance; }

    }
    void Awake()
    {
        instance = this;

    }

    public BoxCollider2D bound;

    // 박스 콜라이도 영역의 최소 최대 xyz값을 가짐
    public Vector3 minMapBound;
    public Vector3 maxMapBound;

    // 카메라의 반너비, 반높이 값을 지닐 변수.
    private float halfWidth;
    private float halfHeight;

    //카메라의 반높이값을 구할 속성을 이용하기 위한 변수
    public Camera theCamera;


    
   
    public float offsetY = 0;
    public float offsetX = -25.5f;
    public Image FadeInOutImg;

    Vector3 cameraPosition;



    // Start is called before the first frame update
    private void Start()
    {
        
        theCamera = GetComponent<Camera>();
        minMapBound = bound.bounds.min;
        maxMapBound = bound.bounds.max;
        theCamera.orthographicSize = (float)Screen.width / Screen.height;
        halfHeight = theCamera.orthographicSize;
        //halfHeight = 2f;
        halfWidth = halfHeight * Screen.width / Screen.height; //카메라 반너비 구하는 공식

    }
    void LateUpdate() //카메라가 따라가는 object가 update 함수 안에서 움직일 경우가 있기 때문에 LateUpdate를 사용한다.
    {
      
        //Debug.Log(Player.transform.position);
        this.transform.position = new Vector3(PlayerMovement.Instance.GetPos().x, PlayerMovement.Instance.GetPos().y, -10.0f);

        float clampedX = Mathf.Clamp(this.transform.position.x, minMapBound.x + halfWidth, maxMapBound.x - halfWidth); // Clamp() 범위 안에 가둬두는 함수
        float clampedY = Mathf.Clamp(this.transform.position.y, minMapBound.y + halfHeight, maxMapBound.y - halfHeight); // Clamp() 범위 안에 가둬두는 함수
        //Debug.Log("" + clampedX + "as" + clampedY);
        this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
       // Debug.Log("width" + Screen.width + "height" + Screen.height);

    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minMapBound = bound.bounds.min;
        maxMapBound = bound.bounds.max;
    }

    public void CameraNextRoom(float x, float y)
    {
        cameraPosition.y = y;
        cameraPosition.x = x;
        cameraPosition.z = -10f;
        transform.position = cameraPosition;
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        float a = 1f;
        FadeInOutImg.color = new Vector4(0f, 0f, 0f, a);
        yield return new WaitForSeconds(0.3f);

        while (a >= 0f)
        {
            FadeInOutImg.color = new Vector4(0f, 0f, 0f, a);
            a -= 0.05f;
            yield return null;
        }
    }

}
