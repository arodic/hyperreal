
struct Vertex {
  float3 position;
  float3 anchor;
  float3 velocity;
  float3 curl;
  float3 normal;
  float2 uv;
  float4 sound;
  float life;
  float debug;
  int id;
};
struct Index {
  int id0;
  int id1;
  int id2;
  int id3;
  int id4;
  int id5;
  int id6;
  int id7;
};

StructuredBuffer<Vertex> vertices;
StructuredBuffer<Index> indices;

#include "snoise.cginc"

struct v2f {
  float4 pos: SV_POSITION;
  float3 normalDir: TEXCOORD0;
  float2 uv: TEXCOORD1;
  float4 posWorld : TEXCOORD2;
  float2 viewNormal : TEXCOORD3;
  float life: TEXCOORD4;
  float4 sound : TEXCOORD5;
  float debug: TEXCOORD6;
  float4 screenUV : TEXCOORD7;

};

v2f vert(uint id : SV_VertexID) {
  v2f o;
  Index i = indices[id];
  o.uv = vertices[i.id0].uv;
  o.life = vertices[i.id0].life;
  o.sound = vertices[i.id0].sound;
  o.debug = vertices[i.id0].debug;

  float3 pos = vertices[i.id0].position;
  o.posWorld = float4(pos, 1.0);
  o.pos = UnityObjectToClipPos(o.posWorld);
  o.screenUV = ComputeGrabScreenPos(o.pos);
  o.normalDir = UnityObjectToWorldNormal(vertices[i.id0].normal);
  o.viewNormal = normalize(mul((float3x3)UNITY_MATRIX_MV, o.normalDir));
  return o;
}

[maxvertexcount(3)]
void geom(triangle v2f input[3], inout TriangleStream<v2f> OutputStream)
{
  v2f g0 = input[0];
  v2f g1 = input[1];
  v2f g2 = input[2];
  if ((g0.debug + g1.debug + g2.debug) < 0.5) return;
  if (distance(g0.pos, g1.pos) > 10.0) return;
  if (distance(g1.pos, g2.pos) > 10.0) return;
  OutputStream.Append(g0);
  OutputStream.Append(g1);
  OutputStream.Append(g2);
}
