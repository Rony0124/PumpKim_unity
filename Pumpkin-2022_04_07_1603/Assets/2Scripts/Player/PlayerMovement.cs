using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    static PlayerMovement instance;
    public static PlayerMovement Instance
    {
        get { return instance; }
    }
    void Awake() { instance = this; }
    [SerializeField] public AudioClip buttonCliclSFX;
    Rigidbody2D rb;
    public float moveSpeed;
    public bool IsMoving =true;
    Vector2 CollisionDis;
    private Gate theGate;
    private SpriteRenderer sprite;
    //PlayerData playerData;
    
    // Start is called before the first frame update
    void Start()
    {
       
        //playerData = FindObjectOfType<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
        theGate = FindObjectOfType<Gate>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (JoyStickController.Instance.JoyVec.x != 0 || JoyStickController.Instance.JoyVec.y != 0)
        {
            if (IsMoving)
            {
                rb.velocity = new Vector3(JoyStickController.Instance.JoyVec.x, JoyStickController.Instance.JoyVec.y) * moveSpeed;
            }

        } else
        {
            //Debug.Log(prev);
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.1f);
            
        }
        //rb.AddForce(moveVec);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("Boss") )
        {
            AudioMng.Instance.PlaySFX(buttonCliclSFX);
            PlayerData.Instance.currentHp -= 1;
            CollisionDis = (transform.position - other.transform.position).normalized;
            IsMoving = false;
            StartCoroutine(OnDamaged(CollisionDis));
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Gate"))
        {
            
            Vector3 GatePos = theGate.CalcGateDis(collision.transform.localPosition);
            Debug.Log(GatePos);
            if (GatePos.x < 0 )
                transform.position = new Vector3(transform.position.x + 2.5f, transform.position.y, 0f);
            else if(GatePos.x > 0)
                transform.position = new Vector3(transform.position.x - 2.5f, transform.position.y, 0f);
            else if (GatePos.y > 0)
                transform.position = new Vector3(transform.position.x, transform.position.y - 2.5f, 0f);
            else if (GatePos.y < 0)
                transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, 0f);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        if (collision.transform.CompareTag("NextStage"))
        {
            StageMng.Instance.NextStage();
        }
        if (collision.transform.CompareTag("EnemyProjectile")|| collision.transform.CompareTag("Enemy"))
        {
            AudioMng.Instance.PlaySFX(buttonCliclSFX);
            PlayerData.Instance.currentHp -= 1;
            IsMoving = false;
            StartCoroutine(OnDamaged((transform.position - collision.transform.position).normalized));
        }
    }

    IEnumerator OnDamaged(Vector2 CollisionDis)
    {
        UIController.Instance.Damage();
        gameObject.layer = 16;
        //yield return null;
        //Debug.Log("collisiondis" + CollisionDis);
        
        sprite.color = new Color(1f, 1f, 1f, 0.4f);
        
        rb.AddForce(CollisionDis * 3, ForceMode2D.Impulse);
        Invoke("LetHimMove", 0.2f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1f, 1f, 1f, 0.8f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1f, 1f, 1f, 0.4f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1f, 1f, 1f, 0.8f);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(1f, 1f, 1f, 0.4f);
        yield return new WaitForSeconds(0.5f);
        OffDamaged();
    }
    public Vector3 GetPos()
    {
        return transform.position;
    }
    void OffDamaged()
    {
        
        sprite.color = new Color(1f, 1f, 1f, 1f);
        gameObject.layer = 9;
    }
    void LetHimMove()
    {
        IsMoving = true;
    }

}
