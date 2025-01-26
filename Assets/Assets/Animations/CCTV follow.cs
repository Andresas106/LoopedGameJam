using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVfollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform rotatingHorizontalPart;

    [SerializeField]
    float rotationSpeed = 0.3f;

    bool followPlayer;

    [SerializeField]
    bool instaLock = true;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Player"))
        {
            player = GameObject.Find("Player").transform;
        }

        if (instaLock && player != null)
        {
            rotatingHorizontalPart.LookAt(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer && player != null)
        {
            Vector3 targetDirection = player.position - new Vector3(
                rotatingHorizontalPart.position.x,
                rotatingHorizontalPart.position.y,
                rotatingHorizontalPart.position.z);

            float singleStep = rotationSpeed * Time.deltaTime;

            Vector3 newDirection = Vector3.RotateTowards(
                rotatingHorizontalPart.forward,
                targetDirection,
                singleStep,
                0.0f);

            rotatingHorizontalPart.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            followPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            followPlayer = false;
        }
    }
}
