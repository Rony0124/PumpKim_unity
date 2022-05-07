using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController_spider : MonoBehaviour
{
    static BossController_spider instance;
    public static BossController_spider Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake() { instance = this; }
  
    public bool isDead;//보스가 죽었는지 체크
    [SerializeField] public AudioClip buttonCliclSFX;

    //private float timeBtwDamage = 1.5f;
   
    public float speed;
    //public ParticleSystem dust;

    private Vector4 color;
    RoomCondition roomCondition;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        //playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        color = gameObject.GetComponent<SpriteRenderer>().color;
        //Player = GameObject.FindGameObjectWithTag("Player");
        if (gameObject.transform.parent == null) return;
        roomCondition = gameObject.transform.parent.GetComponent<RoomCondition>();

    }

    // Update is called once per frame
    void Update()
    {
        //necessary?
        //if (PlayerTargeting.Instance.TargetIndex == -1) return;
        if (roomCondition == null) return;
        if (roomCondition.playerInThisRoom)
        {
            StartCoroutine(Wait4Player());
        }
    }

    IEnumerator Wait4Player()
    {
        yield return new WaitForSeconds(1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            
            gameObject.GetComponent<Animator>().SetTrigger("IsStop");
        }
    }

    

 
}
