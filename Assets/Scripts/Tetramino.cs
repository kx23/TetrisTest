using UnityEngine;

public class Tetramino : MonoBehaviour
{
    private void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject); // ���������� ������ ���� � ���� �� �������� �����
        }
    }
}
