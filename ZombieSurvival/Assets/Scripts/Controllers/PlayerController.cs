using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace GameClient.Controllers
{
    class PlayerController : MonoBehaviour
    {
        public static string PlayerName;
        public float speed = 2f;
        public float jumpVel = 4f;
        public float onGroundDist = 1.01f;
        public float sensitivity = 1f;
        private new Rigidbody rigidbody;
        private PlayerState ps;
        public new Camera camera;
        private GameObject deathCam;
        public AudioClip hurt;
        public AudioClip reload;
        public AudioSource audiosur;
        bool esc;


        private static byte myID = 0xFF;
        public static byte MyID
        {
            get { return myID; }
            set
            {
                myID = value;
            }
        }
        public static PlayerController instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Debug.Log("only one instance should exist");
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            ps = new PlayerState(myID);
            Reload();
            updateHP(0);
            deathCam = GameObject.FindGameObjectWithTag("DeathCam");
            deathCam.SetActive(false);
            GameUIController.Instance.playerDead = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float f = 0, r = 0;
            if (Input.GetKey(KeyCode.W))
                f++;
            if (Input.GetKey(KeyCode.S))
                f--;
            if (Input.GetKey(KeyCode.A))
                r--;
            if (Input.GetKey(KeyCode.D))
                r++;

            if (Input.GetKey(KeyCode.Space))
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), onGroundDist))
                {
                    rigidbody.velocity = new Vector3(0, jumpVel, 0);
                }
            }

            Vector3 v = new Vector3(r, 0, f);
            v = Quaternion.AngleAxis(camera.transform.rotation.eulerAngles.y, Vector3.up) * v;
            rigidbody.position = Vector3.MoveTowards(rigidbody.position, v.normalized + rigidbody.position, Time.fixedDeltaTime * speed);
            ps.position.xMov = v.normalized.x;
            ps.position.zMov = v.normalized.z;

            f = 0;
            r = 0;
            if (Input.GetKey(KeyCode.UpArrow))
                f++;
            if (Input.GetKey(KeyCode.DownArrow))
                f--;
            if (Input.GetKey(KeyCode.LeftArrow))
                r--;
            if (Input.GetKey(KeyCode.RightArrow))
                r++;

            r += sensitivity * Input.GetAxis("Mouse X");
            f += sensitivity * Input.GetAxis("Mouse Y");

            transform.eulerAngles = transform.eulerAngles + new Vector3(0f, 90f * r * Time.fixedDeltaTime, 0f);
            Vector3 udDir = camera.transform.eulerAngles + new Vector3(-90f * f * Time.fixedDeltaTime, 0f, 0f);
            float d = udDir.x;
            if (d > 180)
                d = Mathf.Clamp(d, 270f, 360f);
            else
                d = Mathf.Clamp(d, -1f, 90f);
            camera.transform.eulerAngles = new Vector3(d, udDir.y, udDir.z);


            ps.position.x = transform.position.x;
            ps.position.y = transform.position.y;
            ps.position.z = transform.position.z;
            ps.position.yRot = transform.rotation.eulerAngles.y;
        }

        private float hpRegen = 0;
        private float sendDataTimer = 0.1f;
        private void Update()
        {
            hpRegen += 2 * Time.deltaTime;
            if (hpRegen >= 1) {
                updateHP(1);
                hpRegen--;
            }

            if (Input.GetKeyDown(KeyCode.R))
                Reload();

            sendDataTimer -= Time.deltaTime;
            if (sendDataTimer < 0)
            {
                GameController.instance.outgoingQueue.Enqueue(ps);
                sendDataTimer = 0.1f;
            }
        }

        public void updateHP(int healthChange)
        {
            ps.Hp = Mathf.Clamp(ps.Hp + healthChange, 0, 100);

            if (ps.Hp <= 0)
            {
                camera.gameObject.SetActive(false);
                deathCam.SetActive(true);
                GameController.instance.outgoingQueue.Enqueue(new PlayerDead(ps.playerId));
                GameController.instance.queue.Enqueue(new Respawn());
                GameUIController.Instance.playerDead = true;
                Destroy(gameObject);
            }
            GameUIController.Instance.setHPPercent(ps.Hp / 100f);

            if (healthChange < 0)
                audiosur.PlayOneShot(hurt,0.7f);
        }

        private void Reload() 
        {
            ps.Ammo = 18;
            GameUIController.Instance.setAmmoAmt(ps.Ammo, 18);
            audiosur.PlayOneShot(reload,0.7f);
        }

        public bool shoot() 
        {
            if (ps.Ammo > 0) 
            {
                ps.Ammo--;
                GameUIController.Instance.setAmmoAmt(ps.Ammo, 18);
                return true;
            }
            return false;
        }
    }
}