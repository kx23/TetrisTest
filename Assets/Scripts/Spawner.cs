using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject nextTetramino;
    public GameObject[] Tetraminos;
    public Transform Grid;
    public Transform NextTetraminoPos;


    private void Awake()
    {
        nextTetramino = Instantiate(Tetraminos[Random.Range(0, Tetraminos.Length)],
                        NextTetraminoPos.position,
                        Quaternion.identity, NextTetraminoPos); 
    }
    public Tetramino SpawnNewTetramino()
    {
        
        var t = nextTetramino;
        t.transform.parent = Grid;
        t.transform.SetPositionAndRotation(transform.position,transform.rotation);
        t.transform.localScale = transform.localScale;
        nextTetramino = Instantiate(Tetraminos[Random.Range(0, Tetraminos.Length)],
                        NextTetraminoPos.position,
                        Quaternion.identity, NextTetraminoPos);
        return t.GetComponent<Tetramino>();
    }
}
