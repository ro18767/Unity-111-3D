using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 pos = other.gameObject.transform.position;
            playerPos.Set(pos.x, pos.y, pos.z);
            Debug.Log("OnTriggerEnter");
            animator.SetBool("IsCollected", true);
            
        }
    }

    readonly Vector3 playerPos = new Vector3();

    public void OnDisapear() //
    {
        /* д.з. забезпечити випадкове переміщення монети:
         * - не ближче ніж 20 одиниць відстані від початкового положення
         * - не далі 40
         * - не ближче 100 до країв terrain
         * додати анімацію рук для кліпу "рух вперед"
         */

        Debug.Log("OnDisapear");

        const float terrainSideSize = 1000.0f;
        const float distanceToEdges = 100.0f;
        const float minDistanceToPlayer = 20.0f;
        const float maxDistanceToPlayer = 40.0f;

        float minX = 0.0f + distanceToEdges;
        float minZ = 0.0f + distanceToEdges;

        float maxX = terrainSideSize - distanceToEdges;
        float maxZ = terrainSideSize - distanceToEdges;


        Vector3 offset = new Vector3(
            Random.Range(-(maxDistanceToPlayer - minDistanceToPlayer), (maxDistanceToPlayer - minDistanceToPlayer)),
            0.0f,
            Random.Range(-(maxDistanceToPlayer - minDistanceToPlayer), (maxDistanceToPlayer - minDistanceToPlayer))
            );

        if (offset.x >= 0.0f)
        {
            offset.x += maxDistanceToPlayer - minDistanceToPlayer;
        }
        if (offset.x <= 0.0f)
        {
            offset.x -= maxDistanceToPlayer - minDistanceToPlayer;
        }
        if (offset.z >= 0.0f)
        {
            offset.z += maxDistanceToPlayer - minDistanceToPlayer;
        }
        if (offset.z <= 0.0f)
        {
            offset.z -= maxDistanceToPlayer - minDistanceToPlayer;
        }

        offset.x = Mathf.Max(offset.x, minX);
        offset.x = Mathf.Min(offset.x, maxX);

        offset.z = Mathf.Max(offset.z, minZ);
        offset.z = Mathf.Min(offset.z, maxZ);

        Vector3 newPosition = transform.position + offset;
        newPosition.y = Terrain.activeTerrain.SampleHeight(newPosition) + 1.5f;

        
        transform.position = newPosition;
        animator.SetBool("IsCollected", false);


    }
}
