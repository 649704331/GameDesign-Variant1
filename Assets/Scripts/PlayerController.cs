using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int State;//角色状态
    private int oldState = 0;//前一次角色的状态
    private int UP = 0;//角色状态向前
    private int RIGHT = 1;//角色状态向右
    private int DOWN = 2;//角色状态向后
    private int LEFT = 3;//角色状态向左
    public Slider player_warrior_hp;
    //boss关卡
    private bool isThreeLevel = false;
    private GameObject boss_bro;
    private Animator bossAnim;
    private float disToBossBro;
    private bool broAttack;

    private Animator anim;
    
    private int MoveSpeed = 8;
    private Transform MainCamera;

    private Rigidbody Rig;
    //public GameObject JumpEffect;

    public GameObject myGameManager;

    void Start()
    {
        Data.warrior_hp = Data.warrior_max_hp * (Data.wolf_hp / Data.wolf_max_hp);
        player_warrior_hp.value = Data.warrior_hp;
        MainCamera = GameObject.Find("Camera").GetComponent<Transform>();
        anim = this.GetComponent<Animator>();
        transform.rotation = new Quaternion(0, MainCamera.rotation.y, 0, MainCamera.rotation.w);
        Rig = GetComponent<Rigidbody>();
        myGameManager = GameObject.Find("GameManager");
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "SceneThree")
        {
            broAttack = false;
            isThreeLevel = true;
            boss_bro = GameObject.Find("Boss_brother");
            bossAnim = boss_bro.GetComponent<Animator>();
            disToBossBro = Vector3.Distance(boss_bro.transform.position, transform.position);
        }
    }
    void Update()
    {
        if (Data.isLive && Data.isGame)
        {
            if (transform.position.y <= -1)
            {
                anim.SetTrigger("Death");
                Data.warrior_hp = 0;
                Data.isLive = false;
            }
            //伤害判定
            int tmp = Data.attackTime;
            if (tmp >= 0)
            {
                Data.warrior_hp -= tmp * Time.deltaTime * 25;
                Data.attackTime -= tmp;
                //anim.SetTrigger("Damage");
            }
            player_warrior_hp.value = Data.warrior_hp;

            if (Data.warrior_hp <= 0)
            {
                anim.SetTrigger("Death");
                Data.isLive = false;
            }
            //boss伤害判定
            if (isThreeLevel)
            {
                if (bossAnim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.howl"))
                {
                    broAttack = true;
                    Data.broAttackTime++;
                }
                disToBossBro = Vector3.Distance(boss_bro.transform.position, transform.position);
                if (disToBossBro <= 6 && bossAnim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.standBite"))
                {
                    tmp = Data.broAttackTime;
                    if (tmp > 0)
                    {
                        Data.warrior_hp -= tmp * Time.deltaTime * 70;
                        Data.broAttackTime -= tmp;
                    }
                    else
                    {
                        Data.warrior_hp -= 30 * Time.deltaTime;
                    }
                }
                else if (disToBossBro > 6 && broAttack && bossAnim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.standBite"))
                {
                    Data.broAttackTime = 0;
                    broAttack = false;
                }
            }
            /*转对方向*/
            transform.rotation = new Quaternion(0, MainCamera.rotation.y, 0, MainCamera.rotation.w);
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime / Mathf.Sqrt(2));
                else
                    transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime / Mathf.Sqrt(2));
                else
                    transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                    transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime / Mathf.Sqrt(2));
                else
                    transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                    transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime / Mathf.Sqrt(2));
                else
                    transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime);
            }

            /*走路及待命动作*/
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                anim.SetFloat("Speed", 2f, 1f, Time.deltaTime);
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                anim.SetFloat("Speed", 0);
            }
                
            /*普通攻击*/
            if (Input.GetMouseButtonDown(0))
                anim.SetTrigger("Attack_1");

            if (Input.GetMouseButtonDown(1))
                anim.SetTrigger("Attack_2");

            /* 切换狼模型 */
            Scene scene = SceneManager.GetActiveScene();
            if (Input.GetKeyDown(KeyCode.Q) && scene.name != "TeachScene" && scene.name != "SceneOne")
            {
                //changeWolf();
                GameObject.Destroy(gameObject, 0.1f);
            }
                
            /*跳跃*/
            if (Input.GetKeyDown(KeyCode.Space) && Data.isGrounded)
            {
                Data.isGrounded = false;
                anim.SetTrigger("Jump");
                Vector3 Pos = new Vector3(transform.position.x, transform.position.y + 0.35f, transform.position.z);
                Vector3 Rot = new Vector3(transform.eulerAngles.x - 90, transform.eulerAngles.y, transform.eulerAngles.z);
                //Instantiate(JumpEffect, Pos, Quaternion.Euler(Rot));
                Rig.AddForce(Vector3.up * 10000);
            }
            Physics.gravity = new Vector3(0, -50, 0);
        }
        else if (Data.isLive == false)
        {
            myGameManager.GetComponent<GameManager>().Failed();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "fire")
        {
            Data.warrior_hp -= 40 * Time.deltaTime;
            player_warrior_hp.value = Data.warrior_hp;
            if (Data.warrior_hp <= 0)
            {
                anim.SetTrigger("Death");
                Data.isLive = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Data.isGrounded = true;
    }

    private void changeWolf()
    {
        GameObject new_wolf = (GameObject)Resources.Load("Player_Wolf");
        new_wolf = Instantiate(new_wolf);
        new_wolf.name = "Player_Wolf";
    }
}