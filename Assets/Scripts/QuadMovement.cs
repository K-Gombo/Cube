using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadMovement : MonoBehaviour
{
    private Transform startPos;
    private Transform endPos;
    private float moveSpeed = 4f;
    private float startTime;
    private float journeyLength;
    private float spawnInterval = 3f;

    private bool isMoving = false;

    private GameManager.FaceGroup referencedFaceGroup;
    
    
    public void Initialize(Transform start, Transform end, float interval, GameManager.FaceGroup faceGroup)
    {
        startPos = start;
        endPos = end;
        spawnInterval = interval;
        referencedFaceGroup = faceGroup;

        transform.position = startPos.position;
        transform.rotation = startPos.rotation;
        transform.localScale = startPos.localScale;

        startTime = Time.time;
        journeyLength = Vector3.Distance(startPos.position, endPos.position);

        // moveSpeed를 GameManager의 globalMoveSpeed로 설정
        moveSpeed = GameManager.globalMoveSpeed;
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    void Update()
    {
        if (GameManager.instance.IsGameOver())
            return;  // 게임 종료 상태면 아무 동작도 하지 않음

        moveSpeed = GameManager.globalMoveSpeed; // 동기화 추가
        if (isMoving)
            MoveQuad();
        Debug.Log("Current moveSpeed: " + moveSpeed);
    }
    
    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    void MoveQuad()
    {
        float distCovered = (Time.time - startTime) * moveSpeed;
        float fractionOfJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfJourney);
        transform.rotation = Quaternion.Lerp(startPos.rotation, endPos.rotation, fractionOfJourney);
        transform.localScale = Vector3.Lerp(startPos.localScale, endPos.localScale, fractionOfJourney);

        if (fractionOfJourney >= 1f)
        {
            isMoving = false;

            int currentTopFace = GameManager.instance.DetermineTopFace();
            if (referencedFaceGroup == (GameManager.FaceGroup)(currentTopFace - 1))
            {
               GameManager.instance.MatchSuccess();
            }
            else
            {
                GameManager.instance.LifeLost();
            }

            Destroy(gameObject, 0f); // 즉시 삭제
            
        }
    }
}
