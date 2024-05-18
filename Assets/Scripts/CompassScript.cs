using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject coin;
    [SerializeField]
    GameObject character;
    [SerializeField]
    Image arrow;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (arrow == null) return;
        if (coin == null) return;
        if (character == null) return;

        arrow.transform.eulerAngles = new Vector3(0, 0,
        Vector3.SignedAngle(
            (coin.transform.position - character.transform.position),
            character.transform.forward,
            Vector3.up
            )
        );

    }
}
