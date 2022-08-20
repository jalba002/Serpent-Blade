using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTrail : MonoBehaviour
{
    public float ActiveTime = 2f;

    public float MeshRefreshRate = 0.1f;
    public float MeshDestroyDelay = 0.3f;

    private bool isTrailActive = false;
    private Coroutine TrailCoroutine;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    public Transform spawnPosition;
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.01f;

    public Material[] newMaterials;

    public void ActivateTrail()
    {
        isTrailActive = true;
        TrailCoroutine = StartCoroutine(ActivateTrailCo());
    }

    public void DeactivateTrail()
    {
        isTrailActive = false;
        StopCoroutine(TrailCoroutine);
    }

    IEnumerator ActivateTrailCo()
    {
        while (isTrailActive)
        {
            if (skinnedMeshRenderers == null)
            {
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
            }

            for (int i = 0; i < skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(spawnPosition.position, spawnPosition.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.materials = newMaterials;
                mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

                StartCoroutine(AnimateMaterialFloat(mr.materials, MeshDestroyDelay));

                Destroy(gObj, MeshDestroyDelay);
            }

            yield return new WaitForSeconds(MeshRefreshRate);
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