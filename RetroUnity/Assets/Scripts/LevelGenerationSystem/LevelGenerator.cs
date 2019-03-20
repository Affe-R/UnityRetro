using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Platform
{
    public float Length;
    public Vector2 Position;
    public float Points;

    public Platform(float length, Vector2 position, float points)
    {
        this.Length = length;
        this.Position = position;
        this.Points = points;
    }
}

public class LevelGenerator : MonoBehaviour
{
    public Vector2 WidthHeight;
    public float Spacing;

    public List<Vector2> points = new List<Vector2>();

    public Platform[] platforms;

    public int NumPlatforms = 5;
    public int Resolution = 1;

    public int PixelsPerUnit = 24;

    void CreatePlatforms()
    {
        float totalSpaceingLength = Spacing * (NumPlatforms);
        float sumPlatformsLength = NumPlatforms * (NumPlatforms + 1) / 2;
        float SCALE = (WidthHeight.x - totalSpaceingLength) / (sumPlatformsLength);

        platforms = new Platform[NumPlatforms];
        float currentX = Spacing * .5f;

        List<int> lengths = new List<int>();
        // Create list
        for (int i = 0; i < NumPlatforms; i++)
        {
            lengths.Add(i + 1);
        }

        for (int i = 0; i < lengths.Count; i++)
        {
            int length = lengths[i];
            lengths.RemoveAt(i);
            lengths.Insert(Random.Range(0, lengths.Count - 1), length);
        }

        for (int i = 0; i < NumPlatforms; i++)
        {
            int length = lengths[i];
            float Length = (length) * SCALE;
            Vector2 position = new Vector2(currentX, Random.Range(0, WidthHeight.y));
            float points = (NumPlatforms) - length;

            platforms[i] = new Platform(Length, position, points);
            currentX += Length + Spacing;
        }
    }

    Vector2[] CreatePoints()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(new Vector2(0, 0));

        for (int i = 0; i < platforms.Length; i++)
        {
            float mountainSpaceing = Spacing / Resolution + 1;
            // Add extra points
            for (int j = 0; j < Resolution; j++)
            {
                if(i - 1 < 0)
                    continue;

                float gradient = (j + 0.5f) / Resolution;
                Vector2 lastPosEnd = platforms[i - 1].Position + Vector2.right * platforms[i - 1].Length;
                float xPosition = Mathf.Lerp(lastPosEnd.x, platforms[i].Position.x, gradient);
                float yPosition = Random.Range(0, WidthHeight.y);
                points.Add(new Vector2(xPosition, yPosition));
            }

            // Add left platform position
            points.Add(platforms[i].Position);
            // Add right platform position
            points.Add(platforms[i].Position + Vector2.right * platforms[i].Length);
        }

        points.Add(new Vector2(WidthHeight.x, 0));

        this.points = points;

        return points.ToArray();
    }

    Vector2[] GetWorldPoints()
    {
        Vector2[] points = CreatePoints();
        for (int i = 0; i < points.Length; i++)
        {
            points[i] += (Vector2)transform.position - WidthHeight * .5f;
        }
        return points;
    }

    PolygonCollider2D CreateCollider(Vector2[] points)
    {
        PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();
        var test = new PolygonCollider2D();
        pc.points = points;
        pc.offset = -WidthHeight * .5f;

        return pc;
    }

    Texture2D CreateTexture(Vector2[] points, Vector2Int size, int pixelsPerUnit = 24)
    {
        Vector2Int textureSize = new Vector2Int((int)(size.x * pixelsPerUnit), (int)(size.y * pixelsPerUnit));
        Texture2D texture = new Texture2D(textureSize.x, textureSize.y, TextureFormat.RGBA32, false);

        for (int y = 0; y < textureSize.y; y++)
        {
            for (int x = 0; x < textureSize.x; x++)
            {
                float xWorld = Mathf.Lerp(0, (float)size.x, x / (float)textureSize.x);
                float yWorld = Mathf.Lerp(0, (float)size.y, y / (float)textureSize.y);

                Vector3 worldSpaceCoordinate = new Vector3(xWorld, yWorld, 0);

                if(WithinLineBounds(worldSpaceCoordinate, points))
                    texture.SetPixel(x, y, Color.red);
                else
                    texture.SetPixel(x, y, Color.clear);
            }
        }
        texture.Apply();
        texture.filterMode = FilterMode.Point;
        return texture;
    }

    bool WithinLineBounds(Vector3 point, Vector2[] lineVerts)
    {
        // Check if outside
        if(point.x < 0 || point.x > lineVerts[lineVerts.Length - 1].x || point.y < 0)
            return false;

        Vector2 leftPoint = new Vector2();
        Vector2 rightPoint = new Vector2();
        // get the closest points
        for (int i = 0; i < lineVerts.Length - 1; i++)
        {
            // If the next line is further to the right than the point
            if(point.x < lineVerts[i + 1].x)
            {
                rightPoint = lineVerts[i + 1];
                leftPoint = lineVerts[i];
                break;
            }
        }

        // No solution found
        if(rightPoint == leftPoint)
            return false;

        float scalar = (point.x - leftPoint.x) / (rightPoint.x - leftPoint.x);
        Vector2 intersectionOnLine = leftPoint + (rightPoint - leftPoint) * scalar;
        if(point.y < intersectionOnLine.y)
            return true;

        return false;
    }

    void Start()
    {
        int numberOfPoints = (int)(WidthHeight.x / Spacing);

        CreatePlatforms();
        CreatePoints();

        Vector2[] points = CreatePoints();
        CreateCollider(points);
        Texture2D texture = CreateTexture(points, new Vector2Int((int)WidthHeight.x, (int)WidthHeight.y));
        Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), PixelsPerUnit);
        gameObject.AddComponent<SpriteRenderer>().sprite = sprite;
    }

    void OnDrawGizmosSelected()
    {
        Vector2 bottomLeft = (Vector2)transform.position - WidthHeight * .5f;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, WidthHeight);
        Gizmos.color = Color.white;

        // for (int i = 0; i < points.Count - 1; i++)
        // {
        //     Gizmos.DrawLine(bottomLeft + points[i], bottomLeft + points[i + 1]);
        // }
    }
}
