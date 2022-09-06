using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float moveSpeed;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float jumpPower;
    [SerializeField] private bool canJump;
    [SerializeField] private LineRenderer gunshotPath;
    [SerializeField] private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody> ();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis ("Horizontal");
        verticalInput = Mathf.Round(Input.GetAxisRaw ("Vertical"));

        if (Input.GetButtonDown ("Jump") && canJump) {
            Jump ();
        }

        if(Input.GetMouseButtonDown(0) && canShoot) {
            canShoot = false;
            Vector3 aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //normally for a calculation like this, you need to feed a converted z value in, but...
            aimPosition.z = 0; //simply setting the z value back to 0 after the conversion works as long as the camera looks along the z axis
            Vector3 aimDirection = aimPosition - transform.position;
            Ray gunshot = new Ray (transform.position, aimDirection);
            RaycastHit hit;
            gunshotPath.gameObject.SetActive (true);
            gunshotPath.SetPosition (0, transform.position);

            if (Physics.Raycast(gunshot, out hit, 50f)) {
                gunshotPath.SetPosition (1, hit.point);
                StartCoroutine (RemoveGunshotPath ());
                if(hit.collider.gameObject.CompareTag("Enemy")) {
                    Destroy (hit.collider.gameObject);
                }
            } else {
                gunshotPath.SetPosition (1, gunshot.GetPoint (50f));
                StartCoroutine (RemoveGunshotPath ());
            }
        }
    }

    IEnumerator RemoveGunshotPath() {
        yield return new WaitForSeconds (0.1f);
        gunshotPath.gameObject.SetActive (false);
        canShoot = true;
    }

    private void FixedUpdate() {
        playerRb.transform.Translate (Vector3.right * horizontalInput * moveSpeed);
    }

    private void Jump() {
        playerRb.AddForce (Vector3.up * jumpPower, ForceMode.Impulse);
        canJump = false;
    }

    private void OnCollisionEnter(Collision collision) {
        //If the player collides with a block that is at their feet, the player should be able to jump
        if(collision.gameObject.CompareTag("Block") && collision.gameObject.transform.position.y < (transform.position.y - (transform.localScale.y * 0.95f))) {
            canJump = true;
        }
        //temporary code for allowing the same interaction with the Ground gameObject
        if(collision.gameObject.CompareTag("Ground")) {
            canJump = true;
        }
    }
}
