﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPreview : MonoBehaviour
{
    public Material blockmat;
    float TextureAtlasSize = Chunk.TextureAtlasSize;
    float UVBleedCompromise = Chunk.UVBleedCompromise;
    List<Vector3> verts;
    List<int> tris;
    List<Vector2> uvs;

    public GameObject GenerateBlockMesh(Block block)
    {
        //Stopwatch sw = new Stopwatch();
        //sw.Start();

        int faceCount = 0;
        verts = new List<Vector3>(98304);
        tris = new List<int>(147456);
        uvs = new List<Vector2>(98304);

        // top

        verts.Add(new Vector3(0, 0 + 1, 0));
        verts.Add(new Vector3(0, 0 + 1, 0 + 1));
        verts.Add(new Vector3(0 + 1, 0 + 1, 0 + 1));
        verts.Add(new Vector3(0 + 1, 0 + 1, 0));
        AddTris(faceCount);
        AddUvs(block.GetTexture(Faces.Top));
        faceCount++;


        // bottom

        verts.Add(new Vector3(0 + 1, 0, 0));
        verts.Add(new Vector3(0 + 1, 0, 0 + 1));
        verts.Add(new Vector3(0, 0, 0 + 1));
        verts.Add(new Vector3(0, 0, 0));
        AddTris(faceCount);
        AddUvs(block.GetTexture(Faces.Bottom));
        faceCount++;


        // left

        verts.Add(new Vector3(0, 0, 0 + 1));
        verts.Add(new Vector3(0, 0 + 1, 0 + 1));
        verts.Add(new Vector3(0, 0 + 1, 0));
        verts.Add(new Vector3(0, 0, 0));
        AddTris(faceCount);
        AddUvs(block.GetTexture(Faces.Left));
        faceCount++;


        // right

        verts.Add(new Vector3(0 + 1, 0, 0));
        verts.Add(new Vector3(0 + 1, 0 + 1, 0));
        verts.Add(new Vector3(0 + 1, 0 + 1, 0 + 1));
        verts.Add(new Vector3(0 + 1, 0, 0 + 1));
        AddTris(faceCount);
        AddUvs(block.GetTexture(Faces.Right));
        faceCount++;


        // front

        verts.Add(new Vector3(0, 0, 0));
        verts.Add(new Vector3(0, 0 + 1, 0));
        verts.Add(new Vector3(0 + 1, 0 + 1, 0));
        verts.Add(new Vector3(0 + 1, 0, 0));
        AddTris(faceCount);
        AddUvs(block.GetTexture(Faces.Front));
        faceCount++;


        // back

        verts.Add(new Vector3(0 + 1, 0, 0 + 1));
        verts.Add(new Vector3(0 + 1, 0 + 1, 0 + 1));
        verts.Add(new Vector3(0, 0 + 1, 0 + 1));
        verts.Add(new Vector3(0, 0, 0 + 1));
        AddTris(faceCount);
        AddUvs(block.GetTexture(Faces.Back));
        faceCount++;


        // create new mesh out of the new verts and tris and apply it to the chunk
        Mesh chunkMesh = new Mesh();
        chunkMesh.SetVertices(verts);
        chunkMesh.SetTriangles(tris, 0);
        chunkMesh.SetUVs(0, uvs);
        chunkMesh.RecalculateNormals();

        GameObject go = new GameObject("mesh");
        go.AddComponent<MeshFilter>().mesh = chunkMesh;
        go.AddComponent<MeshRenderer>().material = blockmat;
        go.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        go.transform.position = new Vector3(-0.5f, -0.5f, -0.5f);
        GameObject go2 = new GameObject(block.name);
        go.transform.SetParent(go2.transform);
        return go2;
    }

    private void AddTris (int fc) {
        tris.Add(fc * 4);
        tris.Add(fc * 4 + 1);
        tris.Add(fc * 4 + 2);
        tris.Add(fc * 4);
        tris.Add(fc * 4 + 2);
        tris.Add(fc * 4 + 3);
    }

    private void AddUvs (Vector2 texture) {
        uvs.Add(new Vector2(TextureAtlasSize * texture.x + UVBleedCompromise, TextureAtlasSize * ((1/TextureAtlasSize) - 1 - texture.y) + UVBleedCompromise));
        uvs.Add(new Vector2(TextureAtlasSize * texture.x + UVBleedCompromise, TextureAtlasSize * ((1/TextureAtlasSize) - 1 - texture.y) + TextureAtlasSize - UVBleedCompromise));
        uvs.Add(new Vector2(TextureAtlasSize * texture.x + TextureAtlasSize - UVBleedCompromise, TextureAtlasSize * ((1/TextureAtlasSize) - 1 - texture.y) + TextureAtlasSize - UVBleedCompromise));
        uvs.Add(new Vector2(TextureAtlasSize * texture.x + TextureAtlasSize - UVBleedCompromise, TextureAtlasSize * ((1/TextureAtlasSize) - 1 - texture.y) + UVBleedCompromise));
    }
}
