using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowController : MonoBehaviour {

  public Color _Color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

  public float volume = 0f;

  [Range(1, 100)]
  public int xSize = 1;
  [Range(1, 100)]
  public int ySize = 1;
  [Range(1, 50)]
  public int resolution = 20;

  public float _Curl = 0.37f;
  public float _CurlFreq = 0.25f;
  public float _CurlDamping = 0.6f;
  public Vector3 _CurlScroll = new Vector3(0.0f, 0.0f, 0.0f);
  public float _Spring = 0.4f;
  public float _Anchor = 0.06f;
  public float _Smooth = 0.6f;
  public float _Damping = 0.6f;

  public Vector3 _Force = new Vector3(0.0f, 0.0f, 0.0f);

  public Vector4 sound = new Vector4(0,0,0,0);

  public ComputeShader computeShader;
  public Material ParticleMaterial;

  private const int computeGroupSize = 128;
  private int computeKernel;
  private int numberOfGroups;
  private int resX;
  private int resY;
  private int resX1;
  private int resY1;

  private ComputeBuffer vertexBuffer;
  private const int vertexStride = 96;
  struct Vertex {
    public Vector3 position;
    public Vector3 anchor;
    public Vector3 velocity;
    public Vector3 curl;
    public Vector3 normal;
    public Vector2 uv;
    public Vector4 sound;
    public float life;
    public float debug;
    public int id;
  };
  private int vertexCount;

  private ComputeBuffer indexBuffer;
  private const int indexStride = 32;
  struct Index {
    public int id0;
    public int id1;
    public int id2;
    public int id3;
    public int id4;
    public int id5;
    public int id6;
    public int id7;
  };
  private int indexCount;

  public int waitTime = 5;
  IEnumerator SetVolumeDelayed() {
    yield return new WaitForSeconds(waitTime);
    volume = 1f;
  }

  void Start() {
    volume = 0f;
    StartCoroutine(SetVolumeDelayed());
    Generate();
  }

  void Generate() {
    Dispose();

    resX = xSize * resolution;
    resY = ySize * resolution;
    resX1 = xSize * resolution + 1;
    resY1 = ySize * resolution + 1;
    vertexCount = resX1 * resY1;
    indexCount = (resX1 + 1) * (resY1 + 1) * 6;

    computeKernel = computeShader.FindKernel("CSMain");
    vertexBuffer = new ComputeBuffer(vertexCount, vertexStride);
    indexBuffer = new ComputeBuffer(indexCount, indexStride);

    Vertex[] vertices = new Vertex[vertexCount];
    for (int x = 0; x < resX1; x++) {
      for (int y = 0; y < resY1; y++) {
        int i = (int)x + y * resX1;
        vertices[i].position = new Vector3( 0f, 0f, 0f );
        vertices[i].anchor = new Vector3( 0f, 0f, 0f );
        vertices[i].velocity = new Vector3( 0f, 0f, 0f );
        vertices[i].curl = new Vector3( 0f, 0f, 0f );
        vertices[i].normal = Vector3.forward;
        vertices[i].uv = new Vector2( (float)x / resX, (float)y / resY );
        vertices[i].sound = new Vector4(0f, 0f, 0f, 0f);
        vertices[i].life = (float)x / resX1;
        vertices[i].debug = 0.0f;
        vertices[i].id = (int)(x + resX1 * y) * 6;
      }
    }
    vertexBuffer.SetData(vertices);

    Index[] indices = new Index[indexCount];
    int t = 0;
    for (int u = 0; u < resX1; u++) {
      for (int v = 0; v < resY1; v++) {
        int vp  = Mathf.Min(( v + 1 ), resY);
        int vpp = Mathf.Min(( v + 2 ), resY);
        int vm  = Mathf.Max(( v - 1 ), 0);
        int up  = Mathf.Min(( u + 1 ), resX);
        int upp = Mathf.Min(( u + 2 ), resX);
        int um  = Mathf.Max(( u - 1 ), 0);

        int _vp  = ( v + 1 );
        int _vpp = ( v + 2 );
        int _vm  = ( v - 1 );
        int _upp = ( u + 2 );
        int _up = ( u + 1 );
        int _um  = ( u - 1 );

        if (_vp > resY) _vp = 0;
        if (_vpp > resY) _vpp = 0;
        if (_vm < 0) _vm = resY;
        if (_up > resX) _up = 0;
        if (_upp > resX) _upp = 0;
        if (_um < 0) _um = resX;

        int a =  u + resX1 * v;
        int b =  u + resX1 * _vp;
        int c = _up + resX1 * _vp;
        int d = _up + resX1 * v;

        int e =  u + resX1 * _vm;
        int f = _um + resX1 * v;
        int g = up + resX1 * _vpp;
        int h = _upp + resX1 * vp;
        int i = _um + resX1 * vp;
        int j =  u + resX1 * _vpp;
        int k = _upp + resX1 * v;
        int l = up + resX1 * _vm;

        indices[t].id0 = a;
        indices[t].id1 = f;
        indices[t].id2 = b;
        indices[t].id3 = d;
        indices[t].id4 = e;
        t++;
        indices[t].id0 = b;
        indices[t].id1 = i;
        indices[t].id2 = j;
        indices[t].id3 = c;
        indices[t].id4 = a;
        t++;
        indices[t].id0 = d;
        indices[t].id1 = a;
        indices[t].id2 = c;
        indices[t].id3 = k;
        indices[t].id4 = l;
        t++;
        indices[t].id0 = b;
        indices[t].id1 = i;
        indices[t].id2 = j;
        indices[t].id3 = c;
        indices[t].id4 = a;
        t++;
        indices[t].id0 = c;
        indices[t].id1 = b;
        indices[t].id2 = g;
        indices[t].id3 = h;
        indices[t].id4 = d;
        t++;
        indices[t].id0 = d;
        indices[t].id1 = a;
        indices[t].id2 = c;
        indices[t].id3 = k;
        indices[t].id4 = l;
        t++;

      }
    }
    indexBuffer.SetData(indices);
  }

  void Dispose() {
    if (vertexBuffer != null) vertexBuffer.Release();
    if (indexBuffer != null) indexBuffer.Release();
    vertexBuffer = null;
    indexBuffer = null;
  }

  void FixedUpdate() {

    if (vertexBuffer == null && indexBuffer == null) {
      Generate();
    } else if (resX != xSize * resolution || resY != ySize * resolution) {
      Generate();
    }
    computeShader.SetBuffer(computeKernel, "indices", indexBuffer);
    computeShader.SetBuffer(computeKernel, "vertices", vertexBuffer);
    computeShader.SetFloat("_Time", Time.time);
    computeShader.SetFloat("_Delta", Time.deltaTime);
    computeShader.SetFloat("_Resolution", resolution * xSize);
    computeShader.SetFloat("_xSize", xSize);
    computeShader.SetFloat("_ySize", ySize);

    computeShader.SetFloat("_Curl", _Curl);
    computeShader.SetFloat("_CurlFreq", _CurlFreq);
    computeShader.SetFloat("_CurlDamping", _CurlDamping);
    computeShader.SetFloat("_Spring", _Spring);
    computeShader.SetFloat("_Anchor", _Anchor);
    computeShader.SetFloat("_Smooth", _Smooth);
    computeShader.SetFloat("_Damping", _Damping);
    computeShader.SetVector("_CurlScroll", _CurlScroll);

    computeShader.SetMatrix("localToWorldMatrix", transform.localToWorldMatrix);
    numberOfGroups = Mathf.CeilToInt((float)vertexCount / computeGroupSize);
    computeShader.Dispatch(computeKernel, numberOfGroups, 1, 1);
  }

  void OnRenderObject() {
    ParticleMaterial.SetPass(0);
    ParticleMaterial.SetBuffer("indices", indexBuffer);
    ParticleMaterial.SetBuffer("vertices", vertexBuffer);
    ParticleMaterial.SetFloat("_Resolution", resolution * xSize);
    ParticleMaterial.SetVector("_Color", _Color);
    // ParticleMaterial.SetFloat("_Time", Time.time);
    ParticleMaterial.SetPass(1);
    Graphics.DrawProcedural(MeshTopology.Triangles, indexCount);
  }

  void OnApplicationQuit() {
    Dispose();
  }
  void OnDestroy() {
    Dispose();
  }
  void OnDisable() {
    Dispose();
  }
  void Reset() {
    Dispose();
  }

}
