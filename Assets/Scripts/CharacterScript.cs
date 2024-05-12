using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    private CharacterController characterController;
    private float spead = 30.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            dx *= 2;
            dy *= 2;
        }
        // new Vector3(dx, 0, dy) - по Світових координатах: Х - завжди по "клітинках"
        // вимагається - рух у відповідності до повороту камери
        // осі камери задаються векторами forward та right
        characterController.SimpleMove(
            Camera.main.transform.forward * spead * dy +
            Camera.main.transform.right * spead * dx);

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
