using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody B_Rb;
    [SerializeField]
    private float speed = 15;
    private bool isTravelling;
    private Vector3 TravelDir;
    private Vector3 NextColisionPosition;

    public int minSwipeRecognition = 500;
    private Vector2 SwipePositionLastFrame;
    private Vector2 SwipePositionCurrentFrame;
    private Vector2 CurrentSwipe;

    private Color solvedColor;

    public AudioSource playerAudio;
    public AudioClip HitWall;
    private bool PlayAudioClip = false;

    public ParticleSystem pHitWall;

    private void Start()
    {
        B_Rb = GetComponent<Rigidbody>();
        solvedColor = Random.ColorHSV(0.5f,  1f);
        GetComponent<MeshRenderer>().material.color = solvedColor;
        pHitWall.startColor = solvedColor;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("WallTag"))
        {
            PlayAudioClip = true;
            if (PlayAudioClip)
            {
                playerAudio.PlayOneShot(HitWall, 1.0f);
                pHitWall.Play();
            }
        }
    }

    private void FixedUpdate()
    {
       
        if (isTravelling)//by default isTravelling wld be false that is why the ball doesnt run immediately the game is started
        {
            B_Rb.velocity = speed * TravelDir ;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f );
        int  i = 0;
        while (i < hitColliders .Length )
        {
            GroundPiece ground = hitColliders[i].transform.GetComponent<GroundPiece>();
            if(ground && !ground .isColored)
            {
                ground.ColorChange(solvedColor);
            }
            i++;
        }

        if (NextColisionPosition != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, NextColisionPosition) < 1) //0 is less than one so this will always be true hit.point = NextColisionposition and that is equal to transform.position
            {
                isTravelling = false;
                TravelDir = Vector3.zero;
                NextColisionPosition = Vector3.zero;
            }

        }

        if (isTravelling)//If isTravelling is true the remaing part of this block wont be executed viceVersa
            return;

        if (Input.GetMouseButton(0))
        {
            SwipePositionCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y); //Gets the current position of the mouse immediately it is clicked

            if (SwipePositionLastFrame != Vector2.zero)//by default SwipePositionLastFame = Vector2.zero, 
            {
                CurrentSwipe = SwipePositionCurrentFrame - SwipePositionLastFrame;

                if (CurrentSwipe.sqrMagnitude < minSwipeRecognition)
                {
                    return;
                }

                CurrentSwipe.Normalize();

                if (CurrentSwipe.x > -0.5f && CurrentSwipe.x < 0.5)
                {
                    SetDestination(CurrentSwipe.y > 0 ? Vector3.forward : Vector3.back);
                }

                if (CurrentSwipe.y > -0.5f && CurrentSwipe.y <0.5)
                {
                    SetDestination(CurrentSwipe.x > 0 ? Vector3.right : Vector3.left);
                }
            }
            SwipePositionLastFrame = SwipePositionCurrentFrame;//changes the default value of SPLastFrame to that of SPCurrentFrame
        }

        if(Input.GetMouseButtonUp(0))
        {
            SwipePositionLastFrame = Vector2.zero;
            CurrentSwipe = Vector2.zero;
        }
        pHitWall.transform.position = transform.position;
    }

 

    private void SetDestination(Vector3 Direction)
    {
        TravelDir = Direction;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Direction, out hit, 100f))
        {
            NextColisionPosition = hit.point;//makes second if statement true
        }
        isTravelling = true;
    }
}
