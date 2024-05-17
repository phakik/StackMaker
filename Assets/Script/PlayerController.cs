using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] LayerMask layer;
    [SerializeField] public GameObject StartPoint;
    [SerializeField] public GameObject EndPoint;
    [SerializeField] float _speed;
    private List<GameObject> pickedBrick;
    [SerializeField] private GameObject playerAva;
    [SerializeField] private GameObject brickAva;
    [SerializeField] GameObject win;
    Vector3 mousePosDown;
    bool isMoving;
    Vector3 direct;
    public Vector3 lastPos;


    // Start is called before the first frame update
    void Start()
    {
      
        OnInit();
    }
    void OnInit()
    {
        this.transform.position = StartPoint.transform.position + (Vector3.up * 1.1f);
        lastPos = transform.position;
        playerAva.transform.localPosition = new Vector3(0, -0.9f, 0);
        pickedBrick = new List<GameObject>();
        pickedBrick.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
            UserInput();
            Move(lastPos);
        

    }
    private void Move(Vector3 lastPos)
    {
        if (pickedBrick.Count == 0)
        {
            transform.position += Vector3.zero;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, lastPos, _speed * Time.deltaTime);
        }
    }
    void CheckMoving()
    {
        if (Vector3.Distance(transform.position, lastPos) <= 0.1f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }
    void UserInput()
    {
        CheckMoving();
        if (isMoving)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            mousePosDown = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            direct = Input.mousePosition - mousePosDown;
            if (direct.magnitude > 500f)
            {

                direct = direct.normalized;
                if (Mathf.Abs(direct.x) > Mathf.Abs(direct.y))
                {
                    if (direct.x > 0)
                    {
                        direct = Vector3.right;
                    }
                    else if (direct.x < 0)
                    {
                        direct = Vector3.left;
                    }
                }
                else
                {
                    if (direct.y > 0)
                    {
                        direct = Vector3.forward;
                    }
                    else if (direct.y < 0)
                    {
                        direct = Vector3.back;
                    }
                }
                lastPos = GetEndPosition(direct);
                return;
            }
            else
            {
                return;
            }

        }
    }

    /*shoot tia ray len 1 buoc theo huong chi dinh, check tag cua collider tia ray ban trung xem co phai ground khong, neu dung thi tiep tuc
     * tien len 1 buoc va lam dieu tuong tu
    */
    Vector3 GetEndPosition(Vector3 direction)
    {

        RaycastHit hit;
        Vector3 nextMove = transform.position;
        while (Physics.Raycast(nextMove, Vector3.down, out hit, 100, layer))
        {
            Debug.DrawRay(nextMove, Vector3.down * hit.distance, Color.red, 10000);

            if (hit.collider.CompareTag("Road"))
            {
                nextMove += direction;
            }
            else
            {
                break;
            }
        }
        return nextMove - direct;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(CONSTANTS.LASTBRICK))
        {
            Instantiate(win);
            win.SetActive(true);
        }
        if (collision.gameObject.CompareTag(CONSTANTS.BRICK))
        {
            playerAva.transform.position += new Vector3(0, 0.2f, 0);
            GameObject playerBrick = Instantiate(brickAva);
            playerBrick.transform.SetParent(this.gameObject.transform);
            playerBrick.transform.localPosition = new Vector3(0, -1.1f, 0);
            playerBrick.transform.rotation = Quaternion.Euler(-90, 0, 0);
            playerBrick.transform.localPosition = playerAva.transform.localPosition - new Vector3(0, 0.1f, 0);
            StackBrick(playerBrick);
            collision.gameObject.tag = CONSTANTS.UNTAGGED;
        }
        else if (collision.gameObject.CompareTag(CONSTANTS.BLANKBRICK))
        {
            playerAva.transform.position -= new Vector3(0, 0.2f, 0);
            RemoveBrick();
            collision.gameObject.tag = CONSTANTS.UNTAGGED;
            Debug.Log(pickedBrick.Count);
        }
    }
    private void StackBrick(GameObject brick)
    {
        pickedBrick.Add(brick);
    }
    private void RemoveBrick()
    {
        if (pickedBrick.Count >= 1)
        {
            int lastElement = pickedBrick.Count - 1;
            Destroy(pickedBrick[lastElement]);
            pickedBrick.RemoveAt(pickedBrick.Count - 1);

        }
    }
}
