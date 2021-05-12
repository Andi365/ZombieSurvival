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
        public Camera deathCam;
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

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                esc = !esc;

                if (esc)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            if (Input.GetKey(KeyCode.H))
            {
                ZombieSpawn zom = new ZombieSpawn(0, 1);
                SpawnController spawn = new SpawnController();
                spawn.spawnEnemy(zom);
            }

            ps.position.x = transform.position.x;
            ps.position.y = transform.position.y;
            ps.position.z = transform.position.z;
            ps.position.yRot = transform.rotation.eulerAngles.y;
        }

        float sendDataTimer = 0.1f;
        private void Update() {
            sendDataTimer -= Time.deltaTime;
            if (sendDataTimer < 0) 
            {
                GameController.instance.outgoingQueue.Enqueue(ps);
                sendDataTimer = 0.1f;
            }
        }

        public void updateHP(int healthChange)
        {
            ps.Hp = ps.Hp + healthChange;
            
            if (ps.Hp <= 0)
            {
                camera.gameObject.SetActive(false);
                deathCam.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}