using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    static PlayerTargeting instance;
    public static PlayerTargeting Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerTargeting>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("PlayerTargeting");
                    instance = instanceContainer.AddComponent<PlayerTargeting>();

                }
            }
            return instance;
        }
    }
    void Awake() { instance = this; }
    [SerializeField] public AudioClip buttonCliclSFX;
    public GameObject crossHair;
    public Rigidbody2D rb;
    public GameObject bulletPrefab;

    float closestDist = Mathf.Infinity;
    int closestDistIndex = 0;
    public float TargetDist = 100f;
    public float currentDist;
    public int TargetIndex;
    public int prevTargetIndex = 0;

    public List<GameObject> MonsterList = new List<GameObject>();

    public GameObject Player;
    public Animator playerAnim;
    public Transform AttackPoint;
    public int bulletindex = 0;
    PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = Player.transform.GetComponent<Animator>();
        bulletindex = (int)DataMng.instance.currentWeapon *2;
        playerData = PlayerData.Instance;
        if (playerData.PlayerSkill[2] != 0 )
        {
            
            bulletindex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();
        //테스트용
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }*/
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < MonsterList.Count; i++)
        {
            if (MonsterList[i] == null) { return; }
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, MonsterList[i].transform.position - transform.position);
        }
        
    }

    void SetTarget()
    {
        if (MonsterList.Count != 0)
        {
            Vector3 aim = Vector3.zero;
            crossHair.SetActive(true);
            prevTargetIndex = TargetIndex;
            currentDist = 0f;
            closestDistIndex = 0;
            TargetIndex = -1;
            for (int i = 0; i < MonsterList.Count; i++)
            {
                if (MonsterList[i] == null) { return; }
                currentDist = Vector3.Distance(transform.position, MonsterList[i].transform.position);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, MonsterList[i].transform.position - transform.position);
      
                if (hit.transform.CompareTag("Enemy"))
                {
                    if (TargetDist >= currentDist)
                    {
                        TargetDist = currentDist;
                        TargetIndex = i;
                        if(prevTargetIndex != TargetIndex)
                        {
                            TargetIndex = prevTargetIndex;
                        }
                    }
                }
                if (closestDist >= currentDist)
                {
                    closestDistIndex = i;
                    closestDist = currentDist;
                }

            }
            if (TargetIndex == -1)
            {
                TargetIndex = closestDistIndex;
            }
            closestDist = 100f;
            TargetDist = 100f;
            //getATarget = true;
            aim = new Vector3(MonsterList[TargetIndex].transform.position.x, MonsterList[TargetIndex].transform.position.y, 0.0f);

            crossHair.transform.position = aim;
        }
        else
        {
            crossHair.SetActive(false);
            return; 
        }
    }

    public void Shoot()
    {
        AudioMng.Instance.PlaySFX(buttonCliclSFX);
        Bullet();
        //멀티샷
        if (playerData.PlayerSkill[1] != 0)
        {
            Invoke("Bullet", 0.2f);
        }
    }
    void Bullet()
    {
        Vector3 shootingDir;
        shootingDir = crossHair.transform.localPosition;
        GameObject bullet;
        if (MonsterList.Count == 0)
        {
            //shootingDir = AttackPoint.localPosition - Player.transform.localPosition;
            //shootingDir.Normalize();
            
            shootingDir = JoyStickController.Instance.JoyVec;
            //playerskill[2] 값이 2이상 되는 경우?
            //Debug.Log(bulletindex);
            
            bullet = Instantiate(playerData.PlayerBolt[bulletindex], Player.transform.position, Quaternion.identity);
            if (shootingDir == Vector3.zero)
            {
                shootingDir = new Vector3(0.0f, -1.0f, 0.0f);
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            }
            Projectile projectileScript = bullet.transform.GetComponent<Projectile>();

            projectileScript.velocity = shootingDir * 8f;
            float angle = Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
         
            Destroy(bullet, 2.0f);

            //몬스터 0, aim없을때
            
        }
        else
        {
            shootingDir.Normalize();
           
            bullet = Instantiate(playerData.PlayerBolt[bulletindex], Player.transform.position, Quaternion.identity);
            Projectile projectileScript = bullet.GetComponent<Projectile>();
            projectileScript.velocity = shootingDir * 8f;
            float angle = -Mathf.Atan2(shootingDir.x, shootingDir.y) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            Destroy(bullet, 2.0f);
            return;

        }
    }

    
    
}
