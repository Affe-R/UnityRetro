using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Platform
{
    public float Length;
    public Vector2 Position;

    public Platform(float length, Vector2 position)
    {
        this.Length = length;
        this.Position = position;
    }
}

public class LevelGenerator : MonoBehaviour
{
    // public float MaxHeight;
    // public float MinHeight;

    public Vector2 WidthHeight;
    public float Spaceing;

    public List<Vector2> points = new List<Vector2>();

    public Platform[] platforms;

    void Start()
    {
        int numberOfPoints = (int)(WidthHeight.x / Spaceing);

        for (float i = 0; i < numberOfPoints; i += Spaceing)
        {
            
        }
        
        // for (int i = 0; i < platforms.Length; i++)
        // {
            

        //     // Vector2 platformStartPos = platforms[i].Position;
        //     // Vector2 platformEndPosition = platforms[i].Position + Vector2.right * platforms[i].Length;

        //     // float currentXPos = points[points.Count - 1].x;

        //     // while(currentXPos < platformStartPos.x)
        //     // {
        //     //     currentXPos += Spaceing;
        //     // }

        //     // // int pointsBefore = 

        //     // // Loop through positions between last point and current platform
        //     // for (float j = platformEndPosition.x; j < platforms[i].Position.x; j += Spaceing)
        //     // {
        //     //     float xPosition = j * Spaceing;
        //     // }
        // }

        // Draw line
        for (int i = 0; i < numberOfPoints; i++)
        {
            float randomHeight = Random.Range(0, 1.0f);
            randomHeight = Mathf.Lerp(transform.position.y - WidthHeight.y * .5f, transform.position.y + WidthHeight.y * .5f, randomHeight);

            points.Add(new Vector2(i - WidthHeight.x * .5f, randomHeight));
        }

        // points[numberOfPoints / 2] = new Vector2(points[numberOfPoints / 2].x, points[(numberOfPoints / 2) - 1].y);

        // Fill area
        // Apply collider   
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, WidthHeight);

        Gizmos.color = Color.white;
        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }

        Gizmos.DrawLine(points[points.Count - 1], new Vector2(points[points.Count - 1].x + Spaceing, points[0].y));

        for (int i = 0; i < platforms.Length; i++)
        {
            Vector3 bottomRight = new Vector3(transform.position.x - WidthHeight.x * .5f, transform.position.y - WidthHeight.y * .5f);
            Vector2 worldPosition = bottomRight + (Vector3)platforms[i].Position;
            Gizmos.DrawLine(worldPosition, worldPosition + Vector2.right * platforms[i].Length);
        }

    }
}
