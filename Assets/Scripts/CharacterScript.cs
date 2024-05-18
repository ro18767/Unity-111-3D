using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private CharacterController characterController;
    private float speed = 3f;

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
       int moveState = 0;
        float dx = Input.GetAxis("Horizontal") * speed;
        float dy = Input.GetAxis("Vertical") * speed;        
        // new Vector3(dx, 0, dy) - по Світових координатах: Х - завжди по "клітинках"
        // вимагається - рух у відповідності до повороту камери
        // осі камери задаються векторами forward та right
        Vector3 step =
            Camera.main.transform.forward * dy +
            Camera.main.transform.right * dx;
        if(step.magnitude < 0.01f)
        {
            moveState = 0;
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            step *= 2f;
            moveState = 2;  // RunForward

            if (dx > 0.4f)
            {
                moveState = 5;  // RunRight
            }
            if (dx < -0.4f)
            {
                moveState = 4;  // Runleft
            }

        }
        else
        {
            moveState = 1;
        }
        characterController.SimpleMove(step);
        animator.SetInteger("State", moveState);

        // повертаємо персонаж поглядом у напрямі камери
        Vector3 f = Camera.main.transform.forward;  // вектор камери може бути нахиленим
        f.y = 0f;   // проєкція на горизонтальну площину
        f = f.normalized;  // призводимо до довжини = 1
        this.transform.forward = f;
    }
}
/* Д.З. Обмежити кути вертикального повороту
 * камери (через управління мишою)
 * орієнтовні кути: від 25..30 (униз) до -35..-40 (угору)
 * по горизонталі - без обмежень, але якщо кут збільшується 
 * понад 360, то корегувати - віднімати 360. Аналогічно якщо
 * менше -360
 */
