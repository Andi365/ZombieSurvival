using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameClient.Controllers;
using Data;

namespace GameClient.AI
{
    public class RaycastShoot : MonoBehaviour
    {
        //Damage done by weapon
        public int gunDamage = 25;
        // Delay on when you can shoot again
        public float fireRate = 0.25f;
        // how far the ray will be shot
        public float weaponRange = 50f;
        // If a raycast intersect an object with a rigidbody component it will be applied force.
        public float hitForce = 100f;
        // Where the gunline will begin
        public Transform muzzle;
        private Camera fpsCam;
        // how long the "bullet" will remain visabl after a shot
        private WaitForSeconds shotDuration = new WaitForSeconds(1f);
        //sound of the gun shooting
        private AudioSource gunSound;
        // two points to draw line between them in game view
        private LineRenderer gunLine;
        // how long time you have to wait before you can fire again
        private float nextFire;

        void Start()
        {
            gunLine = GetComponent<LineRenderer>();
            gunSound = GetComponent<AudioSource>();
            fpsCam = PlayerController.instance.camera;
            gunLine.startWidth = 0.05f;
            gunLine.endWidth = 0.05f;
        }

        void Update()
        {
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                if (PlayerController.instance.shoot())
                {
                    // make sure we cant shoot before time have exceeded.  
                    nextFire = Time.time + fireRate;
                    StartCoroutine(ShotEffect());

                    RaycastHit hit;
                    // to render a gunline we have to have two points in space to do so.
                    // the first point in space is out gun muzzle
                    gunLine.SetPosition(0, muzzle.position);

                    // if we hit something with out gun
                    if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
                    {
                        //set this hit position to our second point in space
                        gunLine.SetPosition(1, hit.point);

                        NPCScript zombie = hit.collider.GetComponent<NPCScript>();

                        if (zombie != null)
                        {
                            zombie.Damage(gunDamage);
                            GameController.instance.outgoingQueue.Enqueue(new ZombieHit(zombie.zombie.Id, gunDamage));
                        }
                    }
                    else
                    {
                        //else set a point weaponrange out from the muzzle
                        gunLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));

                    }
                }
            }

            //Draws a line so we can see the line we shoot in for debugging
            Debug.DrawRay(rayOrigin, fpsCam.transform.forward * weaponRange, Color.cyan);
        }

        private IEnumerator ShotEffect()
        {
            gunSound.Play();
            gunLine.enabled = true;
            yield return shotDuration;
            gunLine.enabled = false;
        }
    }
}
