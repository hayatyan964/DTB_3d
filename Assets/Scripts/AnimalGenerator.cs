using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AnimalGenerator : MonoBehaviour
{
    public GameObject[] animals;
    [SerializeField]
    private float generateHeight = 3;
    private GameObject generatedAnimal;
    private Rigidbody animalsRigid;

    [SerializeField]
    private float rotationSpeed = 0.25f;

    [SerializeField]
    private GameObject _inputController;
    private InputManager _input;

    [SerializeField]
    private GameObject _mainCamera;

    [SerializeField]
    private float MoveSens = 0.01f;

    private void Start()
    {
        _input = _inputController.GetComponent<InputManager>();
    }

    public void PreparAnimal()
    {
        if (generatedAnimal == null)
        {
            int random = Random.Range(0, animals.Length);
            generatedAnimal = Instantiate(animals[random],
                transform.position + Vector3.up * generateHeight,
                Quaternion.identity);
            animalsRigid = generatedAnimal.GetComponent<Rigidbody>();
            animalsRigid.isKinematic = true;
        }
    }

    public void MoveAnimal()
    {
        if(generatedAnimal != null)
        {
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            Vector3 cameraForward = _mainCamera.transform.forward;
            cameraForward.y = 0f;
            cameraForward.Normalize();
            Vector3 cameraRight = _mainCamera.transform.right;
            cameraRight.y = 0f;
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * inputDirection.z + cameraRight * inputDirection.x;
            generatedAnimal.transform.position += moveDirection * MoveSens;
        }
    }

    public void RotateAnimal()
    {
        if(generatedAnimal != null)
        {
            Vector3 rotation = new Vector3(_input.move.y, _input.move.x, 0) * rotationSpeed;
            generatedAnimal.transform.Rotate(rotation);
        }
    }

    public void DropAnimal()
    {
        animalsRigid.isKinematic = false;
        generatedAnimal = null;
    }
}
