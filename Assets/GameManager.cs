using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    private ARSession _arSession;
    private bool _gameStarted = false;

    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;

    [SerializeField] private GameObject mapPrefab; // ? เพิ่มตัวแปรแมพ
    private GameObject mapInstance; // ? เก็บแมพที่ถูกสร้าง

    void Start()
    {
        _arSession = FindFirstObjectByType<ARSession>();

        UIGameManager.OnStartButtonPressed += StartGame;
        UIGameManager.OnRestartButtonPressed += RestartGame;
    }

    void StartGame()
    {
        if (_gameStarted) return;
        _gameStarted = true;
        print("Game started!!!");

        // ? สร้างแมพขึ้นมาก่อน
        if (!mapInstance)
        {
            mapInstance = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

        // ? กำหนดตำแหน่งให้ตัวละครอยู่บนแมพ
        if (!playerInstance && mapInstance)
        {
            Vector3 spawnPosition = mapInstance.transform.position + new Vector3(0, 1, 0); // สูงขึ้นเล็กน้อย
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }

        planeManager.enabled = false;
        foreach (var plane in planeManager.trackables)
        {
            var meshVisual = plane.GetComponent<ARPlaneMeshVisualizer>();
            if (meshVisual) meshVisual.enabled = false;

            var lineVisual = plane.GetComponent<LineRenderer>();
            if (lineVisual) lineVisual.enabled = false;
        }
    }

    void RestartGame()
    {
        _gameStarted = false;

        // ? ลบแมพเมื่อรีสตาร์ท
        if (mapInstance)
        {
            Destroy(mapInstance);
            mapInstance = null;
        }

        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        while (ARSession.state != ARSessionState.SessionTracking)
        {
            yield return null;
        }
        _arSession.Reset();
        planeManager.enabled = true;
    }
}