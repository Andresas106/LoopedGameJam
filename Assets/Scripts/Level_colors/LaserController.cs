using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laser; // Objeto del l�ser
    public LineRenderer lineRenderer; // Componente LineRenderer del l�ser
    public float laserLength = 10f; // Longitud m�xima del l�ser
    public Transform laserStart; // Punto inicial del l�ser
    public enum Direction { Izquierda, Derecha, Arriba, Abajo }
    public Direction selectedDirection; // Direcci�n elegida por el jugador

    private void Start()
    {
        // Configurar la posici�n inicial del l�ser
        lineRenderer.positionCount = 2;
        UpdateLaser(laserStart.position, laserStart.position + Vector3.left * laserLength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar la posici�n donde el jugador muri�
            Vector3 deathPosition = other.transform.position;

            // Cambiar la direcci�n del l�ser en el punto de muerte
            ChangeLaserDirection(deathPosition);
        }
    }

    private void ChangeLaserDirection(Vector3 deathPosition)
    {
        // Calcular la posici�n relativa dentro del l�ser
        Vector3 startPosition = laserStart.position;
        Vector3 directionToDeath = (deathPosition - startPosition).normalized;
        float distanceToDeath = Vector3.Distance(startPosition, deathPosition);

        // Actualizar el primer tramo del l�ser (hasta el punto de muerte)
        UpdateLaser(startPosition, deathPosition);

        // Calcular la nueva direcci�n basada en la elecci�n del jugador
        Vector3 newDirection = Vector3.zero;
        switch (selectedDirection)
        {
            case Direction.Arriba:
                newDirection = Vector3.left;
                break;
            case Direction.Abajo:
                newDirection = Vector3.right;
                break;
            case Direction.Derecha:
                newDirection = Vector3.forward;
                break;
            case Direction.Izquierda:
                newDirection = Vector3.back;
                break;
        }

        // A�adir el segundo tramo del l�ser con la nueva direcci�n
        Vector3 newLaserEnd = deathPosition + newDirection * (laserLength - distanceToDeath);
        lineRenderer.positionCount = 3; // A�adir un tercer punto
        lineRenderer.SetPosition(2, newLaserEnd);
    }

    private void UpdateLaser(Vector3 start, Vector3 intendedEnd)
    {
        // Crea un raycast desde el punto inicial hacia la direcci�n del l�ser
        RaycastHit hit;
        Vector3 direction = (intendedEnd - start).normalized;
        float maxDistance = Vector3.Distance(start, intendedEnd);

        if (Physics.Raycast(start, direction, out hit, maxDistance))
        {
            // Si se detecta un obst�culo, corta el l�ser en ese punto
            lineRenderer.positionCount = 2; // Aseg�rate de tener 2 puntos
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // Si no hay obst�culos, traza el l�ser normalmente
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, intendedEnd);
        }
    }
}
