using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BattleTransitions : MonoBehaviour
{
    [SerializeField] GameObject squareSnakes;
    [SerializeField] Image squareExtend;

    public IEnumerator SquareSnakes()
    {
        squareSnakes.SetActive(true);
        Animator anim = squareSnakes.GetComponent<Animator>();
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        yield return StartCoroutine(ExtendSquares());
    }

    IEnumerator ExtendSquares()
    {
        Vector3 start1 = new Vector3(-224, 160), start2 = new Vector3(224, -160);
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                var block1 = Instantiate(squareExtend, start1, Quaternion.identity, squareSnakes.transform);
                block1.transform.localPosition = start1;
                var block2 = Instantiate(squareExtend, start2, Quaternion.identity, squareSnakes.transform);
                block2.transform.localPosition = start2;
                yield return new WaitForSeconds(0.1f);
                start1 += new Vector3(64 * (-2 * (i % 2) + 1), 0);
                start2 += new Vector3(64 * (2 * (i % 2) - 1), 0);
            }
            start1 += new Vector3(64 * (2 * (i % 2) - 1), -64);
            start2 += new Vector3(64 * (-2 * (i % 2) + 1), 64);
        }
        yield return new WaitForSeconds(0.2f);
    }
}
