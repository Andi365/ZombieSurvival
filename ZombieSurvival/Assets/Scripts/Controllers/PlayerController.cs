using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace GameClient.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 2f;
        public float jumpVel = 4f;
        public float onGroundDist = 1.01f;
        public float sensitivity = 1f;
        private new Rigidbody rigidbody;
        private PlayerState ps;
        public new Camera camera;
        bool esc;
        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            ps = new PlayerState();
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
                Zombie zom = new Zombie(0, 1);
                SpawnController spawn = new SpawnController();
                spawn.spawnEnemy(zom);
            }
        }

        public void updateHP(int damageTaken)
        {
            ps.Hp = ps.Hp - damageTaken;

            if (ps.Hp <= 0)
            {
                //DIE
            }
        }
    }
}