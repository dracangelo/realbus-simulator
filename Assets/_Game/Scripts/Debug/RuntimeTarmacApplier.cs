using UnityEngine;

public class RuntimeTarmacApplier : MonoBehaviour
{
    [Header("Material")]
    public Color tarmacColor = new Color(0.08f, 0.08f, 0.08f, 1f);
    public float metallic = 0f;
    public float smoothness = 0.15f;
    public Vector2 textureScale = new Vector2(6f, 6f);

    [Header("Targets")]
    public string roadsRootName = "OSM_Roads";
    public bool applyToPlane = true;
    public string planeObjectName = "Plane";
    public bool applyToRoadMeshes = true;

    Material runtimeMaterial;

    void Start()
    {
        ApplyTarmac();
    }

    public void ApplyTarmac()
    {
        if (runtimeMaterial == null)
            runtimeMaterial = CreateTarmacMaterial();

        if (applyToPlane)
            ApplyToPlane();

        if (applyToRoadMeshes)
            ApplyToRoads();
    }

    void ApplyToPlane()
    {
        var plane = GameObject.Find(planeObjectName);
        if (plane == null) return;
        var renderer = plane.GetComponent<Renderer>();
        if (renderer == null) return;
        renderer.material = runtimeMaterial;
    }

    void ApplyToRoads()
    {
        var roadsRoot = GameObject.Find(roadsRootName);
        if (roadsRoot == null) return;
        var renderers = roadsRoot.GetComponentsInChildren<MeshRenderer>(true);
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = runtimeMaterial;
    }

    Material CreateTarmacMaterial()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        if (shader == null) shader = Shader.Find("Unlit/Color");
        var mat = new Material(shader);
        mat.color = tarmacColor;
        if (mat.HasProperty("_Metallic")) mat.SetFloat("_Metallic", metallic);
        if (mat.HasProperty("_Glossiness")) mat.SetFloat("_Glossiness", smoothness);
        if (mat.HasProperty("_Smoothness")) mat.SetFloat("_Smoothness", smoothness);
        if (mat.HasProperty("_MainTex")) mat.SetTextureScale("_MainTex", textureScale);
        return mat;
    }
}
