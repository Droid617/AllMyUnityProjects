using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private float _boneDistance;
    [SerializeField] private GameObject _bonePrefab;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private GameObject _eatPrefab;
    [Range(0, 100), SerializeField] private float _moveSpeed;
    [Range(0, 20), SerializeField] private float _rotateSpeed;
    float dirX;
    float dirY;
    int reverse = -1;

    private void Start()
    {
        GameObject startFood = Instantiate(_eatPrefab, new Vector3(Random.Range(-80.0f, 80.0f), 2, Random.Range(-80.0f, 80.0f)), Quaternion.identity);
    }

    private void Update()
    {
        MoveHead(_moveSpeed);
        MoveTail();
        Rotate(_rotateSpeed);
    }

    private void MoveHead(float speed)
    {
        
        if((transform.position.x >= 99) || (transform.position.x <= -99))
        {
            transform.position = new Vector3(transform.position.x * reverse, transform.position.y, transform.position.z);
        }
        if ((transform.position.z >= 99) || (transform.position.z <= -99))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z * reverse);
        }
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
    }

    private void MoveTail()
    {
        Vector3 v = transform.position;
        if(_tails.Count > 0)
        {
            _tails.Last().position = v;

            _tails.Insert(0, _tails.Last());
            _tails.RemoveAt(_tails.Count - 1);
        }
    }

    private void Rotate(float speed)
    {
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion target = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Quaternion target = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Quaternion target = Quaternion.Euler(0, 180, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "FOOD")
        {
            Destroy(other.gameObject);

            Vector3 pos = transform.position;
            pos.x += 3;
            pos.z += 3;

            GameObject bone = Instantiate(_bonePrefab, pos, Quaternion.identity);
            _tails.Add(bone.transform);
            GameObject Food = Instantiate(_eatPrefab, new Vector3(Random.Range(-80.0f, 80.0f), 2, Random.Range(-80.0f, 80.0f)), Quaternion.identity);
        }
    }
}
