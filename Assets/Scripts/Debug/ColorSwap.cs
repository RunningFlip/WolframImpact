using UnityEngine;


public class ColorSwap : MonoBehaviour
{
    public float timeStep;
    public Material[] mat;
    public MeshRenderer[] meshRenderer;

    //Iterate
    private float passedTime = Mathf.Infinity;
    private int index;


    public void Update()
    {
        if (passedTime >= timeStep)
        {
            passedTime = 0;

            for (int i = 0; i < meshRenderer.Length; i++)
            {
                meshRenderer[i].material = mat[index];
            }
            index++;
            if (index == mat.Length) index = 0;
        }
        else
        {
            passedTime += Time.deltaTime;
        }
    }
}
