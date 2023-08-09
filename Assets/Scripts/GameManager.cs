using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject quadPrefab;
    public GameObject[] posLineStarts;
    public GameObject[] posLineEnds;
    public GameObject playerN;
    public GameObject endPanel;

    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    public Text scoreText; // Score Text 오브젝트
    public Text comboText; // Combo Text 오브젝트
    public Text speedText; // speed Text 오브젝트
   
    private bool isGameOver = false;

    private float minSpawnInterval = 2f;
    private float maxSpawnInterval = 5f;
    private float timer = 0f;
    private float spawnInterval = 1f;
    private float currentSpeed = 1.00f; // 현재 스피드

    public static float globalMoveSpeed = 4f;
    
    private int currentLife = 3; //현재 목숨
    private int currentScore = 0; // 현재 점수
    private int currentCombo = 0; //현재 콤보
    
    private int comboCount = 0; // 콤보 카운트
    private int baseBonusScore = 50; // 기본 보너스 점수
    
   

    public enum FaceGroup
    {
        Face1,
        Face2,
        Face3,
        Face4,
        Face5,
        Face6
    }

    public delegate void QuadCreatedDelegate(FaceGroup quadFaceGroup);

    public event QuadCreatedDelegate OnQuadCreated;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializeFaceMaterials();
        UpdateScoreUI(); // 초기 스코어를 표시해줍니다.
    }

    private void Update()
    {
        if (isGameOver)
            return; // 게임이 종료된 경우 아무 것도 실행하지 않습니다.

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnAndMoveQuad();
            timer = 0f;
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    public void SpawnAndMoveQuad()
    {
        int startPosIndex = Random.Range(0, posLineStarts.Length);
        GameObject startPos = posLineStarts[startPosIndex];
        GameObject endPos = posLineEnds[startPosIndex];

        GameObject quadInstance = Instantiate(quadPrefab);

        FaceGroup randomFaceGroup = (FaceGroup)Random.Range(0, 6);
        Material quadMaterial = GetFaceMaterial(randomFaceGroup);
        Renderer quadRenderer = quadInstance.GetComponent<Renderer>();
        quadRenderer.material = quadMaterial;

        QuadMovement quadMovement = quadInstance.GetComponent<QuadMovement>();
        quadMovement.Initialize(startPos.transform, endPos.transform, spawnInterval, randomFaceGroup);
        quadMovement.StartMoving();

        OnQuadCreated?.Invoke(randomFaceGroup);
    }

    private Material[] faceMaterials;

    private void InitializeFaceMaterials()
    {
        faceMaterials = new Material[System.Enum.GetValues(typeof(FaceGroup)).Length];
        Renderer playerRenderer = playerN.GetComponent<Renderer>();

        for (int i = 0; i < faceMaterials.Length; i++)
        {
            Transform face = playerN.transform.Find("face" + (i + 1));
            if (face != null)
            {
                faceMaterials[i] = new Material(playerRenderer.material);
                faceMaterials[i].color = face.GetComponent<Renderer>().material.color;
            }
        }
    }

    private Material GetFaceMaterial(FaceGroup faceGroup)
    {
        return faceMaterials[(int)faceGroup];
    }

    // 현재 맨 위에 올라온 면을 판단하는 함수
    public int DetermineTopFace()
    {
        float maxY = float.MinValue;
        int topFace = 1;

        for (int i = 1; i <= 6; i++)
        {
            Transform face = playerN.transform.Find("face" + i);
            if (face != null)
            {
                float faceY = face.position.y;
                if (faceY > maxY)
                {
                    maxY = faceY;
                    topFace = i;
                }
            }
        }

        return topFace;
    }

    public void LifeLost()
    {
        
        currentLife--;

        // 콤보 리셋 및 UI 업데이트
        currentCombo = 0;
        UpdateComboUI();

        switch (currentLife)
        {
            case 2:
                Destroy(life1);
                break;
            case 1:
                Destroy(life2);
                break;
            case 0:
                Destroy(life3);
                isGameOver = true; // 게임이 종료되었습니다.
                StartCoroutine(GameOverCoroutine());
                break;
        }
    }

    // 매칭 성공 시에 호출되는 함수
    public void MatchSuccess()
    {
        currentScore += 50; 
        currentCombo += 1;
        UpdateScoreUI();
        UpdateComboUI();
        
        comboCount++;  // 매치 성공할 때마다 콤보 카운트 증가
        if (comboCount % 10 == 0)
        {
            int multiplier = comboCount / 10;
            int bonusScore = baseBonusScore * multiplier;
            currentScore += bonusScore;
            // 점수에 보너스 점수 추가
            // ... 여기에 점수를 추가하는 코드를 적용
        }
       
    }
    

    private IEnumerator GameOverCoroutine()
    {
        // Life3 이미지가 사라지기를 기다립니다.
        yield return new WaitForEndOfFrame();

        // 마지막 Life가 사라진 후 0.3초 후에 게임 종료 처리를 실행합니다.
        yield return new WaitForSeconds(0.3f);

        // 게임 종료 처리
        endPanel.SetActive(true);
        // 여기에 게임 종료에 대한 처리를 추가할 수 있습니다.
    }

    // UpdateScoreUI() 메서드 내에서 1000의 배수일 때 UpdateSpeedUI() 메서드를 호출하도록 수정
    private void UpdateScoreUI()
    {
        scoreText.text = currentScore.ToString(); // 스코어 UI를 업데이트합니다.

        if (currentScore > 0 && currentScore % 1000 == 0) // 1000의 배수인 경우
        {
            GameManager.globalMoveSpeed += 0.5f; // globalMoveSpeed를 직접 증가시킵니다.
            UpdateSpeedUI(); // 1000의 배수일 때 스피드 UI를 업데이트합니다.
        }
    }

    private void UpdateComboUI()
    {
        comboText.text = currentCombo.ToString() + " COMBO"; // 콤보 UI 업데이트
    }

    // 새로운 메서드로 스피드 UI 업데이트
    private void UpdateSpeedUI()
    {
        if (currentScore > 0 && currentScore % 1000 == 0)
        {
            currentSpeed += 0.125f;
        }
        UpdateSpeedText();
    }

    private void UpdateSpeedText()
    {
        speedText.text = "x" + currentSpeed.ToString("F3");
    }
    
    public bool IsGameOver()
    {
        return isGameOver;
    }

    
    
    
}
