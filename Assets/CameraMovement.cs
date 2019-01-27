using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float smoothTime = 0.05F;
    [SerializeField] Transform background;
    private Vector3 velocity = Vector3.zero;

    private float rightBound;
    private float leftBound;
    private float topBound;
    private float bottomBound;
    Bounds spriteBounds;

    private void Start()
    {
        spriteBounds = background.GetComponent<SpriteRenderer>().sprite.bounds;
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        leftBound = (float)(horzExtent - spriteBounds.size.x / 2.0f);
        rightBound = (float)(spriteBounds.size.x / 2.0f - horzExtent);
        bottomBound = (float)(vertExtent - spriteBounds.size.y / 2.0f);
        topBound = (float)(spriteBounds.size.y / 2.0f - vertExtent);
    }

    void Update()
    {
        if (target != null) { 
            var pos = new Vector3(target.position.x, target.position.y, transform.position.z);
            pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
            pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
            Vector3 cameraPosition = pos;

            transform.position = Vector3.SmoothDamp(transform.position,
                                                    cameraPosition,
                                                    ref velocity,
                                                    smoothTime);
        }
    }
}
