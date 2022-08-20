using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float NumberOfTrails = 5f;
    public float TrailLifespan = 0.3f;

    private Coroutine TrailCoroutine;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    public Transform spawnPosition;
    public Material mat;
    public string shaderVarRef;

    public Material[] newMaterials;

    public void ActivateTrail(float activeTime)
    {
        TrailCoroutine = StartCoroutine(ActivateTrailCo(activeTime));
    }

    IEnumerator ActivateTrailCo(float activeTime)
    {
        for (int i = 0; i < NumberOfTrails; i++)
        {
            if (skinnedMeshRenderers == null)
            {
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            for (int j = 0; j < skinnedMeshRenderers.Length; j++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[j].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.materials = newMaterials;
                mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                StartCoroutine(AnimateMaterialFloat(mr.materials, TrailLifespan));

                Destroy(gObj, TrailLifespan);
            }

            yield return new WaitForSeconds(activeTime / NumberOfTrails);
        }
    }

    IEnumerator AnimateMaterialFloat(Material[] mat, float duration)
    {
        float counter = mat[0].GetFloat(shaderVarRef) * duration;

        while (counter >= 0)
        {
            counter -= Time.deltaTime;
            foreach (var item in mat)
            {
                item.SetFloat(shaderVarRef, counter/duration);
            }
            yield return null;
        }
    }
}