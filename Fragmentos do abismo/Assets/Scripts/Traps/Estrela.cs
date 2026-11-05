using UnityEngine;

public class Estrela : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private Transform targetA;
    [SerializeField] private Transform targetB;
     
     private void Update()
    {
        if(currentTarget == null || targetA == null || targetB == null)
        {
            return;
        }
        else
        {
            Move();
        }
    }
    
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if(currentTarget == targetA && transform.position.x == targetA.position.x)
        {
            currentTarget = targetB;
        }
        else if(currentTarget == targetB && transform.position.x == targetB.position.x)
        {
            currentTarget = targetA;
        }
    }
}
